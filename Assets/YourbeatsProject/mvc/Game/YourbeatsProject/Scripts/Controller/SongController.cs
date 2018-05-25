//-----------------------------------------------【Function Indroduction】----------------------------------------------
//	  SongController：  
//    Language：  C#
//    IDE：VS2013
//    2015.12.5  Created by 茅炜  
//    2015.12.5  LastEdited by 茅炜  
//---------------------------------------------------------------------------------------------------------------------
using thelab.mvc;
using UnityEngine;
using YourBeats.core.gamemode;

public class SongController : Controller<YourBeatsApplication> 
{
	public override void OnNotification(string pEvent, Object pTarget, params object[] pData)
	{
		switch (pEvent)
		{

			// 初始化
			case "song.init":
				app.model.Song.InitSongModel();
				break;
			// 音乐播放
			case "song.play":
					app.model.Song.PlayCurrentSong();
				break;
            case "song.update":
                //app.model.Song.HandlerCurrentTuneJudge();
                break;
			// 音乐停止
			case "song.stop":
					app.model.Song.StopCurrentSong();	
				break;
			// 音乐暂停
			case "song.pause":
					app.model.Song.PauseCurrentSong();
				break;
		}
	}
}
