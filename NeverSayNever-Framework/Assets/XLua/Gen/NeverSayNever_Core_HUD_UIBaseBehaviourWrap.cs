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
    public class NeverSayNeverCoreHUDUIBaseBehaviourWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(NeverSayNever.Core.HUD.UIBaseBehaviour);
			Utils.BeginObjectRegister(type, L, translator, 0, 2, 6, 5);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InitCollectedUIComponents", _m_InitCollectedUIComponents);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetUICollection", _m_GetUICollection);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "uiScriptType", _g_get_uiScriptType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "fixedElements", _g_get_fixedElements);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "dynamicElements", _g_get_dynamicElements);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "isPanel", _g_get_isPanel);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "isLuaPanel", _g_get_isLuaPanel);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Collection", _g_get_Collection);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "uiScriptType", _s_set_uiScriptType);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "fixedElements", _s_set_fixedElements);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "dynamicElements", _s_set_dynamicElements);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "isPanel", _s_set_isPanel);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "isLuaPanel", _s_set_isLuaPanel);
            
			
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
					
					NeverSayNever.Core.HUD.UIBaseBehaviour gen_ret = new NeverSayNever.Core.HUD.UIBaseBehaviour();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to NeverSayNever.Core.HUD.UIBaseBehaviour constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InitCollectedUIComponents(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                NeverSayNever.Core.HUD.UIBaseBehaviour gen_to_be_invoked = (NeverSayNever.Core.HUD.UIBaseBehaviour)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.InitCollectedUIComponents(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetUICollection(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                NeverSayNever.Core.HUD.UIBaseBehaviour gen_to_be_invoked = (NeverSayNever.Core.HUD.UIBaseBehaviour)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        UnityEngine.Component gen_ret = gen_to_be_invoked.GetUICollection( _name );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uiScriptType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NeverSayNever.Core.HUD.UIBaseBehaviour gen_to_be_invoked = (NeverSayNever.Core.HUD.UIBaseBehaviour)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uiScriptType);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_fixedElements(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NeverSayNever.Core.HUD.UIBaseBehaviour gen_to_be_invoked = (NeverSayNever.Core.HUD.UIBaseBehaviour)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.fixedElements);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_dynamicElements(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NeverSayNever.Core.HUD.UIBaseBehaviour gen_to_be_invoked = (NeverSayNever.Core.HUD.UIBaseBehaviour)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.dynamicElements);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isPanel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NeverSayNever.Core.HUD.UIBaseBehaviour gen_to_be_invoked = (NeverSayNever.Core.HUD.UIBaseBehaviour)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.isPanel);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isLuaPanel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NeverSayNever.Core.HUD.UIBaseBehaviour gen_to_be_invoked = (NeverSayNever.Core.HUD.UIBaseBehaviour)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.isLuaPanel);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Collection(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NeverSayNever.Core.HUD.UIBaseBehaviour gen_to_be_invoked = (NeverSayNever.Core.HUD.UIBaseBehaviour)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Collection);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uiScriptType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NeverSayNever.Core.HUD.UIBaseBehaviour gen_to_be_invoked = (NeverSayNever.Core.HUD.UIBaseBehaviour)translator.FastGetCSObj(L, 1);
                NeverSayNever.Core.HUD.UIBaseBehaviour.EUIScriptType gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.uiScriptType = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_fixedElements(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NeverSayNever.Core.HUD.UIBaseBehaviour gen_to_be_invoked = (NeverSayNever.Core.HUD.UIBaseBehaviour)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.fixedElements = (System.Collections.Generic.List<NeverSayNever.Core.HUD.UIComponentItem>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<NeverSayNever.Core.HUD.UIComponentItem>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_dynamicElements(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NeverSayNever.Core.HUD.UIBaseBehaviour gen_to_be_invoked = (NeverSayNever.Core.HUD.UIBaseBehaviour)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.dynamicElements = (System.Collections.Generic.List<NeverSayNever.Core.HUD.UIComponentItem>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<NeverSayNever.Core.HUD.UIComponentItem>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_isPanel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NeverSayNever.Core.HUD.UIBaseBehaviour gen_to_be_invoked = (NeverSayNever.Core.HUD.UIBaseBehaviour)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.isPanel = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_isLuaPanel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NeverSayNever.Core.HUD.UIBaseBehaviour gen_to_be_invoked = (NeverSayNever.Core.HUD.UIBaseBehaviour)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.isLuaPanel = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
