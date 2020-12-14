using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NeverSayNever;

namespace NeverSayNever.Core
{
    public class CoroutineManager : USingleton<CoroutineManager>
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

                    Instance.RemoveCoroutine(Id);
                }

                yield return null;
            }
        }

        private static Dictionary<long, CoroutineTask> _coroutineDic;

        private long _taskCounter;

        public override void OnInitialize()
        {
            _coroutineDic = new Dictionary<long, CoroutineTask>();
        }

        private void OnDestroy() => OnDispose();
        
        public override void OnDispose()
        {
            if (_coroutineDic == null)
                return;
            foreach (var task in _coroutineDic.Values)
            {
                task.Running = false;
            }

            _coroutineDic.Clear();
            base.OnDispose();
        }
        
        
        /// <summary>
        /// 添加并立即调用
        /// </summary>
        /// <param name="co"></param>
        /// <returns></returns>
        public long AddCoroutine(IEnumerator co)
        {
            if (!this.gameObject.activeSelf) return -1;
            var task = new CoroutineTask(_taskCounter++);
            _coroutineDic.Add(task.Id, task);
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
            _coroutineDic.TryGetValue(key, out var task);
            if (task == null) return;
            task.Running = false;
            _coroutineDic.Remove(key);
        }

        /// <summary>
        /// 暂停指定协程
        /// </summary>
        /// <param name="id"></param>
        public void PauseCoroutine(long id)
        {
            var key = id.ToString();
            _coroutineDic.TryGetValue(id, out var task);
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
            _coroutineDic.TryGetValue(key, out var task);
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
            return AddCoroutine(DelayedCallImpl(delayedTime, callback));
        }

        private IEnumerator DelayedCallImpl(float delayedTime, System.Action callback)
        {
            if (delayedTime >= 0)
                yield return new WaitForSeconds(delayedTime);
            callback();
        }


    }
}