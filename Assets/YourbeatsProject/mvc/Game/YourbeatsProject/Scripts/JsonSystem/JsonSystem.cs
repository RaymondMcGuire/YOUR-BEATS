//-----------------------------------------------【Function Indroduction】----------------------------------------------
//	  JsonSystem：  
//    Language：  C#
//    IDE：mono
//    2015.12.3  Created by 茅炜  
//    2015.12.5  LastEdited by 茅炜  
//---------------------------------------------------------------------------------------------------------------------
using UnityEngine;

public class JsonSystem : SingletonBase<JsonSystem> 
{
	/// <summary>
	/// Json文件的存放路径
	/// </summary>
	public string JsonPath;

	public void InitJsonSystem()
	{
		Debug.Log("Json系统初始化完成！");
	}

	/// <summary>
	/// 读取Json文件
	/// </summary>
	public void LoadJsonFile()
	{
		Debug.Log("读取Json文件完成！");
	}

	/// <summary>
	/// 分析Json文件
	/// </summary>
	public void AnalyseJsonFile()
	{
		Debug.Log("分析json文件完成！");
	}



}
