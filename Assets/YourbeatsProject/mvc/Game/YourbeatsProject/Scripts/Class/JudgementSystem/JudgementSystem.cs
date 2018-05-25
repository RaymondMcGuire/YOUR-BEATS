//-----------------------------------------------【Function Indroduction】----------------------------------------------
//	  JudgementSystem：  精准度判定系统
//    Language：  C#
//    IDE：VS2013
//    2015.11.27  Created by 史礼华  
//    2015.11.27  LastEdited by RaymondMG  
//---------------------------------------------------------------------------------------------------------------------

using System;
using UnityEngine;

public class JudgementSystem : SingletonBase<JudgementSystem>
{

    double _judgementSigma;

    /// <summary>
    /// 初始化判定系统
    /// </summary>
    public void InitJudgementSystem()
    {
        Debug.Log("判定系统初始化完成");
    }
 

    /// <summary>
    /// 判定是否击中
    /// </summary>
    /// <param name="insideNote">在音符内</param>
    /// <param name="beatsSigma">节奏偏移</param>
    /// <param name="codeBias">代码运算调整值</param>
    /// <returns>0=Perfect 1=Great 2=Cool 3=Poor 4=Miss</returns>
//    public int Judgement(bool insideNote, double beatsSigma,double codeBias)
//    {
//        if (insideNote != true) return 4;
//
//        _judgementSigma = System.Math.Abs(beatsSigma - 1) - codeBias;
//
//        if (_judgementSigma <= 0.2)
//            return 0;
//        if (_judgementSigma <= 0.4)
//            return 1;
//        if (_judgementSigma <= 0.6)
//            return 2;
//        return _judgementSigma <= 1 ? 3 : 4;
//    }

	public JudgementEnum Judgement(bool insideNote, double beatsSigma,double codeBias)
	{
		if (insideNote != true) return JudgementEnum.Miss;

		_judgementSigma = System.Math.Abs(beatsSigma - 1) - codeBias;

		if (_judgementSigma <= 0.2)
			return JudgementEnum.Prefect;
		if (_judgementSigma <= 0.4)
			return JudgementEnum.Great;
		if (_judgementSigma <= 0.6)
			return JudgementEnum.Cool;
		return _judgementSigma <= 1 ? JudgementEnum.Poor : JudgementEnum.Miss;
	}
}
