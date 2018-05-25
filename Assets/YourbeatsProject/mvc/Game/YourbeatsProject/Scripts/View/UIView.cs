//-----------------------------------------------【Function Indroduction】----------------------------------------------
//	  UIView：  
//    Language：  C#
//    IDE：VS2013
//    2015.11.14  Created by RaymondMG  
//    2015.11.27  LastEdited by RaymondMG  
//---------------------------------------------------------------------------------------------------------------------
using thelab.mvc;
using YourBeats.core.gamemode;
public class UIView : View<YourBeatsApplication>
{

    void Awake()
    {
        Notify("ui.awake");
    }

    void Start()
    {
        Notify("ui.start");
    }

    void Update()
    {
        Notify("ui.pressbutton");
    }


    /// <summary>
    /// 上传音乐文件
    /// </summary>
    public void UpLoadMusicFile()
    {
        GameMode.GetInstance.GameModeStateSwitch(GameModeType.SelectMusic);
    }

    /// <summary>
    /// 选择音乐开始游戏
    /// </summary>
    public void SelectMusic()
    {
        GameMode.GetInstance.GameModeStateSwitch(GameModeType.StartPlay);
    }

    /// <summary>
    /// 暂停
    /// </summary>
    public void Pause()
    {
        GameMode.GetInstance.GameModeStateSwitch(GameModeType.Pause);
    }

    /// <summary>
    /// 停止
    /// </summary>
    public void Stop()
    {
        GameMode.GetInstance.GameModeStateSwitch(GameModeType.Stop);
    }

    /// <summary>
    /// 继续
    /// </summary>
    public void Resume()
    {
        GameMode.GetInstance.GameModeStateSwitch(GameModeType.Playing);
    }

    /// <summary>
    /// 返回主界面
    /// </summary>
    public void BackSelectMusic()
    {
        GameMode.GetInstance.GameModeStateSwitch(GameModeType.SelectMusic);
    }
	
}
