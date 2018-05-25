using UnityEngine;
using System.Collections;

namespace thelab.mvc
{

    /// <summary>
    /// 控制器基类
    /// </summary>
    public class Controller : Element
    {

        /// <summary>
        /// 在当前运行场景中发送通知
        /// </summary>
        /// <param name="pEvent"></param>
        /// <param name="pTarget"></param>
        /// <param name="pData"></param>
        virtual public void OnNotification(string pEvent, Object pTarget, params object[] pData) { }
    }

    /// <summary>
    /// 所有控制器相关的类
    /// </summary>
    public class Controller<T> : Controller where T : BaseApplication
    {
        /// <summary>
        /// Returns app as a custom 'T' type.
        /// </summary>
        new public T app { get { return (T)base.app; } }
    }

}