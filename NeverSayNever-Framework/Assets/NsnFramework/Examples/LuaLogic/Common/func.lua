local _setmetatableindex
-- 设置元表索引
_setmetatableindex = function(t, index)
    if type(t) == "userdata" then
        local peer = tolua.getpeer(t)
        if not peer then
            peer = {}
            tolua.setpeer(t, peer)
        end
        _setmetatableindex(peer, index)
    else
        local mt = getmetatable(t)
        if not mt then mt = {} end
        if not mt.__index then
            mt.__index = index
            setmetatable(t, mt)
        elseif mt.__index ~= index then
            _setmetatableindex(mt, index)
        end
    end
end
-- 声明一个全局的设置元表index的方法
setmetatableindex = _setmetatableindex

-- 深拷贝对象
function clone(object)
    local lookup_table = {}
    local function _copy(object)
        if type(object) ~= "table" then
            return object
        elseif lookup_table[object] then
            return lookup_table[object]
        end
        local newObject = {}
        lookup_table[object] = newObject
        for key, value in pairs(object) do
            newObject[_copy(key)] = _copy(value)
        end
        return setmetatable(newObject, getmetatable(object))
    end
    return _copy(object)
end

-- 类方法，模拟继承
function class(classname, ...)
    local cls = {__cname = classname}

    local supers = {...}
    for _, super in ipairs(supers) do
        local superType = type(super)
        assert(superType == "nil" or superType == "table" or superType == "function",
            string.format("class() - create class \"%s\" with invalid super class type \"%s\"",
                classname, superType))

        if superType == "function" then
            assert(cls.__create == nil,
                string.format("class() - create class \"%s\" with more than one creating function",
                    classname));
            -- if super is function, set it to __create
            cls.__create = super
        elseif superType == "table" then
            if super[".isclass"] then
                -- super is native class
                assert(cls.__create == nil,
                    string.format("class() - create class \"%s\" with more than one creating function or native class",
                        classname));
                cls.__create = function() return super:create() end
            else
                -- super is pure lua class
                cls.__supers = cls.__supers or {}
                cls.__supers[#cls.__supers + 1] = super
                if not cls.super then
                    -- set first super pure lua class as class.super
                    cls.super = super
                end
            end
        else
            error(string.format("class() - create class \"%s\" with invalid super type",
                        classname), 0)
        end
    end

    cls.__index = cls
    if not cls.__supers or #cls.__supers == 1 then
        setmetatable(cls, {__index = cls.super})
    else
        setmetatable(cls, {__index = function(_, key)
            local supers = cls.__supers
            for i = 1, #supers do
                local super = supers[i]
                if super[key] then return super[key] end
            end
        end})
    end

    if not cls.ctor then
        -- add default constructor
        cls.ctor = function() end
    end
    cls.new = function(...)
        local instance
        if cls.__create then
            instance = cls.__create(...)
        else
            instance = {}
        end
        setmetatableindex(instance, cls)
        instance.class = cls
        instance:ctor(...)
        return instance
    end
    cls.create = function(_, ...)
        return cls.new(...)
    end

    return cls
end

-- 判断对象的数据类型
local iskindof_
iskindof_ = function(cls, name)
    local __index = rawget(cls, "__index")
    if type(__index) == "table" and rawget(__index, "__cname") == name then return true end

    if rawget(cls, "__cname") == name then return true end
    local __supers = rawget(cls, "__supers")
    if not __supers then return false end
    for _, super in ipairs(__supers) do
        if iskindof_(super, name) then return true end
    end
    return false
end

function iskindof(obj, classname)
    local t = type(obj)
    if t ~= "table" and t ~= "userdata" then return false end

    local mt
    if t == "userdata" then
        if tolua.iskindof(obj, classname) then return true end
        mt = tolua.getpeer(obj)
    else
        mt = getmetatable(obj)
    end
    if mt then
        return iskindof_(mt, classname)
    end
    return false
end