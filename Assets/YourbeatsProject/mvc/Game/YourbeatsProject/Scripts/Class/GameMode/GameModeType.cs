//-----------------------------------------------【Function Indroduction】----------------------------------------------
//	  GameModeType：  
//    Language：  C#
//    IDE：VS2013
//    2015.11.14  Created by RaymondMG  
//    2015.11.27  LastEdited by RaymondMG  
//---------------------------------------------------------------------------------------------------------------------
namespace YourBeats.core.gamemode
{
    /// <summary>
    /// 游戏模式类型
    /// </summary>
    public enum GameModeType
    {

        Awake, //整个项目唤醒
        Start, //项目开始运行
        Calibration, //校准
        UpLoadLocalMusicFile, //上传本地音频文件生成自动鼓点曲目
        SelectMusic, //音乐选择界面    
        StartPlay, //开始游戏初始化数据
        Playing, //游戏进行时
        Pause, //游戏暂停
        Stop, //游戏结束
        End, //游戏结束
        Evalute //游戏评估界面
    }
}