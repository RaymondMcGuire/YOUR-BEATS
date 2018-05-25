//-----------------------------------------------【Function Indroduction】----------------------------------------------
//	  YourBeats：  ScoreSystem  记分系统
//    Language：  C#
//    IDE：VS2013
//    2015.11.27  Created by 茅炜  
//    2015.11.27  LastEdited by RaymondMG  
//---------------------------------------------------------------------------------------------------------------------

using System;
using UnityEngine;


public class ScoreSystem : SingletonBase<ScoreSystem> 
{
	//分数参数访问控制
    public int TotalTunes { get; set; }

    public float TotalScore { get; private set; }

    public float CurrentScore { get; private set; }

    public float CurrentComboScore { get; set; }

    //private void Awake()
    //{
    //    InitScoreSystem();
    //}

    /// <summary>
    /// 初始化计分系统
    /// </summary>
    public void InitScoreSystem()
	{
		TotalScore=0;

		CurrentScore=0;

		CurrentComboScore=0;

        Debug.Log("计分系统初始化完成");

	}

	public float CalculateCurrentScore(float perTuneScore)
	{
		CurrentScore+=perTuneScore;

		return CurrentScore;
	}

	public float CalculateCurrentComboScore(int maxCombo,int totalTunes)
	{
		CurrentComboScore=(maxCombo/totalTunes)*CurrentScore;

		return CurrentComboScore;
	}

	public float CalculateTotalScore()
	{
		TotalScore=CurrentScore+CurrentComboScore;

		return TotalScore;
	}
}
