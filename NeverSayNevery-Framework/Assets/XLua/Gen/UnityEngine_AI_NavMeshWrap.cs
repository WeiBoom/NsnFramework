#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class UnityEngineAINavMeshWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UnityEngine.AI.NavMesh);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 21, 3, 3);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "Raycast", _m_Raycast_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CalculatePath", _m_CalculatePath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FindClosestEdge", _m_FindClosestEdge_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SamplePosition", _m_SamplePosition_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetAreaCost", _m_SetAreaCost_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetAreaCost", _m_GetAreaCost_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetAreaFromName", _m_GetAreaFromName_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CalculateTriangulation", _m_CalculateTriangulation_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "AddNavMeshData", _m_AddNavMeshData_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RemoveNavMeshData", _m_RemoveNavMeshData_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "AddLink", _m_AddLink_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RemoveLink", _m_RemoveLink_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateSettings", _m_CreateSettings_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RemoveSettings", _m_RemoveSettings_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetSettingsByID", _m_GetSettingsByID_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetSettingsCount", _m_GetSettingsCount_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetSettingsByIndex", _m_GetSettingsByIndex_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetSettingsNameFromID", _m_GetSettingsNameFromID_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RemoveAllNavMeshData", _m_RemoveAllNavMeshData_xlua_st_);
            
			
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "AllAreas", UnityEngine.AI.NavMesh.AllAreas);
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "avoidancePredictionTime", _g_get_avoidancePredictionTime);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "pathfindingIterationsPerFrame", _g_get_pathfindingIterationsPerFrame);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "onPreUpdate", _g_get_onPreUpdate);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "avoidancePredictionTime", _s_set_avoidancePredictionTime);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "pathfindingIterationsPerFrame", _s_set_pathfindingIterationsPerFrame);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "onPreUpdate", _s_set_onPreUpdate);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "UnityEngine.AI.NavMesh does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Raycast_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 1)&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _sourcePosition;translator.Get(L, 1, out _sourcePosition);
                    UnityEngine.Vector3 _targetPosition;translator.Get(L, 2, out _targetPosition);
                    UnityEngine.AI.NavMeshHit _hit;
                    int _areaMask = LuaAPI.xlua_tointeger(L, 3);
                    
                        bool gen_ret = UnityEngine.AI.NavMesh.Raycast( _sourcePosition, _targetPosition, out _hit, _areaMask );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    translator.Push(L, _hit);
                        
                    
                    
                    
                    return 2;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 1)&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& translator.Assignable<UnityEngine.AI.NavMeshQueryFilter>(L, 3)) 
                {
                    UnityEngine.Vector3 _sourcePosition;translator.Get(L, 1, out _sourcePosition);
                    UnityEngine.Vector3 _targetPosition;translator.Get(L, 2, out _targetPosition);
                    UnityEngine.AI.NavMeshHit _hit;
                    UnityEngine.AI.NavMeshQueryFilter _filter;translator.Get(L, 3, out _filter);
                    
                        bool gen_ret = UnityEngine.AI.NavMesh.Raycast( _sourcePosition, _targetPosition, out _hit, _filter );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    translator.Push(L, _hit);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.AI.NavMesh.Raycast!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CalculatePath_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3>(L, 1)&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<UnityEngine.AI.NavMeshPath>(L, 4)) 
                {
                    UnityEngine.Vector3 _sourcePosition;translator.Get(L, 1, out _sourcePosition);
                    UnityEngine.Vector3 _targetPosition;translator.Get(L, 2, out _targetPosition);
                    int _areaMask = LuaAPI.xlua_tointeger(L, 3);
                    UnityEngine.AI.NavMeshPath _path = (UnityEngine.AI.NavMeshPath)translator.GetObject(L, 4, typeof(UnityEngine.AI.NavMeshPath));
                    
                        bool gen_ret = UnityEngine.AI.NavMesh.CalculatePath( _sourcePosition, _targetPosition, _areaMask, _path );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3>(L, 1)&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& translator.Assignable<UnityEngine.AI.NavMeshQueryFilter>(L, 3)&& translator.Assignable<UnityEngine.AI.NavMeshPath>(L, 4)) 
                {
                    UnityEngine.Vector3 _sourcePosition;translator.Get(L, 1, out _sourcePosition);
                    UnityEngine.Vector3 _targetPosition;translator.Get(L, 2, out _targetPosition);
                    UnityEngine.AI.NavMeshQueryFilter _filter;translator.Get(L, 3, out _filter);
                    UnityEngine.AI.NavMeshPath _path = (UnityEngine.AI.NavMeshPath)translator.GetObject(L, 4, typeof(UnityEngine.AI.NavMeshPath));
                    
                        bool gen_ret = UnityEngine.AI.NavMesh.CalculatePath( _sourcePosition, _targetPosition, _filter, _path );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.AI.NavMesh.CalculatePath!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FindClosestEdge_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Vector3>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    UnityEngine.Vector3 _sourcePosition;translator.Get(L, 1, out _sourcePosition);
                    UnityEngine.AI.NavMeshHit _hit;
                    int _areaMask = LuaAPI.xlua_tointeger(L, 2);
                    
                        bool gen_ret = UnityEngine.AI.NavMesh.FindClosestEdge( _sourcePosition, out _hit, _areaMask );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    translator.Push(L, _hit);
                        
                    
                    
                    
                    return 2;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Vector3>(L, 1)&& translator.Assignable<UnityEngine.AI.NavMeshQueryFilter>(L, 2)) 
                {
                    UnityEngine.Vector3 _sourcePosition;translator.Get(L, 1, out _sourcePosition);
                    UnityEngine.AI.NavMeshHit _hit;
                    UnityEngine.AI.NavMeshQueryFilter _filter;translator.Get(L, 2, out _filter);
                    
                        bool gen_ret = UnityEngine.AI.NavMesh.FindClosestEdge( _sourcePosition, out _hit, _filter );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    translator.Push(L, _hit);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.AI.NavMesh.FindClosestEdge!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SamplePosition_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _sourcePosition;translator.Get(L, 1, out _sourcePosition);
                    UnityEngine.AI.NavMeshHit _hit;
                    float _maxDistance = (float)LuaAPI.lua_tonumber(L, 2);
                    int _areaMask = LuaAPI.xlua_tointeger(L, 3);
                    
                        bool gen_ret = UnityEngine.AI.NavMesh.SamplePosition( _sourcePosition, out _hit, _maxDistance, _areaMask );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    translator.Push(L, _hit);
                        
                    
                    
                    
                    return 2;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.AI.NavMeshQueryFilter>(L, 3)) 
                {
                    UnityEngine.Vector3 _sourcePosition;translator.Get(L, 1, out _sourcePosition);
                    UnityEngine.AI.NavMeshHit _hit;
                    float _maxDistance = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.AI.NavMeshQueryFilter _filter;translator.Get(L, 3, out _filter);
                    
                        bool gen_ret = UnityEngine.AI.NavMesh.SamplePosition( _sourcePosition, out _hit, _maxDistance, _filter );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    translator.Push(L, _hit);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.AI.NavMesh.SamplePosition!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAreaCost_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    int _areaIndex = LuaAPI.xlua_tointeger(L, 1);
                    float _cost = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    UnityEngine.AI.NavMesh.SetAreaCost( _areaIndex, _cost );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetAreaCost_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    int _areaIndex = LuaAPI.xlua_tointeger(L, 1);
                    
                        float gen_ret = UnityEngine.AI.NavMesh.GetAreaCost( _areaIndex );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetAreaFromName_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _areaName = LuaAPI.lua_tostring(L, 1);
                    
                        int gen_ret = UnityEngine.AI.NavMesh.GetAreaFromName( _areaName );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CalculateTriangulation_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        UnityEngine.AI.NavMeshTriangulation gen_ret = UnityEngine.AI.NavMesh.CalculateTriangulation(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddNavMeshData_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& translator.Assignable<UnityEngine.AI.NavMeshData>(L, 1)) 
                {
                    UnityEngine.AI.NavMeshData _navMeshData = (UnityEngine.AI.NavMeshData)translator.GetObject(L, 1, typeof(UnityEngine.AI.NavMeshData));
                    
                        UnityEngine.AI.NavMeshDataInstance gen_ret = UnityEngine.AI.NavMesh.AddNavMeshData( _navMeshData );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.AI.NavMeshData>(L, 1)&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& translator.Assignable<UnityEngine.Quaternion>(L, 3)) 
                {
                    UnityEngine.AI.NavMeshData _navMeshData = (UnityEngine.AI.NavMeshData)translator.GetObject(L, 1, typeof(UnityEngine.AI.NavMeshData));
                    UnityEngine.Vector3 _position;translator.Get(L, 2, out _position);
                    UnityEngine.Quaternion _rotation;translator.Get(L, 3, out _rotation);
                    
                        UnityEngine.AI.NavMeshDataInstance gen_ret = UnityEngine.AI.NavMesh.AddNavMeshData( _navMeshData, _position, _rotation );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.AI.NavMesh.AddNavMeshData!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveNavMeshData_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.AI.NavMeshDataInstance _handle;translator.Get(L, 1, out _handle);
                    
                    UnityEngine.AI.NavMesh.RemoveNavMeshData( _handle );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddLink_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& translator.Assignable<UnityEngine.AI.NavMeshLinkData>(L, 1)) 
                {
                    UnityEngine.AI.NavMeshLinkData _link;translator.Get(L, 1, out _link);
                    
                        UnityEngine.AI.NavMeshLinkInstance gen_ret = UnityEngine.AI.NavMesh.AddLink( _link );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.AI.NavMeshLinkData>(L, 1)&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& translator.Assignable<UnityEngine.Quaternion>(L, 3)) 
                {
                    UnityEngine.AI.NavMeshLinkData _link;translator.Get(L, 1, out _link);
                    UnityEngine.Vector3 _position;translator.Get(L, 2, out _position);
                    UnityEngine.Quaternion _rotation;translator.Get(L, 3, out _rotation);
                    
                        UnityEngine.AI.NavMeshLinkInstance gen_ret = UnityEngine.AI.NavMesh.AddLink( _link, _position, _rotation );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.AI.NavMesh.AddLink!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveLink_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.AI.NavMeshLinkInstance _handle;translator.Get(L, 1, out _handle);
                    
                    UnityEngine.AI.NavMesh.RemoveLink( _handle );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateSettings_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        UnityEngine.AI.NavMeshBuildSettings gen_ret = UnityEngine.AI.NavMesh.CreateSettings(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveSettings_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    int _agentTypeID = LuaAPI.xlua_tointeger(L, 1);
                    
                    UnityEngine.AI.NavMesh.RemoveSettings( _agentTypeID );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetSettingsByID_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    int _agentTypeID = LuaAPI.xlua_tointeger(L, 1);
                    
                        UnityEngine.AI.NavMeshBuildSettings gen_ret = UnityEngine.AI.NavMesh.GetSettingsByID( _agentTypeID );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetSettingsCount_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        int gen_ret = UnityEngine.AI.NavMesh.GetSettingsCount(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetSettingsByIndex_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 1);
                    
                        UnityEngine.AI.NavMeshBuildSettings gen_ret = UnityEngine.AI.NavMesh.GetSettingsByIndex( _index );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetSettingsNameFromID_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    int _agentTypeID = LuaAPI.xlua_tointeger(L, 1);
                    
                        string gen_ret = UnityEngine.AI.NavMesh.GetSettingsNameFromID( _agentTypeID );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveAllNavMeshData_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    UnityEngine.AI.NavMesh.RemoveAllNavMeshData(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_avoidancePredictionTime(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushnumber(L, UnityEngine.AI.NavMesh.avoidancePredictionTime);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_pathfindingIterationsPerFrame(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, UnityEngine.AI.NavMesh.pathfindingIterationsPerFrame);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onPreUpdate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, UnityEngine.AI.NavMesh.onPreUpdate);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_avoidancePredictionTime(RealStatePtr L)
        {
		    try {
                
			    UnityEngine.AI.NavMesh.avoidancePredictionTime = (float)LuaAPI.lua_tonumber(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_pathfindingIterationsPerFrame(RealStatePtr L)
        {
		    try {
                
			    UnityEngine.AI.NavMesh.pathfindingIterationsPerFrame = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onPreUpdate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    UnityEngine.AI.NavMesh.onPreUpdate = translator.GetDelegate<UnityEngine.AI.NavMesh.OnNavMeshPreUpdate>(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
