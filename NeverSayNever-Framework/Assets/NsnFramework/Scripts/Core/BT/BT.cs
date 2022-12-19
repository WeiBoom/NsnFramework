/*
 * using from unity package Gamekit2D
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nsn
{
    public enum BTState
    {
        Failure,
        Success,
        Continue,
        Abort,
    }

    /// <summary>
    /// 行为树 BehaviourTree
    /// </summary>
    public static class BT
    {
        public static BTRoot Root() => new BTRoot();

        public static BTSequence Sequence() => new BTSequence();

        public static BTSelector Selector(bool shuffle = false) => new BTSelector(shuffle);

        public static BTAction RunCoroutine(System.Func<IEnumerator<BTState>> coroutine) => new BTAction(coroutine);

        public static BTAction Call(System.Action fn) => new BTAction(fn);

        public static BTConditionalBranch If(System.Func<bool> fn) => new BTConditionalBranch(fn);

        public static BTWhile While(System.Func<bool> fn) => new BTWhile(fn);

        public static BTCondition Condition(System.Func<bool> fn) => Condition(fn);

        public static BTRepeat Repeat(int count) => Repeat(count);

        public static BTWait Wait(float seconds) => Wait(seconds);

        public static BTTerminate Terminate() => Terminate();

        public static BTLog Log(string msg, params string[] args) => Log(msg, args);
    }

    /// <summary>
    /// 节点抽象类
    /// </summary>
    public abstract class BTNode
    {
        public abstract BTState Tick();
    }

    /// <summary>
    /// 行为树分支 base
    /// </summary>
    public abstract class BTBranch : BTNode
    {
        // 已经检查过的节点数
        protected int activeChild;
        public int ActiveChild => activeChild;
        // 所有的节点对象
        public List<BTNode> children  = new List<BTNode>();
        public List<BTNode> Children => children;

        public virtual BTBranch OpenBranch(params BTNode[] children)
        {
            for (var i = 0; i < children.Length; i++)
                this.children.Add(children[i]);
            return this;
        }

        public virtual void ResetChildren()
        {
            activeChild = 0;
            for (var i = 0; i < children.Count; i++)
            {
                BTBranch tb = children[i] as BTBranch;
                if (tb != null)
                {
                    tb.ResetChildren();
                }
            }
        }
    }

    /// <summary>
    /// 行为块基类，默认会执行所有的子节点内容。 
    /// </summary>
    public abstract class BTBlock : BTBranch
    {
        public override BTState Tick()
        {
            switch (children[activeChild].Tick())
            {
                case BTState.Continue:
                    return BTState.Continue;
                default:
                    activeChild++;
                    if (activeChild == children.Count)
                    {
                        activeChild = 0;
                        return BTState.Success;
                    }
                    return BTState.Continue;
            }
        }
    }

    /// <summary>
    /// 行为树根节点
    /// </summary>
    public class BTRoot : BTBlock
    {
        public bool IsTerminated = false;

        public override BTState Tick()
        {
            if (IsTerminated)
                return BTState.Abort;
            // 循环遍历每个树分支
            while(true)
            {
                switch (children[activeChild].Tick())
                {
                    case BTState.Continue:
                        return BTState.Continue;
                    case BTState.Abort:
                        IsTerminated = true;
                        return BTState.Abort;
                    default:
                        activeChild++;
                        if (activeChild == children.Count)
                        {
                            activeChild = 0;
                            return BTState.Success;
                        }
                        continue;
                }
            }
        }
    }


    /// <summary>
    /// 终端节点，不再执行后续内容
    /// </summary>
    public class BTTerminate : BTNode
    {
        public override BTState Tick()
        {
            return BTState.Abort;
        }
    }

    /// <summary>
    /// 日志节点，输出log日志信息
    /// </summary>
    public class BTLog : BTNode
    {
        string msg;
        string[] args;
        public BTLog(string msg, params string[] args)
        {
            this.msg = msg;
            this.args = args;
        }

        public override BTState Tick()
        {
            Nsn.NsnLog.Print(msg, args);
            return BTState.Success;
        }
    }

    /// <summary>
    /// 装饰节点 仅有一个子节点，作为辅助判断的一个节点，执行时间，执行条件，最大次数，最大时间，运行状态等。
    /// </summary>
    public abstract class BTDecorator : BTNode
    {
        protected BTNode child;
        public BTDecorator Do(BTNode child)
        {
            this.child = child;
            return this;
        }
    }
   
    /// <summary>
    /// 执行行为 节点
    /// </summary>
    public class BTAction : BTNode
    {
        // 执行一个方法
        System.Action func;
        // 或者执行一堆协程
        System.Func<IEnumerator<BTState>> coroutineFactory;
        IEnumerator<BTState> coroutine;

        public BTAction(System.Action func)
        {
            this.func = func;
        }

        public BTAction(System.Func<IEnumerator<BTState>> coroutineFactory)
        {
            this.coroutineFactory = coroutineFactory;
        }

        public override BTState Tick()
        {
            if (func != null)
            {
                func();
                return BTState.Success;
            }
            else
            {
                if (coroutine == null)
                    coroutine = coroutineFactory();
                // 如果协程队列已经执行完
                if (!coroutine.MoveNext())
                {
                    coroutine = null;
                    return BTState.Success;
                }
                var result = coroutine.Current;
                if (result != BTState.Continue)
                    coroutine = null;
                return result;
            }
        }
    }

    /// <summary>
    /// 条件节点
    /// </summary>
    public class BTCondition : BTNode
    {
        public System.Func<bool> func;

        public BTCondition(System.Func<bool> func)
        {
            this.func = func;
        }

        public override BTState Tick()
        {
            return func() ? BTState.Success : BTState.Failure;
        }
    }

    /// <summary>
    /// 顺序节点
    /// </summary>
    public class BTSequence : BTBranch
    {
        public override BTState Tick()
        {
            var childState = children[activeChild].Tick();
            switch (childState)
            {
                case BTState.Failure:
                    activeChild = 0;
                    return BTState.Failure;
                case BTState.Success:
                    activeChild++;
                    if (activeChild == children.Count)
                    {
                        activeChild = 0;
                        return BTState.Success;
                    }
                    return BTState.Continue;
                case BTState.Continue:
                    return BTState.Continue;
                case BTState.Abort:
                    activeChild = 0;
                    return BTState.Continue;
                default:
                    throw new System.Exception("something wrong.. check your trash code, please!");
            }
        }
    }

    /// <summary>
    /// 选择节点 ; 只要子节点中一个success，那么就返回success，否则返回 failure
    /// </summary>
    public class BTSelector : BTBranch
    {
        public BTSelector(bool shuffle)
        {
            if (!shuffle) return;

            var n = children.Count;
            // 对每个元素进行随机位置转换
            while(n >1)
            {
                n--;
                var k = Mathf.FloorToInt(Random.value * (n + 1));
                var value = children[k];
                children[k] = children[n];
                children[n] = value;
            }
        }

        public override BTState Tick()
        {
            var childState = children[activeChild].Tick();
            switch (childState)
            {
                case BTState.Success:
                    activeChild = 0;
                    return BTState.Success;
                case BTState.Failure:
                    activeChild++;
                    if (activeChild == children.Count)
                    {
                        activeChild = 0;
                        return BTState.Failure;
                    }
                    return BTState.Continue;
                case BTState.Continue:
                    return BTState.Continue;
                case BTState.Abort:
                    activeChild = 0;
                    return BTState.Abort;
                default:
                    throw new System.Exception("something wrong.. check your trash code, please!");
            }
        }
    }

    /// <summary>
    /// 条件分支节点
    /// </summary>
    public class BTConditionalBranch : BTBlock
    {
        public System.Func<bool> func;
        bool tested = false;
        public BTConditionalBranch(System.Func<bool> func)
        {
            this.func = func;
        }
        public override BTState Tick()
        {
            if (!tested)
            {
                tested = func();
            }
            if (tested)
            {
                var result = base.Tick();
                if (result == BTState.Continue)
                    return BTState.Continue;
                else
                {
                    tested = false;
                    return result;
                }
            }
            else
            {
                return BTState.Failure;
            }
        }
    }

    /// <summary>
    /// while节点，如果条件为true，会执行所有的子节点
    /// </summary>
    public class BTWhile : BTBlock
    {
        public System.Func<bool> fn;

        public BTWhile(System.Func<bool> fn)
        {
            this.fn = fn;
        }

        public override BTState Tick()
        {
            if (fn())
                base.Tick();
            else
            {
                ResetChildren();
                return BTState.Failure;
            }
            return BTState.Continue;
        }
    }

    /// <summary>
    /// Repeat 节点，会重复执行 子节点
    /// </summary>
    public class BTRepeat : BTBlock
    {
        private int currentCount = 0;
        public int count { get; private set; } = 1;

        public BTRepeat(int count)
        {
            this.count = count;
        }

        public override BTState Tick()
        {
            if (count <= 0 || currentCount >= count)
                return BTState.Success;

            var result = base.Tick();
            switch (result)
            {
                case BTState.Continue:
                    return BTState.Continue;
                default:
                    currentCount++;
                    if (currentCount == count)
                    {
                        currentCount = 0;
                        return BTState.Success;
                    }
                    return BTState.Continue;
            }
        }
    }

   
    /// <summary>
    /// Wait 节点， 等待一定时间
    /// </summary>
    public class BTWait : BTBlock
    {
        public float seconds { get; private set; } = 0;

        float future = -1;

        public BTWait(float seconds)
        {
            this.seconds = seconds;
        }

        public override BTState Tick()
        {
            if (future < 0)
                future = Time.time + seconds;
            if(Time.time >= future)
            {
                future = -1;
                return BTState.Success;
            }
            return BTState.Continue;
        }
    }

}