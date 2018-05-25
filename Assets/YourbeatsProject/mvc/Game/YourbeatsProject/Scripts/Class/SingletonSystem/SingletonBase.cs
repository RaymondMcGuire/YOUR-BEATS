//-----------------------------------------------【Function Indroduction】----------------------------------------------
//	  YourBeats：  SingletonBase  单例类的基类,所有使用单例模式的类继承自这个类
//    Language：  C#
//    IDE：VS2013
//    2015.11.27  Created by 茅炜  
//    2015.11.27  LastEdited by RaymondMG  
//---------------------------------------------------------------------------------------------------------------------
using UnityEngine;

public class SingletonBase <T>
    where T : new()
{
	private static T _instance;						

	public static T GetInstance
	{
		get
		{
		    if (_instance != null) return _instance;
		    //_instance=GameObject.FindObjectOfType(typeof(T)) as T;
		    _instance = new T();
		    if(_instance==null)
		    {
		        Debug.LogError("Singleton ERROR:Cannot find singleton object called "+typeof(T));
		    }
		    return _instance;
		}
	}
}
