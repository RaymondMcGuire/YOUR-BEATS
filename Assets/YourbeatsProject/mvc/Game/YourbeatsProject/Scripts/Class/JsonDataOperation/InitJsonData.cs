using UnityEngine;
using LitJson;


/// <summary>
/// 初始化游戏Json数据
/// </summary>
public class InitJsonData : SingletonBase<InitJsonData>{


    /// <summary>
    /// 通过文件名读取音乐配置相关的json数据信息
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="musicInfo"></param>
    public void ReadJsonFromFile(string fileName,  ref MusicInfo musicInfo)
    {
        var filePath = "JsonFile/" + fileName;
        var textAsset = Resources.Load<TextAsset>(filePath);
        var data = JsonMapper.ToObject<MusicInfo>(textAsset.text);
        musicInfo = data;
        //Debug.Log("解析" + fileName+"成功");
    }


}


