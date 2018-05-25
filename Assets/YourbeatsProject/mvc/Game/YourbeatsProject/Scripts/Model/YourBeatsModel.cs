//-----------------------------------------------【Function Indroduction】----------------------------------------------
//	  YourBeatsModel：  
//    Language：  C#
//    IDE：VS2013
//    2015.11.14  Created by RaymondMG  
//    2015.11.27  LastEdited by RaymondMG  
//---------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using thelab.mvc;
using UnityEngine;


public class YourBeatsModel : Model<YourBeatsApplication>
{
    //歌曲
    public SongModel Song { get { return _mSong = Assert<SongModel>(_mSong); } }
    private SongModel _mSong;
    //UI
    public UIModel ui { get { return _ui = Assert<UIModel>(_ui); } }
    private UIModel _ui;

    public MusicInfo CurrentPlayingMusic;

    public MusicInfoPrefab CurrentPlayingMusicPrefab;

    public Dictionary<string, MusicInfo> PlayingMusicDictionary = new Dictionary<string, MusicInfo>();

    public LoadExistingBeats[] MusicButtonsInfo;

    //PC相机
    public FPmodel FpModel { get { return _fpModel = Assert<FPmodel>(_fpModel); } }
    private FPmodel _fpModel;

    //主相机
    public Camera MainCamera { get { return Camera.main; } }
}

