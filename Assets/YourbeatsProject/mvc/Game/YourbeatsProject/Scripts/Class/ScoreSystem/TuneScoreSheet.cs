//-----------------------------------------------【Function Indroduction】----------------------------------------------
//	  YourBeats：  TuneScoreSheet  音符得分/计分参照表
//    Language：  C#
//    IDE：VS2013
//    2015.11.27  Created by 茅炜  
//    2015.11.27  LastEdited by Raymond  
//---------------------------------------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Data;
using System;
using System.Globalization;

public class TuneScoreSheet :SingletonBase<TuneScoreSheet>
{
    //建立在内存中的 音符-分数 对照表
	private DataTable _referenceDataTable;				

    /// <summary>
    /// 使用DataTable建立数据表,这个函数被下面的初始化函数调用
    /// </summary>
	private void SetupReferenceDataTable()				
	{
		_referenceDataTable=new DataTable();

		_referenceDataTable.Columns.Add("TunesType",typeof(string));

		foreach(var name in Enum.GetNames(typeof(JudgementEnum)))
		{
			_referenceDataTable.Columns.Add(name,typeof(int));
		}

		var singleTune=_referenceDataTable.NewRow();
		singleTune["TunesType"]="SingleTune";
		singleTune["Prefect"]=100;
		singleTune["Great"]=80;
		singleTune["Cool"]=70;
		singleTune["Poor"]=60;
		singleTune["Miss"]=0;
		_referenceDataTable.Rows.Add(singleTune);

		var serialTune=_referenceDataTable.NewRow();
		serialTune["TunesType"]="SerialTune";
		serialTune["Prefect"]=100;
		serialTune["Great"]=75;
		serialTune["Cool"]=65;
		serialTune["Poor"]=55;
		serialTune["Miss"]=0;
		_referenceDataTable.Rows.Add(serialTune);

		var fwdTune=_referenceDataTable.NewRow();
		fwdTune["TunesType"]="FwdTune";
		fwdTune["Prefect"]=100;
		fwdTune["Great"]=85;
		fwdTune["Cool"]=75;
		fwdTune["Poor"]=65;
		fwdTune["Miss"]=0;
		_referenceDataTable.Rows.Add(fwdTune);

		var bakTune=_referenceDataTable.NewRow();
		bakTune["TunesType"]="BakTune";
		bakTune["Prefect"]=100;
		bakTune["Great"]=85;
		bakTune["Cool"]=75;
		bakTune["Poor"]=65;
		bakTune["Miss"]=0;
		_referenceDataTable.Rows.Add(bakTune);

//		foreach(string name in Enum.GetNames(typeof(TunesTypeEnum)))
//		{
//			DataRow row=ReferenceDataTable.NewRow();
//			ReferenceDataTable.Rows.Add(row);
//		}

	}

    /// <summary>
    /// 用于读取相应记分,用法实例：TuneScoreSheet.instance.ReadScore(TunesTypeEnum.FwdTune,JudgementEnum.Great)
    /// </summary>
    /// <param name="tunesType"></param>
    /// <param name="judgementType"></param>
    /// <returns></returns>
	public string ReadScore(TunesTypeEnum tunesType,JudgementEnum judgementType)	
	{
	//	return ReferenceDataTable.Rows[1]["Cool"].ToString();
		var score= _referenceDataTable.Rows[(int)tunesType][judgementType.ToString()].ToString();
        return score;
	}

    /// <summary>
    /// 重载：用于读取相应记分,并且以特定秒数做记分标准
    /// </summary>
    /// <param name="tunesType"></param>
    /// <param name="judgementType"></param>
    /// <param name="playerSerialTime"></param>
    /// <param name="judgeTime"></param>
    /// <returns></returns>
	public string ReadScore(TunesTypeEnum tunesType,JudgementEnum judgementType,float playerSerialTime,float judgeTime=0.5f)
    {
        //	return ReferenceDataTable.Rows[1]["Cool"].ToString();
		var score= _referenceDataTable.Rows[(int)tunesType][judgementType.ToString()].ToString();
        var Score = Convert.ToInt32(score);
        {
            return (Ceiling(playerSerialTime*2,judgeTime)/2*Score).ToString();
        }
    }

    /// <summary>
    /// 初始化函数,需要被首先调用
	/// </summary>
	public void InitDataSheet()		
	{
		SetupReferenceDataTable();
	}

    /// <summary>
    /// 向上取数函数 参数1：音符持续时长 参数2：记分标准时间
    /// </summary>
    /// <param name="playerSerialTime"></param>
    /// <param name="judgeTime"></param>
    /// <returns></returns>
	private static float Ceiling(float playerSerialTime,float judgeTime)	
	{
		if(playerSerialTime>0 && judgeTime>0)
		{
			return playerSerialTime+judgeTime-playerSerialTime%judgeTime;
		}
		return 0;
	}



//	private TunesTypeEnum SingleTuneType=TunesTypeEnum.SingleTune;
//
//	private JudgementEnum judgement=JudgementEnum.Prefect;
//
//	private Dictionary<Dictionary<TunesTypeEnum,JudgementEnum>,int> ReferenceRelation=		//字典键：音符类型-判定偏差
//		new Dictionary<Dictionary<TunesTypeEnum, JudgementEnum>,int>();						//字典值：对应得分
//
//	public void LoadTuneScoreSheet()
//	{
//		SetupReferenceRelation();
//	}
//
//	private void SetupReferenceRelation()
//	{
		//single tune
//		ReferenceRelation.Add(new Dictionary<TunesTypeEnum.SingleTune,JudgementEnum.Prefect>(),100);
//		ReferenceRelation.Add(new Dictionary<SingleTuneType,judgement>(),100);
//		ReferenceRelation.Add(TunesTypeEnum.SingleTune,JudgementEnum.Great,80);
//		ReferenceRelation.Add(TunesTypeEnum.SingleTune,JudgementEnum.Cool,70);
//		ReferenceRelation.Add(TunesTypeEnum.SingleTune,JudgementEnum.Poor,60);
//		ReferenceRelation.Add(TunesTypeEnum.SingleTune,JudgementEnum.Miss,0);
//		//serial tune
//		ReferenceRelation.Add(TunesTypeEnum.SerialTune,JudgementEnum.Prefect,100);
//		ReferenceRelation.Add(TunesTypeEnum.SerialTune,JudgementEnum.Great,75);
//		ReferenceRelation.Add(TunesTypeEnum.SerialTune,JudgementEnum.Cool,65);
//		ReferenceRelation.Add(TunesTypeEnum.SerialTune,JudgementEnum.Poor,55);
//		ReferenceRelation.Add(TunesTypeEnum.SerialTune,JudgementEnum.Miss,0);
//		//fwd tune
//		ReferenceRelation.Add(TunesTypeEnum.FwdTune,JudgementEnum.Prefect,100);
//		ReferenceRelation.Add(TunesTypeEnum.FwdTune,JudgementEnum.Great,85);
//		ReferenceRelation.Add(TunesTypeEnum.FwdTune,JudgementEnum.Cool,75);
//		ReferenceRelation.Add(TunesTypeEnum.FwdTune,JudgementEnum.Poor,65);
//		ReferenceRelation.Add(TunesTypeEnum.FwdTune,JudgementEnum.Miss,0);
//		//bak tune
//		ReferenceRelation.Add(TunesTypeEnum.BakTune,JudgementEnum.Prefect,100);
//		ReferenceRelation.Add(TunesTypeEnum.BakTune,JudgementEnum.Great,85);
//		ReferenceRelation.Add(TunesTypeEnum.BakTune,JudgementEnum.Cool,75);
//		ReferenceRelation.Add(TunesTypeEnum.BakTune,JudgementEnum.Poor,65);
//		ReferenceRelation.Add(TunesTypeEnum.BakTune,JudgementEnum.Miss,0);
//
//	}


}
