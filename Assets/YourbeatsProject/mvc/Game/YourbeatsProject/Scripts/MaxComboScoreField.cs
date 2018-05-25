using UnityEngine;
using UnityEngine.UI;

public class MaxComboScoreField : MonoBehaviour 
{
	//分数的父物体
	public GameObject ScoreTextParent;

	//单个分数物体的预制
	public GameObject ScoreTextImageObject;

	//分数图片
	public Sprite [] ScoreSprites;

	public bool AlreadyInited=false;

	void OnEnable()
	{
		//如果没有初始化过，那么初始化，如果此次游戏中已经初始化过，那么重置
		if(!AlreadyInited)
		{
			InitScoreField();

			AlreadyInited=true;
		}
		else
			ResetScoreField();
	}

	void OnDisable()
	{
		ClearAllScoreTextObject();
	}

	//初始化分数显示区
	public void InitScoreField()
	{
		ScoreTextParent=this.gameObject;

		ScoreTextImageObject=(GameObject)Resources.Load("Prefabs/ScoreTextImage");

		AddScoreTextObject();

	}

	//在父物体下添加一个分数预制物件
	public GameObject AddScoreTextObject()
	{
		if(ScoreTextParent!=null && ScoreTextImageObject !=null)
		{
			GameObject temp=Instantiate(ScoreTextImageObject) as GameObject;

			temp.transform.SetParent(ScoreTextParent.transform);

			temp.transform.localPosition=Vector3.zero;

			temp.transform.localRotation=ScoreTextParent.transform.localRotation;

			SetScoreText(temp,0);

			return temp;
		}
		return null;
	}

	//设置某个分数预制物件的图片（分数）
	public void SetScoreText(GameObject ScoreTextObject,int num)
	{
		if(num>=0 && num <=9)
		{

			ScoreTextObject.GetComponent<NumberProfile>().SelfNumber=num;
				
			ScoreTextObject.GetComponent<Image>().sprite=ScoreSprites[num];

		}
	}

	//将父物体下的所有分数物体清除
	public void ClearAllScoreTextObject()
	{
		foreach(Transform child in ScoreTextParent.transform)
		{
			Destroy(child.gameObject);
		}
	}

	//重置分数显示区域
	public void ResetScoreField()
	{
		ClearAllScoreTextObject();

		AddScoreTextObject();
	}

	//游戏中更新当前显示分数
	public void UpdateScore(float score)
	{
		if(score<=99999 && score >0)
		{
			ClearAllScoreTextObject();

			int intScore=System.Convert.ToInt32(score);

			int num;

			while(intScore!=0)
			{
				num=intScore%10;

				intScore/=10;

				GameObject temp=AddScoreTextObject();

				SetScoreText(temp,num);
			}

		}
	}
}
