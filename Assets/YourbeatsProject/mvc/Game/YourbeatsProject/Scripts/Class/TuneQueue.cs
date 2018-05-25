using UnityEngine;
using System.Collections;

namespace JudgementSystemManager
{
    /// <summary>
    /// 顺序队列
    /// </summary>
    public class TuneQueue
    {

        private readonly ArrayList queue;

        public TuneQueue()
        {
            if (null == queue)
                queue = new ArrayList();
        }

        /// <summary>
        /// 入队列
        /// </summary>
        public void Push(TuneNode a)
        {
            if (null == queue) { Debug.Log("请先进行初始化队列"); return; }

            if (null == a) { Debug.Log("请对顺序节点初始化"); return; }
           
            queue.Add(a);

        }

        /// <summary>
        /// 出队列
        /// </summary>
        public TuneNode Pop()
        {
            var a = new TuneNode();

            if (null == queue) { Debug.Log("请先进行初始化队列"); return a; }

            if (0 == queue.Count) { Debug.Log("队列中无元素"); return a; }

            if (queue.Count > 0)
            {
                a = (TuneNode)queue[0];
                queue.Remove(queue[0]);
            }

            return a;
        }

        /// <summary>
        /// 获取长度
        /// </summary>
        /// <returns></returns>
        public int Length()
        {
            return queue.Count;
        }

        /// <summary>
        /// 清空顺序队列
        /// </summary>
        public void Clear()
        {
            queue.Clear();
        }
    }
}
