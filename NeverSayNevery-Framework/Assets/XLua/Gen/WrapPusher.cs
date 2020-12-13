﻿#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using System;


namespace XLua
{
    public partial class ObjectTranslator
    {
        
        class IniterAdderUnityEngineVector2
        {
            static IniterAdderUnityEngineVector2()
            {
                LuaEnv.AddIniter(Init);
            }
			
			static void Init(LuaEnv luaenv, ObjectTranslator translator)
			{
			
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Vector2>(translator.PushUnityEngineVector2, translator.Get, translator.UpdateUnityEngineVector2);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Vector3>(translator.PushUnityEngineVector3, translator.Get, translator.UpdateUnityEngineVector3);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Vector4>(translator.PushUnityEngineVector4, translator.Get, translator.UpdateUnityEngineVector4);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Color>(translator.PushUnityEngineColor, translator.Get, translator.UpdateUnityEngineColor);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Quaternion>(translator.PushUnityEngineQuaternion, translator.Get, translator.UpdateUnityEngineQuaternion);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Ray>(translator.PushUnityEngineRay, translator.Get, translator.UpdateUnityEngineRay);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Bounds>(translator.PushUnityEngineBounds, translator.Get, translator.UpdateUnityEngineBounds);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Ray2D>(translator.PushUnityEngineRay2D, translator.Get, translator.UpdateUnityEngineRay2D);
				translator.RegisterPushAndGetAndUpdate<XLuaTest.Pedding>(translator.PushXLuaTestPedding, translator.Get, translator.UpdateXLuaTestPedding);
				translator.RegisterPushAndGetAndUpdate<XLuaTest.MyStruct>(translator.PushXLuaTestMyStruct, translator.Get, translator.UpdateXLuaTestMyStruct);
				translator.RegisterPushAndGetAndUpdate<XLuaTest.PushAsTableStruct>(translator.PushXLuaTestPushAsTableStruct, translator.Get, translator.UpdateXLuaTestPushAsTableStruct);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.RuntimePlatform>(translator.PushUnityEngineRuntimePlatform, translator.Get, translator.UpdateUnityEngineRuntimePlatform);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.PrimitiveType>(translator.PushUnityEnginePrimitiveType, translator.Get, translator.UpdateUnityEnginePrimitiveType);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.WrapMode>(translator.PushUnityEngineWrapMode, translator.Get, translator.UpdateUnityEngineWrapMode);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.AI.NavMeshPathStatus>(translator.PushUnityEngineAINavMeshPathStatus, translator.Get, translator.UpdateUnityEngineAINavMeshPathStatus);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.AI.ObstacleAvoidanceType>(translator.PushUnityEngineAIObstacleAvoidanceType, translator.Get, translator.UpdateUnityEngineAIObstacleAvoidanceType);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.AI.NavMeshObstacleShape>(translator.PushUnityEngineAINavMeshObstacleShape, translator.Get, translator.UpdateUnityEngineAINavMeshObstacleShape);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.AI.OffMeshLinkType>(translator.PushUnityEngineAIOffMeshLinkType, translator.Get, translator.UpdateUnityEngineAIOffMeshLinkType);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.SceneManagement.LoadSceneMode>(translator.PushUnityEngineSceneManagementLoadSceneMode, translator.Get, translator.UpdateUnityEngineSceneManagementLoadSceneMode);
				translator.RegisterPushAndGetAndUpdate<DG.Tweening.RotateMode>(translator.PushDGTweeningRotateMode, translator.Get, translator.UpdateDGTweeningRotateMode);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Space>(translator.PushUnityEngineSpace, translator.Get, translator.UpdateUnityEngineSpace);
				translator.RegisterPushAndGetAndUpdate<Tutorial.TestEnum>(translator.PushTutorialTestEnum, translator.Get, translator.UpdateTutorialTestEnum);
				translator.RegisterPushAndGetAndUpdate<XLuaTest.MyEnum>(translator.PushXLuaTestMyEnum, translator.Get, translator.UpdateXLuaTestMyEnum);
				translator.RegisterPushAndGetAndUpdate<Tutorial.DerivedClass.TestEnumInner>(translator.PushTutorialDerivedClassTestEnumInner, translator.Get, translator.UpdateTutorialDerivedClassTestEnumInner);
			
			}
        }
        
        static IniterAdderUnityEngineVector2 s_IniterAdderUnityEngineVector2_dumb_obj = new IniterAdderUnityEngineVector2();
        static IniterAdderUnityEngineVector2 IniterAdderUnityEngineVector2_dumb_obj {get{return s_IniterAdderUnityEngineVector2_dumb_obj;}}
        
        
        int UnityEngineVector2_TypeID = -1;
        public void PushUnityEngineVector2(RealStatePtr L, UnityEngine.Vector2 val)
        {
            if (UnityEngineVector2_TypeID == -1)
            {
			    bool is_first;
                UnityEngineVector2_TypeID = getTypeId(L, typeof(UnityEngine.Vector2), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 8, UnityEngineVector2_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Vector2 ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Vector2 val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector2_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector2");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Vector2");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Vector2)objectCasters.GetCaster(typeof(UnityEngine.Vector2))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineVector2(RealStatePtr L, int index, UnityEngine.Vector2 val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector2_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector2");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Vector2 ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineVector3_TypeID = -1;
        public void PushUnityEngineVector3(RealStatePtr L, UnityEngine.Vector3 val)
        {
            if (UnityEngineVector3_TypeID == -1)
            {
			    bool is_first;
                UnityEngineVector3_TypeID = getTypeId(L, typeof(UnityEngine.Vector3), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 12, UnityEngineVector3_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Vector3 ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Vector3 val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector3_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector3");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Vector3");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Vector3)objectCasters.GetCaster(typeof(UnityEngine.Vector3))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineVector3(RealStatePtr L, int index, UnityEngine.Vector3 val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector3_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector3");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Vector3 ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineVector4_TypeID = -1;
        public void PushUnityEngineVector4(RealStatePtr L, UnityEngine.Vector4 val)
        {
            if (UnityEngineVector4_TypeID == -1)
            {
			    bool is_first;
                UnityEngineVector4_TypeID = getTypeId(L, typeof(UnityEngine.Vector4), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 16, UnityEngineVector4_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Vector4 ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Vector4 val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector4_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector4");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Vector4");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Vector4)objectCasters.GetCaster(typeof(UnityEngine.Vector4))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineVector4(RealStatePtr L, int index, UnityEngine.Vector4 val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector4_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector4");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Vector4 ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineColor_TypeID = -1;
        public void PushUnityEngineColor(RealStatePtr L, UnityEngine.Color val)
        {
            if (UnityEngineColor_TypeID == -1)
            {
			    bool is_first;
                UnityEngineColor_TypeID = getTypeId(L, typeof(UnityEngine.Color), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 16, UnityEngineColor_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Color ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Color val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineColor_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Color");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Color");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Color)objectCasters.GetCaster(typeof(UnityEngine.Color))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineColor(RealStatePtr L, int index, UnityEngine.Color val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineColor_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Color");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Color ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineQuaternion_TypeID = -1;
        public void PushUnityEngineQuaternion(RealStatePtr L, UnityEngine.Quaternion val)
        {
            if (UnityEngineQuaternion_TypeID == -1)
            {
			    bool is_first;
                UnityEngineQuaternion_TypeID = getTypeId(L, typeof(UnityEngine.Quaternion), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 16, UnityEngineQuaternion_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Quaternion ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Quaternion val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineQuaternion_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Quaternion");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Quaternion");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Quaternion)objectCasters.GetCaster(typeof(UnityEngine.Quaternion))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineQuaternion(RealStatePtr L, int index, UnityEngine.Quaternion val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineQuaternion_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Quaternion");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Quaternion ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineRay_TypeID = -1;
        public void PushUnityEngineRay(RealStatePtr L, UnityEngine.Ray val)
        {
            if (UnityEngineRay_TypeID == -1)
            {
			    bool is_first;
                UnityEngineRay_TypeID = getTypeId(L, typeof(UnityEngine.Ray), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 24, UnityEngineRay_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Ray ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Ray val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRay_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Ray");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Ray");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Ray)objectCasters.GetCaster(typeof(UnityEngine.Ray))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineRay(RealStatePtr L, int index, UnityEngine.Ray val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRay_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Ray");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Ray ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineBounds_TypeID = -1;
        public void PushUnityEngineBounds(RealStatePtr L, UnityEngine.Bounds val)
        {
            if (UnityEngineBounds_TypeID == -1)
            {
			    bool is_first;
                UnityEngineBounds_TypeID = getTypeId(L, typeof(UnityEngine.Bounds), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 24, UnityEngineBounds_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Bounds ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Bounds val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineBounds_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Bounds");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Bounds");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Bounds)objectCasters.GetCaster(typeof(UnityEngine.Bounds))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineBounds(RealStatePtr L, int index, UnityEngine.Bounds val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineBounds_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Bounds");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Bounds ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineRay2D_TypeID = -1;
        public void PushUnityEngineRay2D(RealStatePtr L, UnityEngine.Ray2D val)
        {
            if (UnityEngineRay2D_TypeID == -1)
            {
			    bool is_first;
                UnityEngineRay2D_TypeID = getTypeId(L, typeof(UnityEngine.Ray2D), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 16, UnityEngineRay2D_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Ray2D ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Ray2D val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRay2D_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Ray2D");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Ray2D");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Ray2D)objectCasters.GetCaster(typeof(UnityEngine.Ray2D))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineRay2D(RealStatePtr L, int index, UnityEngine.Ray2D val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRay2D_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Ray2D");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Ray2D ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int XLuaTestPedding_TypeID = -1;
        public void PushXLuaTestPedding(RealStatePtr L, XLuaTest.Pedding val)
        {
            if (XLuaTestPedding_TypeID == -1)
            {
			    bool is_first;
                XLuaTestPedding_TypeID = getTypeId(L, typeof(XLuaTest.Pedding), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 1, XLuaTestPedding_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for XLuaTest.Pedding ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out XLuaTest.Pedding val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != XLuaTestPedding_TypeID)
				{
				    throw new Exception("invalid userdata for XLuaTest.Pedding");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for XLuaTest.Pedding");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (XLuaTest.Pedding)objectCasters.GetCaster(typeof(XLuaTest.Pedding))(L, index, null);
            }
        }
		
        public void UpdateXLuaTestPedding(RealStatePtr L, int index, XLuaTest.Pedding val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != XLuaTestPedding_TypeID)
				{
				    throw new Exception("invalid userdata for XLuaTest.Pedding");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for XLuaTest.Pedding ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int XLuaTestMyStruct_TypeID = -1;
        public void PushXLuaTestMyStruct(RealStatePtr L, XLuaTest.MyStruct val)
        {
            if (XLuaTestMyStruct_TypeID == -1)
            {
			    bool is_first;
                XLuaTestMyStruct_TypeID = getTypeId(L, typeof(XLuaTest.MyStruct), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 25, XLuaTestMyStruct_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for XLuaTest.MyStruct ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out XLuaTest.MyStruct val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != XLuaTestMyStruct_TypeID)
				{
				    throw new Exception("invalid userdata for XLuaTest.MyStruct");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for XLuaTest.MyStruct");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (XLuaTest.MyStruct)objectCasters.GetCaster(typeof(XLuaTest.MyStruct))(L, index, null);
            }
        }
		
        public void UpdateXLuaTestMyStruct(RealStatePtr L, int index, XLuaTest.MyStruct val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != XLuaTestMyStruct_TypeID)
				{
				    throw new Exception("invalid userdata for XLuaTest.MyStruct");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for XLuaTest.MyStruct ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int XLuaTestPushAsTableStruct_TypeID = -1;
        public void PushXLuaTestPushAsTableStruct(RealStatePtr L, XLuaTest.PushAsTableStruct val)
        {
            if (XLuaTestPushAsTableStruct_TypeID == -1)
            {
			    bool is_first;
                XLuaTestPushAsTableStruct_TypeID = getTypeId(L, typeof(XLuaTest.PushAsTableStruct), out is_first);
				
            }
			
			
			LuaAPI.xlua_pushcstable(L, 2, XLuaTestPushAsTableStruct_TypeID);
			
			LuaAPI.xlua_pushasciistring(L, "x");
			LuaAPI.xlua_pushinteger(L, val.x);
			LuaAPI.lua_rawset(L, -3);
			
			LuaAPI.xlua_pushasciistring(L, "y");
			LuaAPI.xlua_pushinteger(L, val.y);
			LuaAPI.lua_rawset(L, -3);
			
			
        }
		
        public void Get(RealStatePtr L, int index, out XLuaTest.PushAsTableStruct val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != XLuaTestPushAsTableStruct_TypeID)
				{
				    throw new Exception("invalid userdata for XLuaTest.PushAsTableStruct");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for XLuaTest.PushAsTableStruct");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (XLuaTest.PushAsTableStruct)objectCasters.GetCaster(typeof(XLuaTest.PushAsTableStruct))(L, index, null);
            }
        }
		
        public void UpdateXLuaTestPushAsTableStruct(RealStatePtr L, int index, XLuaTest.PushAsTableStruct val)
        {
		    
			if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TTABLE)
            {
			    return;
			}
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineRuntimePlatform_TypeID = -1;
		int UnityEngineRuntimePlatform_EnumRef = -1;
        
        public void PushUnityEngineRuntimePlatform(RealStatePtr L, UnityEngine.RuntimePlatform val)
        {
            if (UnityEngineRuntimePlatform_TypeID == -1)
            {
			    bool is_first;
                UnityEngineRuntimePlatform_TypeID = getTypeId(L, typeof(UnityEngine.RuntimePlatform), out is_first);
				
				if (UnityEngineRuntimePlatform_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.RuntimePlatform));
				    UnityEngineRuntimePlatform_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineRuntimePlatform_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineRuntimePlatform_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.RuntimePlatform ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineRuntimePlatform_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.RuntimePlatform val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRuntimePlatform_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.RuntimePlatform");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.RuntimePlatform");
                }
				val = (UnityEngine.RuntimePlatform)e;
                
            }
            else
            {
                val = (UnityEngine.RuntimePlatform)objectCasters.GetCaster(typeof(UnityEngine.RuntimePlatform))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineRuntimePlatform(RealStatePtr L, int index, UnityEngine.RuntimePlatform val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRuntimePlatform_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.RuntimePlatform");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.RuntimePlatform ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEnginePrimitiveType_TypeID = -1;
		int UnityEnginePrimitiveType_EnumRef = -1;
        
        public void PushUnityEnginePrimitiveType(RealStatePtr L, UnityEngine.PrimitiveType val)
        {
            if (UnityEnginePrimitiveType_TypeID == -1)
            {
			    bool is_first;
                UnityEnginePrimitiveType_TypeID = getTypeId(L, typeof(UnityEngine.PrimitiveType), out is_first);
				
				if (UnityEnginePrimitiveType_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.PrimitiveType));
				    UnityEnginePrimitiveType_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEnginePrimitiveType_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEnginePrimitiveType_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.PrimitiveType ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEnginePrimitiveType_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.PrimitiveType val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEnginePrimitiveType_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.PrimitiveType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.PrimitiveType");
                }
				val = (UnityEngine.PrimitiveType)e;
                
            }
            else
            {
                val = (UnityEngine.PrimitiveType)objectCasters.GetCaster(typeof(UnityEngine.PrimitiveType))(L, index, null);
            }
        }
		
        public void UpdateUnityEnginePrimitiveType(RealStatePtr L, int index, UnityEngine.PrimitiveType val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEnginePrimitiveType_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.PrimitiveType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.PrimitiveType ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineWrapMode_TypeID = -1;
		int UnityEngineWrapMode_EnumRef = -1;
        
        public void PushUnityEngineWrapMode(RealStatePtr L, UnityEngine.WrapMode val)
        {
            if (UnityEngineWrapMode_TypeID == -1)
            {
			    bool is_first;
                UnityEngineWrapMode_TypeID = getTypeId(L, typeof(UnityEngine.WrapMode), out is_first);
				
				if (UnityEngineWrapMode_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.WrapMode));
				    UnityEngineWrapMode_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineWrapMode_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineWrapMode_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.WrapMode ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineWrapMode_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.WrapMode val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineWrapMode_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.WrapMode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.WrapMode");
                }
				val = (UnityEngine.WrapMode)e;
                
            }
            else
            {
                val = (UnityEngine.WrapMode)objectCasters.GetCaster(typeof(UnityEngine.WrapMode))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineWrapMode(RealStatePtr L, int index, UnityEngine.WrapMode val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineWrapMode_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.WrapMode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.WrapMode ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineAINavMeshPathStatus_TypeID = -1;
		int UnityEngineAINavMeshPathStatus_EnumRef = -1;
        
        public void PushUnityEngineAINavMeshPathStatus(RealStatePtr L, UnityEngine.AI.NavMeshPathStatus val)
        {
            if (UnityEngineAINavMeshPathStatus_TypeID == -1)
            {
			    bool is_first;
                UnityEngineAINavMeshPathStatus_TypeID = getTypeId(L, typeof(UnityEngine.AI.NavMeshPathStatus), out is_first);
				
				if (UnityEngineAINavMeshPathStatus_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.AI.NavMeshPathStatus));
				    UnityEngineAINavMeshPathStatus_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineAINavMeshPathStatus_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineAINavMeshPathStatus_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.AI.NavMeshPathStatus ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineAINavMeshPathStatus_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.AI.NavMeshPathStatus val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineAINavMeshPathStatus_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.AI.NavMeshPathStatus");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.AI.NavMeshPathStatus");
                }
				val = (UnityEngine.AI.NavMeshPathStatus)e;
                
            }
            else
            {
                val = (UnityEngine.AI.NavMeshPathStatus)objectCasters.GetCaster(typeof(UnityEngine.AI.NavMeshPathStatus))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineAINavMeshPathStatus(RealStatePtr L, int index, UnityEngine.AI.NavMeshPathStatus val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineAINavMeshPathStatus_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.AI.NavMeshPathStatus");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.AI.NavMeshPathStatus ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineAIObstacleAvoidanceType_TypeID = -1;
		int UnityEngineAIObstacleAvoidanceType_EnumRef = -1;
        
        public void PushUnityEngineAIObstacleAvoidanceType(RealStatePtr L, UnityEngine.AI.ObstacleAvoidanceType val)
        {
            if (UnityEngineAIObstacleAvoidanceType_TypeID == -1)
            {
			    bool is_first;
                UnityEngineAIObstacleAvoidanceType_TypeID = getTypeId(L, typeof(UnityEngine.AI.ObstacleAvoidanceType), out is_first);
				
				if (UnityEngineAIObstacleAvoidanceType_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.AI.ObstacleAvoidanceType));
				    UnityEngineAIObstacleAvoidanceType_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineAIObstacleAvoidanceType_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineAIObstacleAvoidanceType_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.AI.ObstacleAvoidanceType ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineAIObstacleAvoidanceType_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.AI.ObstacleAvoidanceType val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineAIObstacleAvoidanceType_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.AI.ObstacleAvoidanceType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.AI.ObstacleAvoidanceType");
                }
				val = (UnityEngine.AI.ObstacleAvoidanceType)e;
                
            }
            else
            {
                val = (UnityEngine.AI.ObstacleAvoidanceType)objectCasters.GetCaster(typeof(UnityEngine.AI.ObstacleAvoidanceType))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineAIObstacleAvoidanceType(RealStatePtr L, int index, UnityEngine.AI.ObstacleAvoidanceType val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineAIObstacleAvoidanceType_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.AI.ObstacleAvoidanceType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.AI.ObstacleAvoidanceType ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineAINavMeshObstacleShape_TypeID = -1;
		int UnityEngineAINavMeshObstacleShape_EnumRef = -1;
        
        public void PushUnityEngineAINavMeshObstacleShape(RealStatePtr L, UnityEngine.AI.NavMeshObstacleShape val)
        {
            if (UnityEngineAINavMeshObstacleShape_TypeID == -1)
            {
			    bool is_first;
                UnityEngineAINavMeshObstacleShape_TypeID = getTypeId(L, typeof(UnityEngine.AI.NavMeshObstacleShape), out is_first);
				
				if (UnityEngineAINavMeshObstacleShape_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.AI.NavMeshObstacleShape));
				    UnityEngineAINavMeshObstacleShape_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineAINavMeshObstacleShape_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineAINavMeshObstacleShape_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.AI.NavMeshObstacleShape ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineAINavMeshObstacleShape_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.AI.NavMeshObstacleShape val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineAINavMeshObstacleShape_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.AI.NavMeshObstacleShape");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.AI.NavMeshObstacleShape");
                }
				val = (UnityEngine.AI.NavMeshObstacleShape)e;
                
            }
            else
            {
                val = (UnityEngine.AI.NavMeshObstacleShape)objectCasters.GetCaster(typeof(UnityEngine.AI.NavMeshObstacleShape))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineAINavMeshObstacleShape(RealStatePtr L, int index, UnityEngine.AI.NavMeshObstacleShape val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineAINavMeshObstacleShape_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.AI.NavMeshObstacleShape");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.AI.NavMeshObstacleShape ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineAIOffMeshLinkType_TypeID = -1;
		int UnityEngineAIOffMeshLinkType_EnumRef = -1;
        
        public void PushUnityEngineAIOffMeshLinkType(RealStatePtr L, UnityEngine.AI.OffMeshLinkType val)
        {
            if (UnityEngineAIOffMeshLinkType_TypeID == -1)
            {
			    bool is_first;
                UnityEngineAIOffMeshLinkType_TypeID = getTypeId(L, typeof(UnityEngine.AI.OffMeshLinkType), out is_first);
				
				if (UnityEngineAIOffMeshLinkType_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.AI.OffMeshLinkType));
				    UnityEngineAIOffMeshLinkType_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineAIOffMeshLinkType_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineAIOffMeshLinkType_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.AI.OffMeshLinkType ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineAIOffMeshLinkType_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.AI.OffMeshLinkType val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineAIOffMeshLinkType_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.AI.OffMeshLinkType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.AI.OffMeshLinkType");
                }
				val = (UnityEngine.AI.OffMeshLinkType)e;
                
            }
            else
            {
                val = (UnityEngine.AI.OffMeshLinkType)objectCasters.GetCaster(typeof(UnityEngine.AI.OffMeshLinkType))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineAIOffMeshLinkType(RealStatePtr L, int index, UnityEngine.AI.OffMeshLinkType val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineAIOffMeshLinkType_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.AI.OffMeshLinkType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.AI.OffMeshLinkType ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineSceneManagementLoadSceneMode_TypeID = -1;
		int UnityEngineSceneManagementLoadSceneMode_EnumRef = -1;
        
        public void PushUnityEngineSceneManagementLoadSceneMode(RealStatePtr L, UnityEngine.SceneManagement.LoadSceneMode val)
        {
            if (UnityEngineSceneManagementLoadSceneMode_TypeID == -1)
            {
			    bool is_first;
                UnityEngineSceneManagementLoadSceneMode_TypeID = getTypeId(L, typeof(UnityEngine.SceneManagement.LoadSceneMode), out is_first);
				
				if (UnityEngineSceneManagementLoadSceneMode_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.SceneManagement.LoadSceneMode));
				    UnityEngineSceneManagementLoadSceneMode_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineSceneManagementLoadSceneMode_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineSceneManagementLoadSceneMode_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.SceneManagement.LoadSceneMode ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineSceneManagementLoadSceneMode_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.SceneManagement.LoadSceneMode val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineSceneManagementLoadSceneMode_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.SceneManagement.LoadSceneMode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.SceneManagement.LoadSceneMode");
                }
				val = (UnityEngine.SceneManagement.LoadSceneMode)e;
                
            }
            else
            {
                val = (UnityEngine.SceneManagement.LoadSceneMode)objectCasters.GetCaster(typeof(UnityEngine.SceneManagement.LoadSceneMode))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineSceneManagementLoadSceneMode(RealStatePtr L, int index, UnityEngine.SceneManagement.LoadSceneMode val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineSceneManagementLoadSceneMode_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.SceneManagement.LoadSceneMode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.SceneManagement.LoadSceneMode ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int DGTweeningRotateMode_TypeID = -1;
		int DGTweeningRotateMode_EnumRef = -1;
        
        public void PushDGTweeningRotateMode(RealStatePtr L, DG.Tweening.RotateMode val)
        {
            if (DGTweeningRotateMode_TypeID == -1)
            {
			    bool is_first;
                DGTweeningRotateMode_TypeID = getTypeId(L, typeof(DG.Tweening.RotateMode), out is_first);
				
				if (DGTweeningRotateMode_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(DG.Tweening.RotateMode));
				    DGTweeningRotateMode_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, DGTweeningRotateMode_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, DGTweeningRotateMode_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for DG.Tweening.RotateMode ,value="+val);
            }
			
			LuaAPI.lua_getref(L, DGTweeningRotateMode_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out DG.Tweening.RotateMode val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != DGTweeningRotateMode_TypeID)
				{
				    throw new Exception("invalid userdata for DG.Tweening.RotateMode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for DG.Tweening.RotateMode");
                }
				val = (DG.Tweening.RotateMode)e;
                
            }
            else
            {
                val = (DG.Tweening.RotateMode)objectCasters.GetCaster(typeof(DG.Tweening.RotateMode))(L, index, null);
            }
        }
		
        public void UpdateDGTweeningRotateMode(RealStatePtr L, int index, DG.Tweening.RotateMode val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != DGTweeningRotateMode_TypeID)
				{
				    throw new Exception("invalid userdata for DG.Tweening.RotateMode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for DG.Tweening.RotateMode ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineSpace_TypeID = -1;
		int UnityEngineSpace_EnumRef = -1;
        
        public void PushUnityEngineSpace(RealStatePtr L, UnityEngine.Space val)
        {
            if (UnityEngineSpace_TypeID == -1)
            {
			    bool is_first;
                UnityEngineSpace_TypeID = getTypeId(L, typeof(UnityEngine.Space), out is_first);
				
				if (UnityEngineSpace_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.Space));
				    UnityEngineSpace_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineSpace_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineSpace_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.Space ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineSpace_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Space val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineSpace_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Space");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.Space");
                }
				val = (UnityEngine.Space)e;
                
            }
            else
            {
                val = (UnityEngine.Space)objectCasters.GetCaster(typeof(UnityEngine.Space))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineSpace(RealStatePtr L, int index, UnityEngine.Space val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineSpace_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Space");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.Space ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int TutorialTestEnum_TypeID = -1;
		int TutorialTestEnum_EnumRef = -1;
        
        public void PushTutorialTestEnum(RealStatePtr L, Tutorial.TestEnum val)
        {
            if (TutorialTestEnum_TypeID == -1)
            {
			    bool is_first;
                TutorialTestEnum_TypeID = getTypeId(L, typeof(Tutorial.TestEnum), out is_first);
				
				if (TutorialTestEnum_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(Tutorial.TestEnum));
				    TutorialTestEnum_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, TutorialTestEnum_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, TutorialTestEnum_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for Tutorial.TestEnum ,value="+val);
            }
			
			LuaAPI.lua_getref(L, TutorialTestEnum_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out Tutorial.TestEnum val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != TutorialTestEnum_TypeID)
				{
				    throw new Exception("invalid userdata for Tutorial.TestEnum");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for Tutorial.TestEnum");
                }
				val = (Tutorial.TestEnum)e;
                
            }
            else
            {
                val = (Tutorial.TestEnum)objectCasters.GetCaster(typeof(Tutorial.TestEnum))(L, index, null);
            }
        }
		
        public void UpdateTutorialTestEnum(RealStatePtr L, int index, Tutorial.TestEnum val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != TutorialTestEnum_TypeID)
				{
				    throw new Exception("invalid userdata for Tutorial.TestEnum");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for Tutorial.TestEnum ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int XLuaTestMyEnum_TypeID = -1;
		int XLuaTestMyEnum_EnumRef = -1;
        
        public void PushXLuaTestMyEnum(RealStatePtr L, XLuaTest.MyEnum val)
        {
            if (XLuaTestMyEnum_TypeID == -1)
            {
			    bool is_first;
                XLuaTestMyEnum_TypeID = getTypeId(L, typeof(XLuaTest.MyEnum), out is_first);
				
				if (XLuaTestMyEnum_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(XLuaTest.MyEnum));
				    XLuaTestMyEnum_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, XLuaTestMyEnum_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, XLuaTestMyEnum_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for XLuaTest.MyEnum ,value="+val);
            }
			
			LuaAPI.lua_getref(L, XLuaTestMyEnum_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out XLuaTest.MyEnum val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != XLuaTestMyEnum_TypeID)
				{
				    throw new Exception("invalid userdata for XLuaTest.MyEnum");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for XLuaTest.MyEnum");
                }
				val = (XLuaTest.MyEnum)e;
                
            }
            else
            {
                val = (XLuaTest.MyEnum)objectCasters.GetCaster(typeof(XLuaTest.MyEnum))(L, index, null);
            }
        }
		
        public void UpdateXLuaTestMyEnum(RealStatePtr L, int index, XLuaTest.MyEnum val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != XLuaTestMyEnum_TypeID)
				{
				    throw new Exception("invalid userdata for XLuaTest.MyEnum");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for XLuaTest.MyEnum ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int TutorialDerivedClassTestEnumInner_TypeID = -1;
		int TutorialDerivedClassTestEnumInner_EnumRef = -1;
        
        public void PushTutorialDerivedClassTestEnumInner(RealStatePtr L, Tutorial.DerivedClass.TestEnumInner val)
        {
            if (TutorialDerivedClassTestEnumInner_TypeID == -1)
            {
			    bool is_first;
                TutorialDerivedClassTestEnumInner_TypeID = getTypeId(L, typeof(Tutorial.DerivedClass.TestEnumInner), out is_first);
				
				if (TutorialDerivedClassTestEnumInner_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(Tutorial.DerivedClass.TestEnumInner));
				    TutorialDerivedClassTestEnumInner_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, TutorialDerivedClassTestEnumInner_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, TutorialDerivedClassTestEnumInner_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for Tutorial.DerivedClass.TestEnumInner ,value="+val);
            }
			
			LuaAPI.lua_getref(L, TutorialDerivedClassTestEnumInner_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out Tutorial.DerivedClass.TestEnumInner val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != TutorialDerivedClassTestEnumInner_TypeID)
				{
				    throw new Exception("invalid userdata for Tutorial.DerivedClass.TestEnumInner");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for Tutorial.DerivedClass.TestEnumInner");
                }
				val = (Tutorial.DerivedClass.TestEnumInner)e;
                
            }
            else
            {
                val = (Tutorial.DerivedClass.TestEnumInner)objectCasters.GetCaster(typeof(Tutorial.DerivedClass.TestEnumInner))(L, index, null);
            }
        }
		
        public void UpdateTutorialDerivedClassTestEnumInner(RealStatePtr L, int index, Tutorial.DerivedClass.TestEnumInner val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != TutorialDerivedClassTestEnumInner_TypeID)
				{
				    throw new Exception("invalid userdata for Tutorial.DerivedClass.TestEnumInner");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for Tutorial.DerivedClass.TestEnumInner ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        
		// table cast optimze
		
        
    }
	
	public partial class StaticLuaCallbacks
    {
	    internal static bool __tryArrayGet(Type type, RealStatePtr L, ObjectTranslator translator, object obj, int index)
		{
		
			if (type == typeof(UnityEngine.Vector2[]))
			{
			    UnityEngine.Vector2[] array = obj as UnityEngine.Vector2[];
				translator.PushUnityEngineVector2(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Vector3[]))
			{
			    UnityEngine.Vector3[] array = obj as UnityEngine.Vector3[];
				translator.PushUnityEngineVector3(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Vector4[]))
			{
			    UnityEngine.Vector4[] array = obj as UnityEngine.Vector4[];
				translator.PushUnityEngineVector4(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Color[]))
			{
			    UnityEngine.Color[] array = obj as UnityEngine.Color[];
				translator.PushUnityEngineColor(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Quaternion[]))
			{
			    UnityEngine.Quaternion[] array = obj as UnityEngine.Quaternion[];
				translator.PushUnityEngineQuaternion(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Ray[]))
			{
			    UnityEngine.Ray[] array = obj as UnityEngine.Ray[];
				translator.PushUnityEngineRay(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Bounds[]))
			{
			    UnityEngine.Bounds[] array = obj as UnityEngine.Bounds[];
				translator.PushUnityEngineBounds(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Ray2D[]))
			{
			    UnityEngine.Ray2D[] array = obj as UnityEngine.Ray2D[];
				translator.PushUnityEngineRay2D(L, array[index]);
				return true;
			}
			else if (type == typeof(XLuaTest.Pedding[]))
			{
			    XLuaTest.Pedding[] array = obj as XLuaTest.Pedding[];
				translator.PushXLuaTestPedding(L, array[index]);
				return true;
			}
			else if (type == typeof(XLuaTest.MyStruct[]))
			{
			    XLuaTest.MyStruct[] array = obj as XLuaTest.MyStruct[];
				translator.PushXLuaTestMyStruct(L, array[index]);
				return true;
			}
			else if (type == typeof(XLuaTest.PushAsTableStruct[]))
			{
			    XLuaTest.PushAsTableStruct[] array = obj as XLuaTest.PushAsTableStruct[];
				translator.PushXLuaTestPushAsTableStruct(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.RuntimePlatform[]))
			{
			    UnityEngine.RuntimePlatform[] array = obj as UnityEngine.RuntimePlatform[];
				translator.PushUnityEngineRuntimePlatform(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.PrimitiveType[]))
			{
			    UnityEngine.PrimitiveType[] array = obj as UnityEngine.PrimitiveType[];
				translator.PushUnityEnginePrimitiveType(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.WrapMode[]))
			{
			    UnityEngine.WrapMode[] array = obj as UnityEngine.WrapMode[];
				translator.PushUnityEngineWrapMode(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.AI.NavMeshPathStatus[]))
			{
			    UnityEngine.AI.NavMeshPathStatus[] array = obj as UnityEngine.AI.NavMeshPathStatus[];
				translator.PushUnityEngineAINavMeshPathStatus(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.AI.ObstacleAvoidanceType[]))
			{
			    UnityEngine.AI.ObstacleAvoidanceType[] array = obj as UnityEngine.AI.ObstacleAvoidanceType[];
				translator.PushUnityEngineAIObstacleAvoidanceType(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.AI.NavMeshObstacleShape[]))
			{
			    UnityEngine.AI.NavMeshObstacleShape[] array = obj as UnityEngine.AI.NavMeshObstacleShape[];
				translator.PushUnityEngineAINavMeshObstacleShape(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.AI.OffMeshLinkType[]))
			{
			    UnityEngine.AI.OffMeshLinkType[] array = obj as UnityEngine.AI.OffMeshLinkType[];
				translator.PushUnityEngineAIOffMeshLinkType(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.SceneManagement.LoadSceneMode[]))
			{
			    UnityEngine.SceneManagement.LoadSceneMode[] array = obj as UnityEngine.SceneManagement.LoadSceneMode[];
				translator.PushUnityEngineSceneManagementLoadSceneMode(L, array[index]);
				return true;
			}
			else if (type == typeof(DG.Tweening.RotateMode[]))
			{
			    DG.Tweening.RotateMode[] array = obj as DG.Tweening.RotateMode[];
				translator.PushDGTweeningRotateMode(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Space[]))
			{
			    UnityEngine.Space[] array = obj as UnityEngine.Space[];
				translator.PushUnityEngineSpace(L, array[index]);
				return true;
			}
			else if (type == typeof(Tutorial.TestEnum[]))
			{
			    Tutorial.TestEnum[] array = obj as Tutorial.TestEnum[];
				translator.PushTutorialTestEnum(L, array[index]);
				return true;
			}
			else if (type == typeof(XLuaTest.MyEnum[]))
			{
			    XLuaTest.MyEnum[] array = obj as XLuaTest.MyEnum[];
				translator.PushXLuaTestMyEnum(L, array[index]);
				return true;
			}
			else if (type == typeof(Tutorial.DerivedClass.TestEnumInner[]))
			{
			    Tutorial.DerivedClass.TestEnumInner[] array = obj as Tutorial.DerivedClass.TestEnumInner[];
				translator.PushTutorialDerivedClassTestEnumInner(L, array[index]);
				return true;
			}
            return false;
		}
		
		internal static bool __tryArraySet(Type type, RealStatePtr L, ObjectTranslator translator, object obj, int array_idx, int obj_idx)
		{
		
			if (type == typeof(UnityEngine.Vector2[]))
			{
			    UnityEngine.Vector2[] array = obj as UnityEngine.Vector2[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Vector3[]))
			{
			    UnityEngine.Vector3[] array = obj as UnityEngine.Vector3[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Vector4[]))
			{
			    UnityEngine.Vector4[] array = obj as UnityEngine.Vector4[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Color[]))
			{
			    UnityEngine.Color[] array = obj as UnityEngine.Color[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Quaternion[]))
			{
			    UnityEngine.Quaternion[] array = obj as UnityEngine.Quaternion[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Ray[]))
			{
			    UnityEngine.Ray[] array = obj as UnityEngine.Ray[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Bounds[]))
			{
			    UnityEngine.Bounds[] array = obj as UnityEngine.Bounds[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Ray2D[]))
			{
			    UnityEngine.Ray2D[] array = obj as UnityEngine.Ray2D[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(XLuaTest.Pedding[]))
			{
			    XLuaTest.Pedding[] array = obj as XLuaTest.Pedding[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(XLuaTest.MyStruct[]))
			{
			    XLuaTest.MyStruct[] array = obj as XLuaTest.MyStruct[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(XLuaTest.PushAsTableStruct[]))
			{
			    XLuaTest.PushAsTableStruct[] array = obj as XLuaTest.PushAsTableStruct[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.RuntimePlatform[]))
			{
			    UnityEngine.RuntimePlatform[] array = obj as UnityEngine.RuntimePlatform[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.PrimitiveType[]))
			{
			    UnityEngine.PrimitiveType[] array = obj as UnityEngine.PrimitiveType[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.WrapMode[]))
			{
			    UnityEngine.WrapMode[] array = obj as UnityEngine.WrapMode[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.AI.NavMeshPathStatus[]))
			{
			    UnityEngine.AI.NavMeshPathStatus[] array = obj as UnityEngine.AI.NavMeshPathStatus[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.AI.ObstacleAvoidanceType[]))
			{
			    UnityEngine.AI.ObstacleAvoidanceType[] array = obj as UnityEngine.AI.ObstacleAvoidanceType[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.AI.NavMeshObstacleShape[]))
			{
			    UnityEngine.AI.NavMeshObstacleShape[] array = obj as UnityEngine.AI.NavMeshObstacleShape[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.AI.OffMeshLinkType[]))
			{
			    UnityEngine.AI.OffMeshLinkType[] array = obj as UnityEngine.AI.OffMeshLinkType[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.SceneManagement.LoadSceneMode[]))
			{
			    UnityEngine.SceneManagement.LoadSceneMode[] array = obj as UnityEngine.SceneManagement.LoadSceneMode[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(DG.Tweening.RotateMode[]))
			{
			    DG.Tweening.RotateMode[] array = obj as DG.Tweening.RotateMode[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Space[]))
			{
			    UnityEngine.Space[] array = obj as UnityEngine.Space[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(Tutorial.TestEnum[]))
			{
			    Tutorial.TestEnum[] array = obj as Tutorial.TestEnum[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(XLuaTest.MyEnum[]))
			{
			    XLuaTest.MyEnum[] array = obj as XLuaTest.MyEnum[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(Tutorial.DerivedClass.TestEnumInner[]))
			{
			    Tutorial.DerivedClass.TestEnumInner[] array = obj as Tutorial.DerivedClass.TestEnumInner[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
            return false;
		}
	}
}