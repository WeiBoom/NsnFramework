namespace Nsn.Example
{
    public class NavigateInt2
    {
        public int x;
        public int y;

        public NavigateInt2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString() => $"X : {x}, Y : {y}";

        /*
        如果要把引用类型做为Dictionary或HashTable的key使用时，必须重写这两个方法。
        原因：当我们把引用类型(string除外)做为Dictionary或HashTable的key时，
        有可能永远无法根据Key获得value的值，或者说两个类型的HashCode永远不会相等。
        就拿Dictionary来说，虽然我们存储的时候是键值对，
        但是CLR会先把key转成HashCode并且验证Equals后再做存储，
        根据key取值的时候也是把key转换成HashCode并且验证Equals后再取值，
        一定要注意验证时HashCode和Equals的关系是并且(&&)的关系。也就是说，
        只要GetHashCode和Equlas中有一个方法没有重写，在验证时没有重写的那个方法会调用基类的默认实现，
        而这两个方法的默认实现都是根据内存地址判断的，也就是说，其实一个方法的返回值永远会是false。
        其结果就是，存储的时候你可能任性的存，在取值的时候就是你哭着找不着娘了。
         */
        public override int GetHashCode() => x ^ (y * 256);

        public static bool operator ==(NavigateInt2 a, NavigateInt2 b) => a.Equals(b);
        public static bool operator !=(NavigateInt2 a, NavigateInt2 b) => !a.Equals(b);
        
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != typeof(NavigateInt2))
                return false;
            NavigateInt2 navigateInt2 = (NavigateInt2)obj;
            return x == navigateInt2.x && y == navigateInt2.y;
        }
    }
    
    
}