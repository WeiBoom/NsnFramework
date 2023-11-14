using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nsn;

namespace Nsn
{
    public class CoroutineMgr : SingletonMono<CoroutineMgr>
    {
        private class CoroutineTask
        {
            public long Id { get; private set; }

            public bool Running { get; set; }

            public bool Paused { get; set; }


            public CoroutineTask(long id)
            {
                Id = id;
                Running = true;
                Paused = false;
            }

            public IEnumerator CoroutineWrapper(IEnumerator co)
            {
                var coroutine = co;
                while (Running)
                {
                    if (Paused)
                    {
                        yield return null;
                    }
                    else
                    {
                        if (coroutine != null && coroutine.MoveNext())
                        {
                            yield return coroutine.Current;
                        }
                        else
                        {
                            Running = false;
                        }
                    }
                }
                Instance.RemoveCoroutine(Id);
                yield return null;
            }
        }

        private static Dictionary<long, CoroutineTask> m_CoroutineDic;

        private long m_TtaskCounter;


        public override void OnDispose()
        {
            if (m_CoroutineDic == null)
                return;
            foreach (var task in m_CoroutineDic.Values)
            {
                task.Running = false;
            }

            m_CoroutineDic.Clear();
            base.OnDispose();
        }
        
        
        /// <summary>
        /// 执行协程并添加到Map中
        /// </summary>
        /// <param name="co"></param>
        /// <returns></returns>
        public long ExecuteCoroutine(IEnumerator co)
        {
            if (!this.gameObject.activeSelf) return -1;
            var task = new CoroutineTask(m_TtaskCounter++);
            m_CoroutineDic.Add(task.Id, task);
            StartCoroutine(task.CoroutineWrapper(co));
            return task.Id;

        }

        /// <summary>
        /// 移除协程
        /// </summary>
        /// <param name="id"></param>
        public void RemoveCoroutine(long id)
        {
            var key = id;
            m_CoroutineDic.TryGetValue(key, out var task);
            if (task == null) return;
            task.Running = false;
            m_CoroutineDic.Remove(key);
        }

        /// <summary>
        /// 暂停指定协程
        /// </summary>
        /// <param name="id"></param>
        public void PauseCoroutine(long id)
        {
            var key = id.ToString();
            m_CoroutineDic.TryGetValue(id, out var task);
            if (task != null)
            {
                task.Paused = true;
            }
            else
            {
                Debug.LogError("Coroutine : " + key + " is not exist");
            }
        }

        /// <summary>
        /// 重启暂停的协程
        /// </summary>
        /// <param name="id"></param>
        public void ResumeCoroutine(long id)
        {
            var key = id;
            m_CoroutineDic.TryGetValue(key, out var task);
            if (task != null)
            {
                task.Paused = false;
            }
            else
            {
                Debug.LogError("Coroutine : " + key + " is not exist");
            }
        }

        /// <summary>
        /// 延时调用协程
        /// </summary>
        /// <param name="delayedTime"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public long DelayedCall(float delayedTime, System.Action callback)
        {
            // 本质上也是直接启动协程，但是等待delayedTime 后才真正执行内容
            return ExecuteCoroutine(DelayedCallImpl(delayedTime, callback));
        }

        private IEnumerator DelayedCallImpl(float delayedTime, System.Action callback)
        {
            if (delayedTime >= 0)
                yield return new WaitForSeconds(delayedTime);
            callback();
        }

    }
}