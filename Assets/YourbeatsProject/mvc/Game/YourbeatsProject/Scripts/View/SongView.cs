//-----------------------------------------------【Function Indroduction】----------------------------------------------
//	  SongView：  
//    Language：  C#
//    IDE：mono
//    2015.12.5  Created by 茅炜  
//    2015.12.5  LastEdited by 茅炜  
//---------------------------------------------------------------------------------------------------------------------
using thelab.mvc;

public class SongView : View<YourBeatsApplication>
{
	/// <summary>
	/// 初始化
	/// </summary>
	void Start()
	{
		//游戏Awake初始化
		Notify("song.init");
	}


    void Update()
    {
        Notify("song.update");
    }


	/// <summary>
	/// 播放音乐
	/// </summary>
	public void PlaySong()
	{
		Notify("song.play");
	}

	/// <summary>
	/// 暂停音乐
	/// </summary>
	public void PauseSong()
	{
		Notify("song.pause");
	}

	/// <summary>
	/// 停止音乐
	/// </summary>
	public void StopSong()
	{
		Notify("song.stop");
	}
}
