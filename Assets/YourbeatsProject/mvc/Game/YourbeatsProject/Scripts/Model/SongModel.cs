//-----------------------------------------------【Function Indroduction】----------------------------------------------
//	  SongModel：  
//    Language：  C#
//    IDE：mono
//    2015.12.3  Created by 茅炜  
//    2015.12.5  LastEdited by 茅炜  
//---------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using JudgementSystemManager;
using thelab.mvc;
using UnityEngine;
using System.Linq;
public class SongModel : Model<YourBeatsApplication> 
{
    //单音预置物体
    public GameObject SingleTune;

    public GameObject FwdTune;

    public GameObject BakTune;

	public float PointTime = 3.124f;


    public GameObject flydisk;

	//已经使用过的音符
	public Dictionary<float, GameObject> InitedPoint = new Dictionary<float, GameObject>();

	//时间-鼓点类型集合
	public Dictionary<float, MusicPointPrefab> PointType = new Dictionary<float, MusicPointPrefab>();

	/// <summary>
	/// 音乐播放组件
	/// </summary>
	public static AudioSource SongAudioSource;

	/// <summary>
	/// 音乐片段们
	/// </summary>
    public static AudioClip SongAudioClip;

	/// <summary>
	/// 目前的音乐播放时长
	/// </summary>
	public float CurrentSongTime;

	/// <summary>
	/// 当前音频时间长
	/// </summary>
	public float CurrentAudioLength=0;

	/// <summary>
	/// 歌曲正在播放吗？
	/// </summary>
	public static bool IsSongPlaying { get;  set; }

    /// <summary>
	/// 初始化操作
	/// </summary>
	public void InitSongModel()
	{
		SongAudioSource = GameObject.FindWithTag("MainCameraController") ? GameObject.FindWithTag("MainCameraController").GetComponent<AudioSource>() : Camera.main.GetComponent<AudioSource>();

		// 音乐还未播放
		IsSongPlaying=false;

		InitTunePrefab();

		Log("音乐数据初始化完成");
	}

	//从资源读取预置物体
	public void InitTunePrefab()
	{
		SingleTune = Resources.Load<GameObject>("Prefabs/SingleTune");

		FwdTune = Resources.Load<GameObject>("Prefabs/FwdTune");

		BakTune = Resources.Load<GameObject>("Prefabs/BwdTune");
	}
	//初始化时间-鼓点类型 集合
	public void InitPointType()
	{
		ClearPointTypeDictionary();
	}

	//增加时间-鼓点类型
	public void AddPointType(float time, MusicPointPrefab type)
	{
        if (!PointType.ContainsKey(time))
		PointType.Add(time,type);
	}

	//删除所有时间-鼓点
	public void ClearPointTypeDictionary()
	{
		PointType.Clear();
	}

	//删除已经记录的时间-鼓点
	public void ClearInitedPoint()
	{
		InitedPoint.Clear();
	}



    //最长判定时间
    private const float MaxJudgeTime = 2.0f;
    //处理音符队列
    public static readonly Dictionary<float, GameObject> HandlerDictionary = new Dictionary<float, GameObject>();
    //当前音符
    private static  GameObject _curtuneobj;
    public static float Curtunetime = -1;
    //当前音符是否结束
    public static bool Curover = true;
    /// <summary>
    /// 处理当前音符判定
    /// </summary>
    public void HandlerCurrentTuneJudge()
    {
        if (Curtunetime == -1) return;

        if (!HandlerDictionary.ContainsKey(Curtunetime) )
        {
            if (HandlerDictionary.Count != 0 && Curover)
                Curtunetime = HandlerDictionary.Keys.Min();
            else
                return;
        }


        if (HandlerDictionary[Curtunetime] == null) return;

        if (HandlerDictionary[Curtunetime].GetComponent<SingleTuneJudgement>())
        {
            if (!HandlerDictionary[Curtunetime].GetComponent<SingleTuneJudgement>().Judgeonce)
            {
                HandlerDictionary[Curtunetime].GetComponent<SingleTuneJudgement>().Judgeonce = true;
                Curover = false;
                HandlerDictionary.Remove(Curtunetime);
            }
        }
        else if (HandlerDictionary[Curtunetime].GetComponent<FwdTuneJudgement>())
        {
            if (!HandlerDictionary[Curtunetime].GetComponent<FwdTuneJudgement>().Judgeonce)
            {
                if (!HandlerDictionary[Curtunetime].GetComponent<FwdTuneJudgement>().Judgeonce)
                {
                    HandlerDictionary[Curtunetime].GetComponent<FwdTuneJudgement>().Judgeonce = true;
                    Curover = false;
                    HandlerDictionary.Remove(Curtunetime);
                }
            }
        }
    }


    /// <summary>
    /// 显示鼓点
    /// </summary>
    /// <param name="time"></param>
    public void DisplayPoint(float time)
    {
        foreach (var p in PointType)
	    {
	        if (!(Mathf.Abs(p.Key - time) < 0.1f) || InitedPoint.ContainsKey(p.Key)) 
				continue;
			GameObject tempObj;
	        
			switch(p.Value.MusicType)
			{
				case MusicPointType.SingleTune:
					tempObj=InstaniateTuneType(SingleTune,p.Value.MusicPointV3,p.Value.MusicPointQ4);
			        tempObj.name = p.Key.ToString(CultureInfo.InvariantCulture);
					InitedPoint.Add(p.Key, tempObj);

					break;
				case MusicPointType.BakTuneBegin:
					tempObj=InstaniateTuneType(BakTune,p.Value.MusicPointV3,p.Value.MusicPointQ4);
                    tempObj.name = p.Key.ToString(CultureInfo.InvariantCulture);
					InitedPoint.Add(p.Key, tempObj);

					break;
				case MusicPointType.FwdTuneBegin:
					tempObj=InstaniateTuneType(FwdTune,p.Value.MusicPointV3,p.Value.MusicPointQ4);
                    tempObj.name = p.Key.ToString(CultureInfo.InvariantCulture);
					InitedPoint.Add(p.Key, tempObj);

					break;
         
			}


	        break;
	    }
    }

	private GameObject InstaniateTuneType(GameObject obj,Vector3 position,Quaternion roatation)
	{
		return (GameObject)Instantiate(obj,position,roatation);
	}
		
	public void PlayCurrentSong()
	{
        //SongAudioClip = (AudioClip)Resources.Load("Music/op");

        SongAudioSource.clip = SongAudioClip;
        SongAudioSource.loop = false;
			// 或存在音频，则开始播放
			if(SongAudioSource.clip!=null)
			{
				SongAudioSource.Play();
				GetCurrentAudioClipLength();
				IsSongPlaying=true;
				Log("音乐已经开始播放！");
			}
			else
				Debug.Log("未找到音乐");
	}

	public void StopCurrentSong()
	{
		if(SongAudioSource.clip!=null)
		{
			SongAudioSource.Stop();
			IsSongPlaying=false;
			Log("音乐已经停止！");
		}
		else
			Debug.Log("未找到音乐");
	}

	public void PauseCurrentSong()
	{
		if(SongAudioSource.clip!=null)
		{
			SongAudioSource.Pause();
			IsSongPlaying=false;
			Log("音乐已经暂停！");
		}
		else
			Debug.Log("未找到音乐");
	}

	/// <summary>
	/// 获取当前音频时间长
	/// </summary>
	/// <returns>The current audio clip length.</returns>
	public float GetCurrentAudioClipLength()
	{
		if(SongAudioSource.clip!=null)
		{
			CurrentAudioLength=SongAudioSource.clip.length;
		    Log(CurrentAudioLength);
		}
		else
			Debug.Log("未找到音乐");
		
		return CurrentAudioLength;
	}



    /// <summary>
    /// 平滑音频播放是否结束
    /// </summary>
    /// <returns></returns>
    public static bool IsSmoothAudioTimeOff(float smoothAudioTime)
    {
        //音频播放时间延迟
        if (smoothAudioTime < 0)
        {
            return false;
        }

        //检测平滑时间和实际播放时间是否差距在0.1之内
        return Mathf.Abs(smoothAudioTime - SongAudioSource.time) > 0.1f;
    }

    /// <summary>
    /// 校正音频播放时间
    /// </summary>
    public static void CorrectSmoothAudioTime(float smoothAudioTime)
    {
        smoothAudioTime = SongAudioSource.time;
    }
}