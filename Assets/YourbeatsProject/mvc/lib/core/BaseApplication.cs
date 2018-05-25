using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.ComponentModel;





namespace thelab.mvc
{
    /// <summary>
    /// 扩展基础应用程序类用来操控不同的模型、视图、控制器
    /// </summary>
    /// <typeparam name="M"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <typeparam name="C"></typeparam>
    public class BaseApplication<M, V, C> : BaseApplication
        where M : Element
        where V : Element
        where C : Element
    {
        /// <summary>
        /// 模型类引用
        /// </summary>
        new public M model { get { return (M)(object)base.model; } }

        /// <summary>
        /// 视图类引用
        /// </summary>
        new public V view { get { return (V)(object)base.view; } }

        /// <summary>
        /// 控制器类引用
        /// </summary>
        new public C controller { get { return (C)(object)base.controller; } }
    }

    /// <summary>
    ///场景中脚本的根节点类
    /// </summary>
    public class BaseApplication : Element
    {
        /// <summary>
        /// Arguments passed between scenes.
        /// </summary>
        static List<string> m_args { get { return __args == null ? (__args = new List<string>()) : __args; } }
        static List<string> __args;

        /// <summary>
        /// 标记-证明第一个场景已经读取
        /// </summary>
        static bool m_first_scene;

        /// <summary>
        /// 静态物体初始化
        /// </summary>
        static BaseApplication() { m_first_scene = true; }

        /// <summary>
        /// Arguments passed between scenes.
        /// </summary>
        public List<string> args { get { return __args==null ? (new List<string>()) : __args; } }

        /// <summary>
        /// 详细列表
        /// </summary>
        public int verbose;

        /// <summary>
        /// 获取模型初始化实例
        /// </summary>
        public Model model { get { return m_model = Assert<Model>(m_model); } }
        private Model m_model;

        /// <summary>
        /// 获取视图初始化实例
        /// </summary>
        public View view { get { return m_view = Assert<View>(m_view); } }
        private View m_view;

        /// <summary>
        /// 获取控制器实例
        /// </summary>
        public Controller controller { get { return m_controller = Assert<Controller>(m_controller); } }
        private Controller m_controller;

        /// <summary>
        /// 异步数据结构
        /// </summary>
        private List<UnityEngine.AsyncOperation> m_async_loads { get { return __async_loads == null ? (__async_loads = new List<UnityEngine.AsyncOperation>()) : __async_loads; } }
        private List<UnityEngine.AsyncOperation> __async_loads;

        private List<string> m_async_args { get { return __async_args == null ? (__async_args = new List<string>()) : __async_args; } }
        private List<string> __async_args;

        /// <summary>
        /// 初始化
        /// </summary>
        virtual protected void Start()
        {
            __async_loads = new List<UnityEngine.AsyncOperation>();
            __async_args = new List<string>();
            if (m_first_scene) { m_first_scene = false; OnLevelWasLoaded(Application.loadedLevel); }
        }

        /// <summary>
        /// 捕获场景读取事件并且通知控制器开始运行
        /// </summary>
        /// <param name="p_level"></param>
        private void OnLevelWasLoaded(int p_level)
        {
            Notify("scene.load", new object[] { Application.loadedLevelName, Application.loadedLevel });
        }

        /// <summary>
        /// 通知所有控制器信息--target和data
        /// </summary>
        /// <param name="p_event"></param>
        /// <param name="p_target"></param>
        /// <param name="p_data"></param>
        public void Notify(string p_event, Object p_target, params object[] p_data)
        {            
            Controller root = transform.GetComponentInChildren<Controller>();
            Controller[] list = root.GetComponentsInChildren<Controller>();
            Log(p_event + " [" + p_target + "]", 6);
            for (int i = 0; i < list.Length; i++) list[i].OnNotification(p_event, p_target, p_data);
        }

        /// <summary>
        /// 通知所有控制器信息--target
        /// </summary>
        /// <param name="p_event"></param>
        /// <param name="p_target"></param>
        public void Notify(string p_event, Object p_target) { Notify(p_event, p_target,new object[]{}); }

        /// <summary>
        /// 通知所有控制器信息--target和data,在delay之后
        /// </summary>
        /// <param name="p_event"></param>
        /// <param name="p_target"></param>
        /// <param name="p_data"></param>
        public void Notify(float p_delay,string p_event, Object p_target,params object[] p_data)
        {            
            StartCoroutine(TimedNotify(p_delay,p_event,p_target,p_data));
        }

        /// <summary>
        /// 内部通知
        /// </summary>
        /// <param name="p_delay"></param>
        /// <param name="p_event"></param>
        /// <param name="p_target"></param>
        /// <param name="p_data"></param>
        /// <returns></returns>
        private IEnumerator TimedNotify(float p_delay, string p_event, Object p_target,params object[] p_data)
        {
            yield return new WaitForSeconds(p_delay);
            Notify(p_event, p_target, p_data);
        }

        /// <summary>
        /// 通知所有控制器信息--target，在delay之后
        /// </summary>
        /// <param name="p_delay"></param>
        /// <param name="p_event"></param>
        /// <param name="p_target"></param>
        public void Notify(float p_delay, string p_event, Object p_target) { Notify(p_delay,p_event, p_target); }

        /// <summary>
        /// 添加一个新场景并赋予名称.一部标签可以控制读取的类型
        /// </summary>
        /// <param name="p_name"></param>
        /// <param name="p_async"></param>
        /// <param name="p_args"></param>
        public void SceneAdd(string p_name, bool p_async, params string[] p_args)
        {
            if (p_async) { StartCoroutine(SceneLoadAsync(p_name, true, p_args)); }
            else
            {
                __args = new List<string>(p_args);
                Application.LoadLevelAdditive(p_name);
            }
        }

        /// <summary>
        /// 添加新场景
        /// </summary>
        /// <param name="p_name"></param>
        /// <param name="p_args"></param>
        public void SceneAdd(string p_name,params string[] p_args) { SceneAdd(p_name, false, p_args); }

        /// <summary>
        /// 根据名称读取场景.标明是否需要异步
        /// </summary>
        /// <param name="p_name"></param>
        /// <param name="p_async"></param>
        /// <param name="p_args"></param>
        public void SceneLoad(string p_name,bool p_async,params string[] p_args)
        {
            if (p_async) { StartCoroutine(SceneLoadAsync(p_name,false,p_args)); }
            else
            {
                __args = new List<string>(p_args);
                Application.LoadLevel(p_name);
            }
        }

        /// <summary>
        /// 根据名称读取场景
        /// </summary>
        /// <param name="p_name"></param>
        /// <param name="p_args"></param>
        public void SceneLoad(string p_name,params string[] p_args) { SceneLoad(p_name, false, p_args); }

        /// <summary>
        /// 内部方法-异步读取场景
        /// </summary>
        /// <param name="p_name"></param>
        /// <param name="p_args"></param>
        /// <returns></returns>
        private IEnumerator SceneLoadAsync(string p_name,bool p_additive,params string[] p_args)
        {
            //float p = 0f;
            UnityEngine.AsyncOperation async = null;
            string ev = "";

            if(p_additive)
            {
                ev = "scene.add.progress";
                async = Application.LoadLevelAdditiveAsync(p_name);
            }
            else
            {
                ev = "scene.load.progress";
                async = Application.LoadLevelAsync(p_name);
            }

            m_async_loads.Add(async);
            m_async_args.Add(p_name + "~" + ev);

            yield return async;
            __args = new List<string>(p_args);
        }

        /// <summary>
        /// 更新内部状态
        /// </summary>
        void Update()
        {
            for(int i=0;i<m_async_loads.Count;i++)
            {
                UnityEngine.AsyncOperation async = m_async_loads[i];
                if (async != null)
                {
                    string args = m_async_args[i];
                    string s_name = args.Split('~')[0];
                    string s_ev = args.Split('~')[1];
                    if (s_ev != "") Notify(s_ev, new object[] { s_name, async.progress });
                    if (async.progress >= 1.0) m_async_loads[i] = null;                    
                }
                else
                {
                    m_async_loads.RemoveAt(i--);
                    m_async_args.RemoveAt(i--);
                }
            }
        }
    }
}
