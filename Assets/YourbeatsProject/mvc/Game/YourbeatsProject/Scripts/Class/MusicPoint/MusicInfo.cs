//-----------------------------------------------【Function Indroduction】----------------------------------------------
//	  MusicInfo：  
//    Language：  C#
//    IDE：VS2013
//    2015.12.04  Created by RaymondMG  
//    2015.12.04  LastEdited by RaymondMG  
//---------------------------------------------------------------------------------------------------------------------
using System.Collections.Generic;

public class MusicInfo
{
    /// <summary>
    /// 音乐名称
    /// </summary>
    public string MusicName { get; set; }
    /// <summary>
    /// 音乐作者名称
    /// </summary>
    public string MusicAuthorName { get; set; }
    ///// <summary>
    ///// 音乐发布时间
    ///// </summary>
    public string MusicPublishDate { get; set; }
    ///// <summary>
    ///// 音乐基本介绍
    ///// </summary>
    public string MusicDescription { get; set; }
    ///// <summary>
    ///// 音乐时长
    ///// </summary>
    public double MusicTime { get; set; }
    /// <summary>
    /// 音乐资源路径
    /// </summary>
    public string MusicResourcesPath { get; set; }
    /// <summary>
    /// 音乐鼓点列表
    /// </summary>
    public Dictionary<string, MusicPoint> MusicPointsDictionary { get; set; }


}


public class MusicInfoPrefab
{
    /// <summary>
    /// 音乐名称
    /// </summary>
    public string MusicName { get; set; }
    /// <summary>
    /// 音乐作者名称
    /// </summary>
    public string MusicAuthorName { get; set; }
    ///// <summary>
    ///// 音乐发布时间
    ///// </summary>
    public string MusicPublishDate { get; set; }
    ///// <summary>
    ///// 音乐基本介绍
    ///// </summary>
    public string MusicDescription { get; set; }
    ///// <summary>
    ///// 音乐时长
    ///// </summary>
    public double MusicTime { get; set; }
    /// <summary>
    /// 音乐资源路径
    /// </summary>
    public string MusicResourcesPath { get; set; }
    /// <summary>
    /// 音乐鼓点列表
    /// </summary>
    public Dictionary<string, MusicPointPrefab> MusicPointsDictionary { get; set; }


}
