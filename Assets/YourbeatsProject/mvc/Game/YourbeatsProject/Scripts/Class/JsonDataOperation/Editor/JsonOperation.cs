//-----------------------------------------------【Function Indroduction】----------------------------------------------
//	  JsonOperation：  
//    Language：  C#
//    IDE：VS2013
//    2015.12.04  Created by RaymondMG  
//    2015.12.04  LastEdited by RaymondMG  
//---------------------------------------------------------------------------------------------------------------------
using UnityEngine;
using System.IO;
using System.Text;
using LitJson;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// 对json数据在编辑器内进行存储
/// </summary>
public class JsonOperation:SingletonBase<JsonOperation> {


    /// <summary>
    /// 存储json数据到文件中    将单个音乐文件数据进行存储写入
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="fileContent"></param>
    /// <param name="singleMusicInfo"></param>
    public bool SaveJsonDataToFile(string fileName,MusicInfo singleMusicInfo)
    {

        //没有文件内容
        if (null == singleMusicInfo)
        {
            Debug.Log("写入错误!");
            return false;
        }

        var filePath = Application.dataPath + @"/Resources/JsonFile/" + fileName + ".json";
        var t = new FileInfo(filePath);
        if (!File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        var sw = t.CreateText();

        string jsonInfo = JsonMapper.ToJson(singleMusicInfo);

        sw.WriteLine(jsonInfo);
        sw.Close();
        sw.Dispose();
        AssetDatabase.Refresh();

        return true;
    }

    /// <summary>
    /// 存储json数据到文件中    将字典中存储数据写入
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="fileContent"></param>
    public bool SaveJsonDataToFile(string fileName,Dictionary<string,string> fileContent)
    {
        //没有文件内容
        if (null == fileContent)
        {
            Debug.Log("写入错误!");
            return false;
        }

        var filePath = Application.dataPath + @"/Resources/JsonFile/" + fileName+".json";
        var t = new FileInfo(filePath);
        if (!File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        var sw = t.CreateText();
        var sb = new StringBuilder();
        var writer = new JsonWriter(sb);


        writer.WriteArrayStart();
        foreach (var item in fileContent)
        {
            writer.WriteObjectStart();
            writer.WritePropertyName(item.Key);
            writer.Write(item.Value);
            writer.WriteObjectEnd();
        }
        writer.WriteArrayEnd();
        sw.WriteLine(sb.ToString());
        sw.Close();
        sw.Dispose();
        AssetDatabase.Refresh();

        return true;
    }

	
}
