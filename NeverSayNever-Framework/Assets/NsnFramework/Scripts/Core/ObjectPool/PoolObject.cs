using UnityEngine;

namespace NeverSayNever.Core
{
    public class PoolObject : GameBehaviour
    {
        public string poolName;
        
        public bool isPooled;
        // 是否被使用
        public bool IsUsed { get; private set; }
        // 从对象池中取出并使用
        public void Used() => IsUsed = true;
        // 放回对象池
        public void UnUsed() => IsUsed = false;
    }
}