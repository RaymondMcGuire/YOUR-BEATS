//-----------------------------------------------【Function Indroduction】----------------------------------------------
//	  YourBeats：  RankSystem  评价系统
//    Language：  C#
//    IDE：VS2013
//    2015.11.27  Created by 茅炜  
//    2015.11.27  LastEdited by RaymondMG  
//---------------------------------------------------------------------------------------------------------------------

public class RankSystem : SingletonBase<RankSystem> 
{
	public float CurrentSongtotalScore;

	public void CalculateCurrentSongTotalScore(int totalTunes)
	{
		CurrentSongtotalScore=totalTunes*100;
	}

	public RankTypeEnum CalculateRank(float actualScore)
	{
		float rankScale=actualScore/CurrentSongtotalScore;

		if(rankScale>=0 && rankScale <=0.5f)
		{
			return RankTypeEnum.F;
		}
		else if(rankScale >0.5f && rankScale <=0.6f)
		{
			return RankTypeEnum.D;
		}
		else if(rankScale >0.6f && rankScale <=0.7f)
		{
			return RankTypeEnum.C;
		}
		else if(rankScale >0.7f && rankScale <=0.8f)
		{
			return RankTypeEnum.B;
		}
		else if(rankScale >0.8f && rankScale <=0.9f)
		{
			return RankTypeEnum.A;
		}
		else if(rankScale >0.9f && rankScale <=0.95f)
		{
			return RankTypeEnum.S;
		}
		else if(rankScale >0.95f && rankScale <=1f)
		{
			return RankTypeEnum.SS;
		}

		return RankTypeEnum.F;


	}
		

}

