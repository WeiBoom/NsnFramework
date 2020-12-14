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
    public class NeverSayNeverCoreUIManagerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(NeverSayNever.Core.UIManager);
			Utils.BeginObjectRegister(type, L, translator, 0, 7, 2, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnInitialize", _m_OnInitialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RegisterCsPanel", _m_RegisterCsPanel);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RegisterCsPanelByReflect", _m_RegisterCsPanelByReflect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RegisterLuaPanel", _m_RegisterLuaPanel);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OpenPanel", _m_OpenPanel);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HidePanel", _m_HidePanel);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ClosePanel", _m_ClosePanel);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Root", _g_get_Root);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PoolRoot", _g_get_PoolRoot);
            
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					NeverSayNever.Core.UIManager gen_ret = new NeverSayNever.Core.UIManager();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to NeverSayNever.Core.UIManager constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnInitialize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                NeverSayNever.Core.UIManager gen_to_be_invoked = (NeverSayNever.Core.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object[] _args = translator.GetParams<object>(L, 2);
                    
                    gen_to_be_invoked.OnInitialize( _args );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RegisterCsPanel(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                NeverSayNever.Core.UIManager gen_to_be_invoked = (NeverSayNever.Core.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _panelName = LuaAPI.lua_tostring(L, 2);
                    NeverSayNever.Core.HUD.UIPanelMessenger _panelMessenger = (NeverSayNever.Core.HUD.UIPanelMessenger)translator.GetObject(L, 3, typeof(NeverSayNever.Core.HUD.UIPanelMessenger));
                    
                    gen_to_be_invoked.RegisterCsPanel( _panelName, _panelMessenger );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RegisterCsPanelByReflect(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                NeverSayNever.Core.UIManager gen_to_be_invoked = (NeverSayNever.Core.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _moduleName = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.RegisterCsPanelByReflect( _moduleName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RegisterLuaPanel(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                NeverSayNever.Core.UIManager gen_to_be_invoked = (NeverSayNever.Core.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _panelName = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.RegisterLuaPanel( _panelName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenPanel(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                NeverSayNever.Core.UIManager gen_to_be_invoked = (NeverSayNever.Core.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _panelName = LuaAPI.lua_tostring(L, 2);
                    object[] _args = translator.GetParams<object>(L, 3);
                    
                    gen_to_be_invoked.OpenPanel( _panelName, _args );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HidePanel(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                NeverSayNever.Core.UIManager gen_to_be_invoked = (NeverSayNever.Core.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _panelName = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.HidePanel( _panelName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClosePanel(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                NeverSayNever.Core.UIManager gen_to_be_invoked = (NeverSayNever.Core.UIManager)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    string _panelName = LuaAPI.lua_tostring(L, 2);
                    bool _isPlayCloseAnim = LuaAPI.lua_toboolean(L, 3);
                    bool _putInPool = LuaAPI.lua_toboolean(L, 4);
                    
                    gen_to_be_invoked.ClosePanel( _panelName, _isPlayCloseAnim, _putInPool );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    string _panelName = LuaAPI.lua_tostring(L, 2);
                    bool _isPlayCloseAnim = LuaAPI.lua_toboolean(L, 3);
                    
                    gen_to_be_invoked.ClosePanel( _panelName, _isPlayCloseAnim );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _panelName = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.ClosePanel( _panelName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to NeverSayNever.Core.UIManager.ClosePanel!");
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Root(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NeverSayNever.Core.UIManager gen_to_be_invoked = (NeverSayNever.Core.UIManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Root);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PoolRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NeverSayNever.Core.UIManager gen_to_be_invoked = (NeverSayNever.Core.UIManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.PoolRoot);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
