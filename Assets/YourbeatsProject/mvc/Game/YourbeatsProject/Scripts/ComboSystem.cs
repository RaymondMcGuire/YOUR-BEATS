using UnityEngine;
using System.Collections.Generic;
using thelab.mvc;

public class ComboSystem : SingletonBase<ComboSystem>
{
	public int currentCombo;

	public int maxCombo;

//	public SongModel song;
//
//	//最近一次击中的音符
//	public Dictionary<float,MusicPointPrefab> RecentHitTune =new Dictionary<float, MusicPointPrefab>();
//
//	public Dictionary<float,MusicPointPrefab> PreviousTune =new Dictionary<float, MusicPointPrefab>();

//	public void InitComboSystem()
//	{
//		currentCombo=0;
//
//		maxCombo=0;
//
//		song=GameObject.FindObjectOfType<SongModel>();
//	}
//
//	public void AddCurrentCombo(float time,MusicPointPrefab prefab)
//	{
//		if(song.PointType.ContainsKey(time))
//		{
//			Debug.Log("true,contain");
//			//索引
//			int i=0;
//
//			//先获取本次音符的索引
//			foreach(float t in song.PointType.Keys)
//			{
//				if(t==time) break;
//
//				i++;
//			}
//
//			//获取上一个音符,加入上一个音符字典
//			if(i>0 && i<=song.PointType.Count)
//			{
//				PreviousTune.Clear();
//				PreviousTune.Add(song.PointType.ElementAt(i-1).Key,song.PointType.ElementAt(i-1).Value);
//			}
//			else if(i==0)
//			{
//				PreviousTune.Clear();
//				PreviousTune.Add(song.PointType.ElementAt(i).Key,song.PointType.ElementAt(i).Value);
//			}
//
//			//比较
//			if(PreviousTune.ElementAt(0).Key==RecentHitTune.ElementAt(0).Key && PreviousTune.ElementAt(0).Value==RecentHitTune.ElementAt(0).Value)
//			{
//				currentCombo++;
//				Debug.Log(currentCombo);
//			}
//			else
//				ResetCurrentCombo();
//
//		}
//
//		SetRecentHitTune(time,prefab);	
//	}
//
//	public void ResetCurrentCombo()
//	{
//		currentCombo=0;
//	}
//
//	public void SetRecentHitTune(float time,MusicPointPrefab prefab)
//	{
//		RecentHitTune.Clear();
//	}

	public void InitComboSystem()
	{
		currentCombo=0;

		maxCombo=0;

		maxCombo=currentCombo;
	}

	public void AddCurrentCombo()
	{
		currentCombo++;

		OnCurrentComboChanged();

		if(currentCombo>maxCombo)
		{
			maxCombo=currentCombo;

			OnMaxComboChanged();
		}
	}

	public void ResetCurrentCombo()
	{
		currentCombo=0;

		OnCurrentComboChanged();
	}

	public void OnCurrentComboChanged()
	{
		GameObject.FindObjectOfType<ComboScoreField>().UpdateScore(currentCombo);
	}

	public void OnMaxComboChanged()
	{
		GameObject.FindObjectOfType<MaxComboScoreField>().UpdateScore(maxCombo);
	}
}
	