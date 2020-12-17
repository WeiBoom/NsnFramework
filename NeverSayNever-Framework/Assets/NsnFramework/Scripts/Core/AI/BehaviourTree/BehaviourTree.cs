using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever.Core.AI
{
    // 脱胎于 unity package Gamekit2D
    public enum BTState
    {
        Failure,
        Success,
        Continue,
        Abort,
    }

    // 行为树
    public static class BehaviourTree
    {
        public static BTRoot Root() => new BTRoot();

        public static Sequence Sequence() => new Sequence();

        public static Selector Selector(bool shuffle = false) => new Selector(shuffle);

        public static Action RunCoroutine(System.Func<IEnumerator<BTState>> coroutine) => new Action(coroutine);

        public static Action Call(System.Action fn) => new Action(fn);

        public static ConditionalBranch If(System.Func<bool> fn) => new ConditionalBranch(fn);

        public static While While(System.Func<bool> fn) => new While(fn);

        public static Condition Condition(System.Func<bool> fn) => Condition(fn);

        public static Repeat Repeat(int count) => Repeat(count);

        public static Wait Wait(float seconds) => Wait(seconds);

        public static Terminate Terminate() => Terminate();

        public static Log Log(string msg) => Log(msg);
    }

    // 节点抽象类
    public abstract class BTNode
    {
        public abstract BTState Tick();
    }

    // 行为树分支基类
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

    // 行为块基类，默认会执行所有的子节点内容。
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

    // 行为树根节点
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



    /*
     * 终端节点，不再执行后续内容
     */
    public class Terminate : BTNode
    {
        public override BTState Tick()
        {
            return BTState.Abort;
        }
    }

    /*
     * 日志节点，输出log日志信息
     */
    public class Log : BTNode
    {
        string msg;
        string[] args;
        public Log(string msg, params string[] args)
        {
            this.msg = msg;
            this.args = args;
        }

        public override BTState Tick()
        {
            NeverSayNever.Utilities.ULog.Print(msg, args);
            return BTState.Success;
        }
    }

    /*
     * 装饰节点 仅有一个子节点，作为辅助判断的一个节点，执行时间，执行条件，最大次数，最大时间，运行状态等。
     */
    public abstract class Decorator : BTNode
    {
        protected BTNode child;
        public Decorator Do(BTNode child)
        {
            this.child = child;
            return this;
        }
    }


    /*
     * 行为节点 ，执行具体内容
     */
    public class Action : BTNode
    {
        // 执行一个方法
        System.Action func;
        // 或者执行一堆协程
        System.Func<IEnumerator<BTState>> coroutineFactory;
        IEnumerator<BTState> coroutine;

        public Action(System.Action func)
        {
            this.func = func;
        }

        public Action(System.Func<IEnumerator<BTState>> coroutineFactory)
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

    /*
     * 条件节点
     */
    public class Condition : BTNode
    {
        public System.Func<bool> func;

        public Condition(System.Func<bool> func)
        {
            this.func = func;
        }

        public override BTState Tick()
        {
            return func() ? BTState.Success : BTState.Failure;
        }
    }

    /*
     * 顺序节点，从左到右依次执行所有节点，只有当一个节点返回success时，才会执行下一个
     */
    public class Sequence : BTBranch
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

    /*
     * 选择节点，只要子节点中一个success，那么就返回success，否则返回 failure
     */
    public class Selector : BTBranch
    {
        public Selector(bool shuffle)
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

    /*
     */
    public class ConditionalBranch : BTBlock
    {
        public System.Func<bool> func;
        bool tested = false;
        public ConditionalBranch(System.Func<bool> func)
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


    /*
     * while节点，如果条件为true，会执行所有的子节点
     */
    public class While : BTBlock
    {
        public System.Func<bool> fn;

        public While(System.Func<bool> fn)
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

    /*
     * 重复节点，会重复执行一定次数
     */
    public class Repeat : BTBlock
    {
        private int currentCount = 0;
        public int count { get; private set; } = 1;

        public Repeat(int count)
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

    /*
     * 等待节点，一段时间过后返回 success
     */
    public class Wait : BTBlock
    {
        public float seconds { get; private set; } = 0;

        float future = -1;

        public Wait(float seconds)
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