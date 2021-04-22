using System;

namespace NeverSayNever.Core
{
    public abstract class GameModule : IGameModule
    {
        // 模块初始化
        public virtual void OnInitialize()
        {
            RegisterEvents();
        }

        // 模块更新
        public virtual void OnUpdate(float deltaTime)
        {
        }

        // 模块释放（销毁）
        public virtual void OnDispose()
        {
        }

        // 模块注册事件
        protected virtual void RegisterEvents()
        {
        }
    }
}
