//-----------------------------------------------【Function Indroduction】----------------------------------------------
//	  YourBeatsController：  
//    Language：  C#
//    IDE：VS2013
//    2015.11.14  Created by RaymondMG  
//    2015.11.27  LastEdited by RaymondMG  
//---------------------------------------------------------------------------------------------------------------------
using thelab.mvc;
using UnityEngine;
using YourBeats.core.gamemode;

public class YourBeatsController : Controller<YourBeatsApplication> {


    public SongController Song { get { return _mSong = Assert<SongController>(_mSong); } }
    private SongController _mSong;

    public UIController UICon { get { return _UICon = Assert<UIController>(_UICon); } }
    private UIController _UICon;

        /// <summary>
        /// 处理应用程序发出的通知
        /// </summary>
        /// <param name="pEvent"></param>
        /// <param name="pTarget"></param>
        /// <param name="pData"></param>
        public override void OnNotification(string pEvent, Object pTarget, params object[] pData)
        {
            switch (pEvent)
            {
                case "scene.load":
                    Log("场景 [" + pData[0] + "][" + pData[1] + "] 已经读取");
                    break;
                case "game.awake.init":
                    //json数据初始化
                    //InitJsonData.GetInstance.ReadJsonFromFile("op", ref app.model.CurrentPlayingMusic);
                    //app.model.CurrentPlayingMusicPrefab = new MusicInfoPrefab
                    //{
                    //    MusicPointsDictionary = new System.Collections.Generic.Dictionary<string, MusicPointPrefab>()
                    //};
                    //awake初始化
                    GameMode.GetInstance.GameModeStateSwitch(GameModeType.Awake);

                    //初始化Song,ui
                    GameMode.GetInstance.Song = app.model.Song;
                    GameMode.GetInstance.UICon = UICon;

                    //初始化json
                    if (app.model.MusicButtonsInfo != null)
                    {
                        foreach (var b in app.model.MusicButtonsInfo)
                        {
                            b.LoadAllJsonFile();
                        }
                    }
                    break;
                case "game.start.init":
                    //start初始化
                    GameMode.GetInstance.GameModeStateSwitch(GameModeType.Start);

                    // 将json中信息转化为预制物信息
                    //CopyJsonInfoToPrebInfo();
                    
                    //debug调试信息读取是否成功
                    //Log("Start Log Json File------------------------");
                    //Log("音乐信息:" + app.model.CurrentPlayingMusicPrefab.MusicAuthorName + "\n" +
                    //    app.model.CurrentPlayingMusicPrefab.MusicDescription + "\n" +
                    //    app.model.CurrentPlayingMusicPrefab.MusicName + "\n" +
                    //    app.model.CurrentPlayingMusicPrefab.MusicPublishDate + "\n" +
                    //    app.model.CurrentPlayingMusicPrefab.MusicResourcesPath + "\n" +
                    //    app.model.CurrentPlayingMusicPrefab.MusicTime + "\n"+"所有鼓点信息如下");

                
//                    foreach (var p in app.model.CurrentPlayingMusicPrefab.MusicPointsDictionary)
//                    {
////                        Log("鼓点时间" + p.Key +"鼓点类型为:"+p.Value.MusicType+ "V3以及Q4" + p.Value.MusicPointV3 + "." + p.Value.MusicPointQ4);
//                        app.model.Song.AddPointType(float.Parse(p.Key),p.Value);
//                    }

//                    ScoreSystem.GetInstance.TotalTunes = app.model.Song.PointType.Count;
 //                   Log("End   Log Json File------------------------");

                    break;
                case "game.update":
                    //每帧进行检测

                    if (GameMode.GetInstance.GetCurrentGameMode() == GameModeType.Playing)
                    {
                        GameMode.GetInstance.GameModeStateSwitch(GameModeType.Playing);
                    }
                 
                    break;

            }
        }

    /// <summary>
    /// 将json中信息转化为预制物信息
    /// </summary>
    private void CopyJsonInfoToPrebInfo()
    {
        app.model.CurrentPlayingMusicPrefab.MusicAuthorName = app.model.CurrentPlayingMusic.MusicAuthorName;
        app.model.CurrentPlayingMusicPrefab.MusicDescription = app.model.CurrentPlayingMusic.MusicDescription;
        app.model.CurrentPlayingMusicPrefab.MusicName = app.model.CurrentPlayingMusic.MusicName;
        app.model.CurrentPlayingMusicPrefab.MusicPublishDate = app.model.CurrentPlayingMusic.MusicPublishDate;
        app.model.CurrentPlayingMusicPrefab.MusicResourcesPath = app.model.CurrentPlayingMusic.MusicResourcesPath;
        app.model.CurrentPlayingMusicPrefab.MusicTime = app.model.CurrentPlayingMusic.MusicTime;


        foreach (var m in app.model.CurrentPlayingMusic.MusicPointsDictionary)
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
            app.model.CurrentPlayingMusicPrefab.MusicPointsDictionary.Add(m.Key,mm);
        }
       
        
    }
}