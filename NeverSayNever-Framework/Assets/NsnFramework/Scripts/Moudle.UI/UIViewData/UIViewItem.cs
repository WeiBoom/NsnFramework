namespace Nsn
{
    public struct UIViewItem
    {
        public string ViewName;
        public int ViewID;
        public UIView View;

        public UIViewItem(UIView view,string name, int id)
        {
            View = view;
            ViewName = name;
            ViewID = id;
        }

        public bool IsValid()
        {
            if (View == null || string.IsNullOrEmpty(ViewName) || ViewID == 0)
                return false;
            return true;
        }

        public static UIViewItem Empty => default(UIViewItem);
    }
}