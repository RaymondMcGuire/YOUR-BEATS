//-----------------------------------------------【Function Indroduction】----------------------------------------------
//	  MusicPoint：  
//    Language：  C#
//    IDE：VS2013
//    2015.12.04  Created by RaymondMG  
//    2015.12.04  LastEdited by RaymondMG  
//---------------------------------------------------------------------------------------------------------------------
using UnityEngine;


/// <summary>
/// 音乐鼓点类（配置json文件使用）
/// </summary>
public class MusicPoint
{
    public double MusicPointV3X { get; set; }
    public double MusicPointV3Y { get; set; }
    public double MusicPointV3Z { get; set; }

    public double MusicPointQ4X { get; set; }
    public double MusicPointQ4Y { get; set; }
    public double MusicPointQ4Z { get; set; }
    public double MusicPointQ4W { get; set; }

    public MusicPointType MusicType { get; set; }
}


/// <summary>
/// 音乐鼓点预制物类（游戏使用）
/// </summary>
public class MusicPointPrefab
{
    public Vector3 MusicPointV3 { get; set; }
    public Quaternion MusicPointQ4 { get; set; }

    public MusicPointType MusicType { get; set; }
}
