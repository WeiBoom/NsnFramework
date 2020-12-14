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
    public class NeverSayNeverCoreAssetResourceManagerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(NeverSayNever.Core.Asset.ResourceManager);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 7, 1, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "OnInitialize", _m_OnInitialize_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "OnUpdate", _m_OnUpdate_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LoadUIPanel", _m_LoadUIPanel_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LoadAudio", _m_LoadAudio_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LoadTextAsset", _m_LoadTextAsset_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReleaseObject", _m_ReleaseObject_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "LoadMode", _g_get_LoadMode);
            
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "NeverSayNever.Core.Asset.ResourceManager does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnInitialize_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    NeverSayNever.Core.Asset.EAssetLoadType _loadType;translator.Get(L, 1, out _loadType);
                    
                    NeverSayNever.Core.Asset.ResourceManager.OnInitialize( _loadType );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnUpdate_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    NeverSayNever.Core.Asset.ResourceManager.OnUpdate(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadUIPanel_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _panelName = LuaAPI.lua_tostring(L, 1);
                    System.Action<object> _callback = translator.GetDelegate<System.Action<object>>(L, 2);
                    
                    NeverSayNever.Core.Asset.ResourceManager.LoadUIPanel( _panelName, _callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadAudio_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _audioName = LuaAPI.lua_tostring(L, 1);
                    System.Action<object> _callback = translator.GetDelegate<System.Action<object>>(L, 2);
                    
                    NeverSayNever.Core.Asset.ResourceManager.LoadAudio( _audioName, _callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadTextAsset_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _textName = LuaAPI.lua_tostring(L, 1);
                    System.Action<object> _callback = translator.GetDelegate<System.Action<object>>(L, 2);
                    
                    NeverSayNever.Core.Asset.ResourceManager.LoadTextAsset( _textName, _callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReleaseObject_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    
                    NeverSayNever.Core.Asset.ResourceManager.ReleaseObject( _target );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LoadMode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, NeverSayNever.Core.Asset.ResourceManager.LoadMode);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
