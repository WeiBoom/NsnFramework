namespace NeverSayNever.NodeGraphView
{
    [System.Serializable]
    public class ExposedProperty
    {
        public static ExposedProperty CreateInstance() => new ExposedProperty();

        public string PropertyName = "New Name";
        public string PropertyValue = "New Value";
    }
}
