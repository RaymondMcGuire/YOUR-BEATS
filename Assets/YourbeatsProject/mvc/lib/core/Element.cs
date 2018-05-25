using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace thelab.mvc
{

    /// <summary>
    /// 扩展元素类用来操控不同的基础应用程序类别
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Element<T> : Element where T : BaseApplication
    {
        /// <summary>
        /// Returns app as a custom 'T' type.
        /// </summary>
        new public T app { get { return (T)base.app; } }
    }

    /// <summary>
    /// MVC框架中基础元素类
    /// </summary>
    public class Element : MonoBehaviour
    {

        /// <summary>
        /// 与场景中根节点进行挂钩，默认根节点为application
        /// </summary>
        public BaseApplication app { get { return m_app = Assert<BaseApplication>(m_app, true); } }
        private BaseApplication m_app;

        /// <summary>
        /// 变量存储
        /// </summary>
        private Dictionary<string, object> m_store { get { return _store == null ? (_store = new Dictionary<string, object>()) : _store; } }
        private Dictionary<string, object> _store;

        /// <summary>
        /// 如果var是空的那么找到模板T的实例化，反之返回var.
        /// 如果设置全局p_global=true，那么搜索所有节点，否则只搜索子物体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_var"></param>
        /// <param name="p_global"></param>
        /// <returns></returns>
        public T Assert<T>(T p_var, bool p_global=false) where T : Object 
        {
            return p_var == null ? (p_global ? GameObject.FindObjectOfType<T>() : transform.GetComponentInChildren<T>()) : p_var;            
        }

        /// <summary>
        /// 如果var是空的那么找到模板T的实例化，反之返回storage['var'].
        /// 如果设置全局p_global=true，那么搜索所有节点，否则只搜索子物体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_key"></param>
        /// <param name="p_global"></param>
        /// <returns></returns>
        public T Assert<T>(string p_key, bool p_global = false) where T : Object
        {
            if (m_store.ContainsKey(p_key)) { return (T)(object)m_store[p_key]; }
            T v = (p_global ? GameObject.FindObjectOfType<T>() : transform.GetComponentInChildren<T>());
            m_store[p_key] = v;
            return v;
        }

        /// <summary>
        /// 如果var是空的那么找到模板T本地化的实例化，反之返回storage['var'].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_key"></param>
        /// <returns></returns>
        public T AssertLocal<T>(string p_key) where T : Object
        {
            if (m_store.ContainsKey(p_key)) { return (T)(object)m_store[p_key]; }
            T v = GetComponent<T>();
            m_store[p_key] = v;
            return v;
        }

        /// <summary>
        /// 如果var是空的那么找到模板T本地化的实例化，反之返回var      
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_var"></param>
        /// <returns></returns>
        public T AssertLocal<T>(T p_var,string p_store="") where T : Object 
        {
            T v = default(T);
            if (p_store != "") if (m_store.ContainsKey(p_store)) { return (T)(object)m_store[p_store]; }   
            v = p_var == null ? (p_var = GetComponent<T>()) : p_var;
            if (p_store != "") m_store[p_store] = v;
            return v;
        }

        /// <summary>
        ///  如果var是空的那么在这个元素的父级中找到T的实例化，反之返回storage['var'].        
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_key"></param>
        /// <returns></returns>
        public T AssertParent<T>(string p_key) where T : Object
        {
            if (m_store.ContainsKey(p_key)) { return (T)(object)m_store[p_key]; }
            T v = GetComponentInParent<T>();
            m_store[p_key] = v;
            return v;
        }

        /// <summary>
        ///  如果var是空的那么在这个元素的父级中找到T的实例化，反之返回var.         
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_var"></param>
        /// <returns></returns>
        public T AssertParent<T>(T p_var, string p_store = "") where T : Object
        {
            T v = default(T);
            if (p_store != "") if (m_store.ContainsKey(p_store)) { return (T)(object)m_store[p_store]; }
            v = p_var == null ? (p_var = GetComponentInParent<T>()) : p_var;
            if (p_store != "") m_store[p_store] = v;
            return v;
        }

        /// <summary>
        /// 帮助方法用于转化类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Cast<T>() { return (T)(object)this; }

        /// <summary>
        /// 寻找给予的元素在用.分隔开的路径中
        /// </summary>
        /// <param name="p_path"></param>
        /// <returns></returns>
        public T Find<T>(string p_path) where T : Component
        {
            List<string> tks = new List<string>(p_path.Split('.'));
            if (tks.Count <= 0) return default(T);
            Transform it = transform;
            while (tks.Count > 0)
            {
                string p = tks[0];
                tks.RemoveAt(0);
                it = it.FindChild(p);
                if (it == null) return default(T);
            }
            return it.GetComponent<T>();

        }

        /// <summary>
        /// 导航路径并找到第一个控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_path"></param>
        /// <returns></returns>
        public T AssertFind<T>(string p_path) where T : Component
        {
            if (m_store.ContainsKey(p_path)) { return (T)(object)m_store[p_path]; }
            T v = Find<T>(p_path);
            m_store[p_path] = v;
            return v;
        }

        /// <summary>
        /// 发送一条信息给所有控制者通过实例化类似target或者data等
        /// </summary>
        /// <param name="p_event"></param>
        /// <param name="p_data"></param>
        public void Notify(string p_event,params object[] p_data) { app.Notify(p_event, this, p_data); }
        
        /// <summary>
        /// 发送一条信息给所有控制者通过实例化类似target或者data等，在等待。。秒之后
        /// </summary>
        /// <param name="p_delay"></param>
        /// <param name="p_event"></param>
        /// <param name="p_data"></param>
        public void Notify(float p_delay,string p_event,params object[] p_data) { app.Notify(p_delay,p_event, this, p_data); }

        /// <summary>
        /// 使用该元素进行打印信息
        /// </summary>
        /// <param name="p_msg"></param>
        public void Log(object p_msg, int p_verbose = 0)
        {
            //Only outputs logs equal or bigger than the application 'verbose' level.
            if (p_verbose <= app.verbose) Debug.Log(GetType().Name + "> " + p_msg);
        }

    }

}