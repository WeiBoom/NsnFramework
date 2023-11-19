using System.Collections.Generic;

namespace Nsn.MVC
{

    public partial class UIConfigs
    {
        private Dictionary<string, UIViewData> m_RegistedUIConfig;

        public void SetUp()
        {
            m_RegistedUIConfig = new Dictionary<string, UIViewData>(256);
        }
        
        public UIViewData GetConfig(string uiName)
        {
            m_RegistedUIConfig.TryGetValue(uiName, out var viewData);
            return viewData;
        }
    }
}