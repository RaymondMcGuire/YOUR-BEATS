using UnityEngine;
using UnityEngine.UI;

public class RankField : MonoBehaviour 
{
	public Sprite[] rankSprites;

	public GameObject RankImage;

	public SongModel songModel;

	void OnEnable()
	{

		RankImage=this.transform.GetChild(0).gameObject;
	}

	public void SetRankImage(RankTypeEnum rankTypeEnum)
	{
		RankImage.GetComponent<Image>().sprite=rankSprites[(int)rankTypeEnum];
	}


}
