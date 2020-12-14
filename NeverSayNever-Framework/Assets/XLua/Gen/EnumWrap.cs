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
    
    public class UnityEngineRuntimePlatformWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.RuntimePlatform), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.RuntimePlatform), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.RuntimePlatform), L, null, 38, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OSXEditor", UnityEngine.RuntimePlatform.OSXEditor);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OSXPlayer", UnityEngine.RuntimePlatform.OSXPlayer);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "WindowsPlayer", UnityEngine.RuntimePlatform.WindowsPlayer);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "WindowsEditor", UnityEngine.RuntimePlatform.WindowsEditor);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "IPhonePlayer", UnityEngine.RuntimePlatform.IPhonePlayer);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Android", UnityEngine.RuntimePlatform.Android);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LinuxPlayer", UnityEngine.RuntimePlatform.LinuxPlayer);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LinuxEditor", UnityEngine.RuntimePlatform.LinuxEditor);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "WebGLPlayer", UnityEngine.RuntimePlatform.WebGLPlayer);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "WSAPlayerX86", UnityEngine.RuntimePlatform.WSAPlayerX86);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "WSAPlayerX64", UnityEngine.RuntimePlatform.WSAPlayerX64);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "WSAPlayerARM", UnityEngine.RuntimePlatform.WSAPlayerARM);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PS4", UnityEngine.RuntimePlatform.PS4);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "XboxOne", UnityEngine.RuntimePlatform.XboxOne);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "tvOS", UnityEngine.RuntimePlatform.tvOS);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Switch", UnityEngine.RuntimePlatform.Switch);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Lumin", UnityEngine.RuntimePlatform.Lumin);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Stadia", UnityEngine.RuntimePlatform.Stadia);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "CloudRendering", UnityEngine.RuntimePlatform.CloudRendering);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.RuntimePlatform), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineRuntimePlatform(L, (UnityEngine.RuntimePlatform)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "OSXEditor"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.OSXEditor);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OSXPlayer"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.OSXPlayer);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "WindowsPlayer"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.WindowsPlayer);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "WindowsEditor"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.WindowsEditor);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "IPhonePlayer"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.IPhonePlayer);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Android"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.Android);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LinuxPlayer"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.LinuxPlayer);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LinuxEditor"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.LinuxEditor);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "WebGLPlayer"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.WebGLPlayer);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "WSAPlayerX86"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.WSAPlayerX86);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "WSAPlayerX64"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.WSAPlayerX64);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "WSAPlayerARM"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.WSAPlayerARM);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "PS4"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.PS4);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "XboxOne"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.XboxOne);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "tvOS"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.tvOS);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Switch"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.Switch);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Lumin"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.Lumin);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Stadia"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.Stadia);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "CloudRendering"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.CloudRendering);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.RuntimePlatform!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.RuntimePlatform! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEnginePrimitiveTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.PrimitiveType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.PrimitiveType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.PrimitiveType), L, null, 7, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Sphere", UnityEngine.PrimitiveType.Sphere);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Capsule", UnityEngine.PrimitiveType.Capsule);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Cylinder", UnityEngine.PrimitiveType.Cylinder);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Cube", UnityEngine.PrimitiveType.Cube);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Plane", UnityEngine.PrimitiveType.Plane);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Quad", UnityEngine.PrimitiveType.Quad);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.PrimitiveType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEnginePrimitiveType(L, (UnityEngine.PrimitiveType)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Sphere"))
                {
                    translator.PushUnityEnginePrimitiveType(L, UnityEngine.PrimitiveType.Sphere);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Capsule"))
                {
                    translator.PushUnityEnginePrimitiveType(L, UnityEngine.PrimitiveType.Capsule);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Cylinder"))
                {
                    translator.PushUnityEnginePrimitiveType(L, UnityEngine.PrimitiveType.Cylinder);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Cube"))
                {
                    translator.PushUnityEnginePrimitiveType(L, UnityEngine.PrimitiveType.Cube);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Plane"))
                {
                    translator.PushUnityEnginePrimitiveType(L, UnityEngine.PrimitiveType.Plane);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Quad"))
                {
                    translator.PushUnityEnginePrimitiveType(L, UnityEngine.PrimitiveType.Quad);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.PrimitiveType!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.PrimitiveType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineWrapModeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.WrapMode), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.WrapMode), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.WrapMode), L, null, 7, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Once", UnityEngine.WrapMode.Once);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Loop", UnityEngine.WrapMode.Loop);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PingPong", UnityEngine.WrapMode.PingPong);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Default", UnityEngine.WrapMode.Default);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ClampForever", UnityEngine.WrapMode.ClampForever);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Clamp", UnityEngine.WrapMode.Clamp);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.WrapMode), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineWrapMode(L, (UnityEngine.WrapMode)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Once"))
                {
                    translator.PushUnityEngineWrapMode(L, UnityEngine.WrapMode.Once);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Loop"))
                {
                    translator.PushUnityEngineWrapMode(L, UnityEngine.WrapMode.Loop);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "PingPong"))
                {
                    translator.PushUnityEngineWrapMode(L, UnityEngine.WrapMode.PingPong);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Default"))
                {
                    translator.PushUnityEngineWrapMode(L, UnityEngine.WrapMode.Default);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ClampForever"))
                {
                    translator.PushUnityEngineWrapMode(L, UnityEngine.WrapMode.ClampForever);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Clamp"))
                {
                    translator.PushUnityEngineWrapMode(L, UnityEngine.WrapMode.Clamp);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.WrapMode!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.WrapMode! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineAINavMeshPathStatusWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.AI.NavMeshPathStatus), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.AI.NavMeshPathStatus), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.AI.NavMeshPathStatus), L, null, 4, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PathComplete", UnityEngine.AI.NavMeshPathStatus.PathComplete);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PathPartial", UnityEngine.AI.NavMeshPathStatus.PathPartial);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PathInvalid", UnityEngine.AI.NavMeshPathStatus.PathInvalid);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.AI.NavMeshPathStatus), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineAINavMeshPathStatus(L, (UnityEngine.AI.NavMeshPathStatus)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "PathComplete"))
                {
                    translator.PushUnityEngineAINavMeshPathStatus(L, UnityEngine.AI.NavMeshPathStatus.PathComplete);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "PathPartial"))
                {
                    translator.PushUnityEngineAINavMeshPathStatus(L, UnityEngine.AI.NavMeshPathStatus.PathPartial);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "PathInvalid"))
                {
                    translator.PushUnityEngineAINavMeshPathStatus(L, UnityEngine.AI.NavMeshPathStatus.PathInvalid);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.AI.NavMeshPathStatus!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.AI.NavMeshPathStatus! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineAIObstacleAvoidanceTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.AI.ObstacleAvoidanceType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.AI.ObstacleAvoidanceType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.AI.ObstacleAvoidanceType), L, null, 6, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NoObstacleAvoidance", UnityEngine.AI.ObstacleAvoidanceType.NoObstacleAvoidance);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LowQualityObstacleAvoidance", UnityEngine.AI.ObstacleAvoidanceType.LowQualityObstacleAvoidance);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "MedQualityObstacleAvoidance", UnityEngine.AI.ObstacleAvoidanceType.MedQualityObstacleAvoidance);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "GoodQualityObstacleAvoidance", UnityEngine.AI.ObstacleAvoidanceType.GoodQualityObstacleAvoidance);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "HighQualityObstacleAvoidance", UnityEngine.AI.ObstacleAvoidanceType.HighQualityObstacleAvoidance);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.AI.ObstacleAvoidanceType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineAIObstacleAvoidanceType(L, (UnityEngine.AI.ObstacleAvoidanceType)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "NoObstacleAvoidance"))
                {
                    translator.PushUnityEngineAIObstacleAvoidanceType(L, UnityEngine.AI.ObstacleAvoidanceType.NoObstacleAvoidance);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LowQualityObstacleAvoidance"))
                {
                    translator.PushUnityEngineAIObstacleAvoidanceType(L, UnityEngine.AI.ObstacleAvoidanceType.LowQualityObstacleAvoidance);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "MedQualityObstacleAvoidance"))
                {
                    translator.PushUnityEngineAIObstacleAvoidanceType(L, UnityEngine.AI.ObstacleAvoidanceType.MedQualityObstacleAvoidance);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "GoodQualityObstacleAvoidance"))
                {
                    translator.PushUnityEngineAIObstacleAvoidanceType(L, UnityEngine.AI.ObstacleAvoidanceType.GoodQualityObstacleAvoidance);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "HighQualityObstacleAvoidance"))
                {
                    translator.PushUnityEngineAIObstacleAvoidanceType(L, UnityEngine.AI.ObstacleAvoidanceType.HighQualityObstacleAvoidance);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.AI.ObstacleAvoidanceType!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.AI.ObstacleAvoidanceType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineAINavMeshObstacleShapeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.AI.NavMeshObstacleShape), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.AI.NavMeshObstacleShape), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.AI.NavMeshObstacleShape), L, null, 3, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Capsule", UnityEngine.AI.NavMeshObstacleShape.Capsule);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Box", UnityEngine.AI.NavMeshObstacleShape.Box);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.AI.NavMeshObstacleShape), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineAINavMeshObstacleShape(L, (UnityEngine.AI.NavMeshObstacleShape)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Capsule"))
                {
                    translator.PushUnityEngineAINavMeshObstacleShape(L, UnityEngine.AI.NavMeshObstacleShape.Capsule);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Box"))
                {
                    translator.PushUnityEngineAINavMeshObstacleShape(L, UnityEngine.AI.NavMeshObstacleShape.Box);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.AI.NavMeshObstacleShape!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.AI.NavMeshObstacleShape! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineAIOffMeshLinkTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.AI.OffMeshLinkType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.AI.OffMeshLinkType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.AI.OffMeshLinkType), L, null, 4, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LinkTypeManual", UnityEngine.AI.OffMeshLinkType.LinkTypeManual);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LinkTypeDropDown", UnityEngine.AI.OffMeshLinkType.LinkTypeDropDown);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LinkTypeJumpAcross", UnityEngine.AI.OffMeshLinkType.LinkTypeJumpAcross);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.AI.OffMeshLinkType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineAIOffMeshLinkType(L, (UnityEngine.AI.OffMeshLinkType)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "LinkTypeManual"))
                {
                    translator.PushUnityEngineAIOffMeshLinkType(L, UnityEngine.AI.OffMeshLinkType.LinkTypeManual);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LinkTypeDropDown"))
                {
                    translator.PushUnityEngineAIOffMeshLinkType(L, UnityEngine.AI.OffMeshLinkType.LinkTypeDropDown);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LinkTypeJumpAcross"))
                {
                    translator.PushUnityEngineAIOffMeshLinkType(L, UnityEngine.AI.OffMeshLinkType.LinkTypeJumpAcross);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.AI.OffMeshLinkType!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.AI.OffMeshLinkType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineSceneManagementLoadSceneModeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.SceneManagement.LoadSceneMode), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.SceneManagement.LoadSceneMode), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.SceneManagement.LoadSceneMode), L, null, 3, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Single", UnityEngine.SceneManagement.LoadSceneMode.Single);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Additive", UnityEngine.SceneManagement.LoadSceneMode.Additive);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.SceneManagement.LoadSceneMode), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineSceneManagementLoadSceneMode(L, (UnityEngine.SceneManagement.LoadSceneMode)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Single"))
                {
                    translator.PushUnityEngineSceneManagementLoadSceneMode(L, UnityEngine.SceneManagement.LoadSceneMode.Single);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Additive"))
                {
                    translator.PushUnityEngineSceneManagementLoadSceneMode(L, UnityEngine.SceneManagement.LoadSceneMode.Additive);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.SceneManagement.LoadSceneMode!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.SceneManagement.LoadSceneMode! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class DGTweeningRotateModeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(DG.Tweening.RotateMode), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(DG.Tweening.RotateMode), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(DG.Tweening.RotateMode), L, null, 5, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Fast", DG.Tweening.RotateMode.Fast);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "FastBeyond360", DG.Tweening.RotateMode.FastBeyond360);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "WorldAxisAdd", DG.Tweening.RotateMode.WorldAxisAdd);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LocalAxisAdd", DG.Tweening.RotateMode.LocalAxisAdd);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(DG.Tweening.RotateMode), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushDGTweeningRotateMode(L, (DG.Tweening.RotateMode)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Fast"))
                {
                    translator.PushDGTweeningRotateMode(L, DG.Tweening.RotateMode.Fast);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "FastBeyond360"))
                {
                    translator.PushDGTweeningRotateMode(L, DG.Tweening.RotateMode.FastBeyond360);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "WorldAxisAdd"))
                {
                    translator.PushDGTweeningRotateMode(L, DG.Tweening.RotateMode.WorldAxisAdd);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LocalAxisAdd"))
                {
                    translator.PushDGTweeningRotateMode(L, DG.Tweening.RotateMode.LocalAxisAdd);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for DG.Tweening.RotateMode!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for DG.Tweening.RotateMode! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineSpaceWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.Space), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.Space), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.Space), L, null, 3, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "World", UnityEngine.Space.World);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Self", UnityEngine.Space.Self);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.Space), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineSpace(L, (UnityEngine.Space)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "World"))
                {
                    translator.PushUnityEngineSpace(L, UnityEngine.Space.World);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Self"))
                {
                    translator.PushUnityEngineSpace(L, UnityEngine.Space.Self);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.Space!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.Space! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class TutorialTestEnumWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(Tutorial.TestEnum), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(Tutorial.TestEnum), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(Tutorial.TestEnum), L, null, 3, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "E1", Tutorial.TestEnum.E1);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "E2", Tutorial.TestEnum.E2);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(Tutorial.TestEnum), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushTutorialTestEnum(L, (Tutorial.TestEnum)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "E1"))
                {
                    translator.PushTutorialTestEnum(L, Tutorial.TestEnum.E1);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "E2"))
                {
                    translator.PushTutorialTestEnum(L, Tutorial.TestEnum.E2);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for Tutorial.TestEnum!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for Tutorial.TestEnum! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class XLuaTestMyEnumWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(XLuaTest.MyEnum), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(XLuaTest.MyEnum), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(XLuaTest.MyEnum), L, null, 3, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "E1", XLuaTest.MyEnum.E1);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "E2", XLuaTest.MyEnum.E2);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(XLuaTest.MyEnum), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushXLuaTestMyEnum(L, (XLuaTest.MyEnum)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "E1"))
                {
                    translator.PushXLuaTestMyEnum(L, XLuaTest.MyEnum.E1);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "E2"))
                {
                    translator.PushXLuaTestMyEnum(L, XLuaTest.MyEnum.E2);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for XLuaTest.MyEnum!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for XLuaTest.MyEnum! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class TutorialDerivedClassTestEnumInnerWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(Tutorial.DerivedClass.TestEnumInner), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(Tutorial.DerivedClass.TestEnumInner), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(Tutorial.DerivedClass.TestEnumInner), L, null, 3, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "E3", Tutorial.DerivedClass.TestEnumInner.E3);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "E4", Tutorial.DerivedClass.TestEnumInner.E4);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(Tutorial.DerivedClass.TestEnumInner), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushTutorialDerivedClassTestEnumInner(L, (Tutorial.DerivedClass.TestEnumInner)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "E3"))
                {
                    translator.PushTutorialDerivedClassTestEnumInner(L, Tutorial.DerivedClass.TestEnumInner.E3);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "E4"))
                {
                    translator.PushTutorialDerivedClassTestEnumInner(L, Tutorial.DerivedClass.TestEnumInner.E4);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for Tutorial.DerivedClass.TestEnumInner!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for Tutorial.DerivedClass.TestEnumInner! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
}