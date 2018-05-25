using UnityEngine;
using System.Collections;

namespace thelab.mvc
{

    /// <summary>
    /// 视图基类
    /// </summary>
    public class View : Element
    {
        
    }

    /// <summary>
    /// 视图相关的所有类
    /// </summary>
    public class View<T> : View where T : BaseApplication
    {
        /// <summary>
        /// 返回用户所需要的类型
        /// </summary>
        new public T app { get { return (T)base.app; } }
    }

}