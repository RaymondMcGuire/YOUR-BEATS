using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using thelab.mvc;

public class LoadExistingBeats : MonoBehaviour,ISelectHandler
{
	public string MusicName;

	public string MusicPath;

	public string JsonPath;


    [HideInInspector]
	public YourBeatsModel yourBeatsModel;
    [HideInInspector]
	public SongModel songModel;



    /// <summary>
    /// 读取当前按钮json文件信息
    /// </summary>
    public void LoadAllJsonFile()
    {


        yourBeatsModel = GameObject.FindObjectOfType<YourBeatsModel>();

        var currentMusic = new MusicInfo();

        InitJsonData.GetInstance.ReadJsonFromFile(JsonPath, ref currentMusic);

        yourBeatsModel.PlayingMusicDictionary.Add(MusicName, currentMusic);

    }


    //当某个按钮被选择的时候
	public void OnSelect(BaseEventData p)
	{
        //切换音乐
        SongModel.SongAudioClip = (AudioClip)Resources.Load(MusicPath);

	    yourBeatsModel.CurrentPlayingMusic = yourBeatsModel.PlayingMusicDictionary[MusicName];

		yourBeatsModel.CurrentPlayingMusicPrefab = new MusicInfoPrefab
		{
			MusicPointsDictionary = new System.Collections.Generic.Dictionary<string, MusicPointPrefab>()
		};

	    yourBeatsModel.Song.ClearPointTypeDictionary();
	    yourBeatsModel.CurrentPlayingMusicPrefab.MusicPointsDictionary.Clear();
		CopyJsonInfoToPrebInfo();
	}

	private void CopyJsonInfoToPrebInfo()
	{
        yourBeatsModel.CurrentPlayingMusicPrefab.MusicAuthorName = yourBeatsModel.CurrentPlayingMusic.MusicAuthorName;
		yourBeatsModel.CurrentPlayingMusicPrefab.MusicDescription = yourBeatsModel.CurrentPlayingMusic.MusicDescription;
		yourBeatsModel.CurrentPlayingMusicPrefab.MusicName = yourBeatsModel.CurrentPlayingMusic.MusicName;
		yourBeatsModel.CurrentPlayingMusicPrefab.MusicPublishDate = yourBeatsModel.CurrentPlayingMusic.MusicPublishDate;
		yourBeatsModel.CurrentPlayingMusicPrefab.MusicResourcesPath = yourBeatsModel.CurrentPlayingMusic.MusicResourcesPath;
		yourBeatsModel.CurrentPlayingMusicPrefab.MusicTime = yourBeatsModel.CurrentPlayingMusic.MusicTime;


		foreach (var m in yourBeatsModel.CurrentPlayingMusic.MusicPointsDictionary)
		{
			var mm = new MusicPointPrefab
			{
				MusicType = m.Value.MusicType,
				MusicPointV3 =
					new Vector3((float) m.Value.MusicPointV3X, (float) m.Value.MusicPointV3Y,
						(float) m.Value.MusicPointV3Z),
				MusicPointQ4 =
					new Quaternion((float) m.Value.MusicPointQ4X, (float) m.Value.MusicPointQ4Y,
						(float) m.Value.MusicPointQ4Z, (float) m.Value.MusicPointQ4W)
				};

            if (!yourBeatsModel.CurrentPlayingMusicPrefab.MusicPointsDictionary.ContainsKey(m.Key))
			yourBeatsModel.CurrentPlayingMusicPrefab.MusicPointsDictionary.Add(m.Key,mm);
		}

		foreach (var p in yourBeatsModel.CurrentPlayingMusicPrefab.MusicPointsDictionary)
		{
			yourBeatsModel.Song.AddPointType(float.Parse(p.Key),p.Value);
		}

		songModel=GameObject.FindObjectOfType<SongModel>();

		var totalTunes=ScoreSystem.GetInstance.TotalTunes=songModel.PointType.Count;

		RankSystem.GetInstance.CalculateCurrentSongTotalScore(totalTunes);
	}
}
