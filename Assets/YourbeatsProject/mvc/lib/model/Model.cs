using UnityEngine;
using System.Collections;

namespace thelab.mvc
{

    /// <summary>
    /// 模型基类
    /// </summary>
    public class Model : Element
    {

    }


    /// <summary>
    /// 模型相关所有类
    /// </summary>
    public class Model<T> : Model where T : BaseApplication
    {
        /// <summary>
        /// 返回用户所需要的类型
        /// </summary>
        new public T app { get { return (T)base.app; } }
    }
}