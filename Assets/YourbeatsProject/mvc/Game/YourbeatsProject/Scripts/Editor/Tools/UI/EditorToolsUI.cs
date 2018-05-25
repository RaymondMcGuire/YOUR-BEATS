//-----------------------------------------------【Function Indroduction】----------------------------------------------
//	  EditorToolsUI：  
//    Language：  C#
//    IDE：VS2013
//    2015.12.04  Created by RaymondMG  
//    2015.12.04  LastEdited by RaymondMG  
//---------------------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace EncognitaTools
{

    /// <summary>
    /// 工具菜单中UI界面
    /// </summary>
    public class EditorToolsUI : EditorWindow
    {

        private static EditorToolsUI _instance;

        /// <summary>
        /// 获取单例
        /// </summary>
        /// <returns></returns>
        public static EditorToolsUI GetInstance()
        {
            return _instance ?? (_instance = new EditorToolsUI());
        }

        /// <summary>
        /// 初始化EditorUI
        /// </summary>
        public void Init()
        {
            //创建窗口
            var wr = new Rect(0, 0, 500, 500);
            var window =
                (EditorToolsUI) EditorWindow.GetWindowWithRect(typeof (EditorToolsUI), wr, true, "EncognitaTools");
            window.Show();

            //data
            DataInit();


            //DataTest();

        }

        /// <summary>
        /// 数据测试
        /// </summary>
        private void DataTest()
        {
            TimeArray = new List<float>();
            TimeArray.Add(0.5f);
            TimeArray.Add(3);
            TimeArray.Add(4); TimeArray.Add(5);
            TimeArray.Add(8);
            TimeArray.Add(12);
            TimeArray.Add(14);
            TimeArray.Add(19);
            TimeArray.Add(19.3f); TimeArray.Add(22);
            TimeArray.Add(23);
            TimeArray.Add(25);
            TimeArray.Add(28);

            AnalysisTuneToType();

        }

        /// <summary>
        /// 数据初始化
        /// </summary>
        private void DataInit()
        {
            Music = new MusicInfo();
            MusicConfigObj = GameObject.FindWithTag("MusicConfig");
            Music.MusicPointsDictionary =
            new System.Collections.Generic.Dictionary<string, MusicPoint>();
        }


        public int ToolbarOption = 0;
        public string[] ToolbarTexts = {"1.乐曲基本信息", "2.配置乐曲鼓点时间戳","3.生成存储音乐信息的json文件","4.还原音乐json文件物体"};
        //音乐
        public MusicInfo Music;
        //配置音乐物体
        public GameObject MusicConfigObj;
        //音乐json文件命名
        public string MusicJsonName;
        //音乐
        public AudioClip MusicClip;
        //音乐JSON文件
        public TextAsset JsonFile;
        //时间数组
        public List<float> TimeArray;
        //时间标记数组
        public List<float> TimeFlagArray;

        public Dictionary<float, MusicPointType> TimeDictionary=new Dictionary<float,MusicPointType>();

        //绘制界面
        void OnGUI()
        {
            ToolbarOption = GUILayout.Toolbar(ToolbarOption, ToolbarTexts);
            switch (ToolbarOption)
            {
                case 0:
                    if (Music == null) DataInit();

                     EditorGUILayout.LabelField("音乐名称");
                     Music.MusicName=EditorGUILayout.TextField(Music.MusicName);
                     EditorGUILayout.LabelField("音乐作者名称");
                     Music.MusicAuthorName=EditorGUILayout.TextField(Music.MusicAuthorName);
                     EditorGUILayout.LabelField("音乐发布日期");
                     Music.MusicPublishDate=EditorGUILayout.TextField(Music.MusicPublishDate);
                     EditorGUILayout.LabelField("音乐信息描述");
                     Music.MusicDescription=EditorGUILayout.TextField(Music.MusicDescription);
                     EditorGUILayout.LabelField("音乐路径");
                     Music.MusicResourcesPath=EditorGUILayout.TextField(Music.MusicResourcesPath);
                    break;
                case 1:
                   

                    //选择音乐
                    MusicClip = EditorGUILayout.ObjectField("选择音乐", MusicClip, typeof(AudioClip), true) as AudioClip;

                    if (MusicClip == null) return;

                    if (GUILayout.Button("播放音乐并开始设置节拍点（点击下方按钮记录时间戳）", GUILayout.Width(200)))
                    {
                        if (_isPlayingMusic) return;
                        StartRecordMusicBPM();
                    }


                    if (GUILayout.Button("记录当前时间戳", GUILayout.Width(200)))
                    {
                        if (!_isPlayingMusic) return;


                        _currentsmoothaudio = _smoothAudioTime;
                        TimeArray.Add(_currentsmoothaudio);
                        Debug.Log("记录当前时间戳为" + _currentsmoothaudio);
                    }

                    if (GUILayout.Button("X", GUILayout.Width(200)))
                    {
                        TimeFlagArray.Add(_currentsmoothaudio);
                    }

                    break;
                case 2:
                    if (GUILayout.Button("生成音乐鼓点json格式文件", GUILayout.Width(200)))
                    {

                        if (Music == null) return;
                        //音乐时间长度
                        Music.MusicTime = MusicConfigObj.transform.GetComponent<AudioSource>().clip.length;
                        //音乐json格式文件命名， 暂时按照音乐名 
                        MusicJsonName = Music.MusicName;

                        //添加节拍以及对应的鼓点类型
                        foreach (Transform tr in MusicConfigObj.transform)
                        {
                            var musicPoint = new MusicPoint
                            {
                                MusicPointV3X = tr.position.x,
                                MusicPointV3Y = tr.position.y,
                                MusicPointV3Z = tr.position.z,
                                MusicPointQ4X = tr.rotation.x,
                                MusicPointQ4Y = tr.rotation.y,
                                MusicPointQ4Z = tr.rotation.z,
                                MusicPointQ4W = tr.rotation.w,
                            };
                            switch (tr.tag)
                            {
                                case "SingleTune":
                                    musicPoint.MusicType = MusicPointType.SingleTune;
                                    break;
                                case "SerialTuneBegin":
                                    musicPoint.MusicType = MusicPointType.SerialTuneBegin;
                                    break;
                                case "SerialTuneEnd":
                                    musicPoint.MusicType = MusicPointType.SerialTuneEnd;
                                    break;
                                case "FwdTuneBegin":
                                    musicPoint.MusicType = MusicPointType.FwdTuneBegin;
                                    break;
                                case "FwdTuneEnd":
                                    musicPoint.MusicType = MusicPointType.FwdTuneEnd;
                                    break;
                                case "BakTuneBegin":
                                    musicPoint.MusicType = MusicPointType.BakTuneBegin;
                                    break;
                                case "BakTuneEnd":
                                    musicPoint.MusicType = MusicPointType.BakTuneEnd;
                                    break;
                            }

                            Music.MusicPointsDictionary.Add(tr.name, musicPoint);
                        }

                        this.ShowNotification(JsonOperation.GetInstance.SaveJsonDataToFile(MusicJsonName, Music)
                            ? new GUIContent("生成json文件成功")
                            : new GUIContent("生成json文件失败"));
                    }
                    break;
                case 3:


                    JsonFile = EditorGUILayout.ObjectField("音乐Json文件", JsonFile, typeof(TextAsset), true) as TextAsset;


                    if (JsonFile == null) return;

                    if (GUILayout.Button("还原预制物配置鼓点", GUILayout.Width(200)))
                    {
                        var jsonPath = JsonFile.name;

                        var currentMusic = new MusicInfo();

                        InitJsonData.GetInstance.ReadJsonFromFile(jsonPath, ref currentMusic);

                        CopyJsonInfoToPrebInfoAndInit(currentMusic);
                    }
                    break;
            }
        }


   private void CopyJsonInfoToPrebInfoAndInit(MusicInfo currentPlayingMusic)
   {
       var dictionary = new Dictionary<float, MusicPointPrefab>();

       foreach (var m in currentPlayingMusic.MusicPointsDictionary)
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
		    dictionary.Add(float.Parse(m.Key), mm);

		}

       CreateObjectToScene(dictionary);
   }

        //每一帧检测更新
        void Update()
        {
                  
            //正在播放时，检测
            if (!_isPlayingMusic) return;

            _isPlayingMusic = true;

            UpdateSmoothAudioTime();
        }

        //当前是否在播放音乐
        private bool _isPlayingMusic = false;
        private float _smoothAudioTime = 0f;
        private static float _currentsmoothaudio = 0;
        
        /// <summary>
        /// 开始记录播放音乐的节拍
        /// </summary>
        private void StartRecordMusicBPM()
        {
            //重置所有信息
            ResetAllDataInformation();

            //初始化播放器
            InitAudioSource();

            //播放音乐
            PlayMusic();
        }

        /// <summary>
        /// 播放音乐
        /// </summary>
        private void PlayMusic()
        {
            Debug.Log("音乐时长" + MusicConfigObj.transform.GetComponent<AudioSource>().clip.length);

            _isPlayingMusic = true;

            MusicConfigObj.GetComponent<AudioSource>().Play();
            _smoothAudioTime = MusicConfigObj.GetComponent<AudioSource>().time;
        }

        /// <summary>
        /// 重置所有数据信息
        /// </summary>
        private void ResetAllDataInformation()
        {
            _isPlayingMusic = false;
            TimeArray = new List<float>();
            TimeFlagArray = new List<float>();
            TimeArray.Clear();
            TimeFlagArray.Clear();
        }

        /// <summary>
        /// 初始化播放器
        /// </summary>
        private void InitAudioSource()
        {
            if (!MusicClip) return;
            MusicConfigObj.GetComponent<AudioSource>().clip = MusicClip;
        }

        /// <summary>
        /// 平滑音频播放时间
        /// </summary>
        protected void UpdateSmoothAudioTime()
        {
            //使用平滑的音频时间，因为音频时间有较小的偏差，因此，将音频超出时间进行控制
            _smoothAudioTime += Time.deltaTime;

            //音乐播放完毕
            if (_smoothAudioTime >= MusicConfigObj.GetComponent<AudioSource>().clip.length-0.5f)
            {
                _isPlayingMusic = false;
            
                Debug.Log("播放结束");
                _smoothAudioTime = MusicConfigObj.GetComponent<AudioSource>().clip.length;
                OnSongStopped();
            }
            //有时音频会发生跳跃或滞后情况，保证在发生这些情况时进行校正
            if (IsSmoothAudioTimeOff())
            {
                CorrectSmoothAudioTime();
            }
        }

        /// <summary>
        /// 音频播放结束
        /// </summary>
        protected void OnSongStopped()
        {
            if (!MusicConfigObj.GetComponent<AudioSource>().clip)
            {
                return;
            }

           
            _isPlayingMusic = false;
            


         
            Debug.Log("音乐鼓点时间戳记录完毕！");
            //分析鼓点对应音符
            AnalysisTuneToType();

            //生成物体
            CreateObjectToScene();
        }

        /// <summary>
        /// 分析鼓点对应音符
        /// </summary>
        private void AnalysisTuneToType()
        {
            TimeDictionary = new Dictionary<float, MusicPointType>();

            if (TimeArray.Count == 0) return;

            var count = 0;

            //3个音符两两取间隔，对比两个间隔<3时间差>2,将时间间隔长的一部分的起始设置为前仰,>3后仰，其余为单音

            float interval = 0;

            //上一个时间
            float lastTime = 0;
            float currentTime = 0;
            interval = Mathf.Abs(currentTime - currentTime);
            //当前时间
            foreach (float t in TimeArray)
            {
               

                currentTime=t;

                var curInterval = Mathf.Abs(lastTime - currentTime);

                if (curInterval != 0)
                {
                    //差
                    var minusInterval = Mathf.Abs(interval - curInterval);
                    //处理

                    if (++count >= 3)
                    {

                        if (minusInterval >= 1 && minusInterval <= 3)
                        {
                            TimeDictionary.Add(t, MusicPointType.FwdTuneBegin);
                        }
                        else if (minusInterval > 3)
                        {
                            TimeDictionary.Add( t, MusicPointType.BakTuneBegin);
                        }
                        else
                        {
                            TimeDictionary.Add( t, MusicPointType.SingleTune);
                        }

                        Debug.Log(minusInterval);
                    }
                    else
                    {
                        TimeDictionary.Add(t, MusicPointType.SingleTune);
                    }
                    //重新赋值
                    interval = Mathf.Abs(lastTime - currentTime);
                }

                lastTime =  t;

            }


        }

        /// <summary>
        /// 场景生成物体
        /// </summary>
        private void CreateObjectToScene()
        {
            if (TimeArray.Count == 0) return;

            foreach (var t in TimeDictionary)
            {
                var o = new GameObject();
                var obj = (GameObject)Instantiate(o,Vector3.zero, Quaternion.identity);

                if (TimeFlagArray.Contains(t.Key))
                    obj.name = t.Key.ToString()+"X";
                else
                    obj.name = t.Key.ToString();
                obj.transform.parent = MusicConfigObj.transform;


                var SingleTune = Resources.Load<GameObject>("Prefabs/SingleTune");

                var FwdTune = Resources.Load<GameObject>("Prefabs/FwdTune");

                var BakTune = Resources.Load<GameObject>("Prefabs/BwdTune");

                switch (t.Value)
                {
                    case MusicPointType.SingleTune:
                        obj.transform.tag = "SingleTune";
                        var oobj = (GameObject)Instantiate(SingleTune, Vector3.zero, Quaternion.identity);
                        oobj.transform.parent = obj.transform;
                        break;
                    case MusicPointType.SerialTuneEnd:
                        obj.transform.tag = "SerialTuneEnd";
                        break;
                    case MusicPointType.SerialTuneBegin:
                        obj.transform.tag = "SerialTuneBegin";
                        break;
                    case MusicPointType.FwdTuneBegin:
                        obj.transform.tag = "FwdTuneBegin";
                         var oobj1 = (GameObject)Instantiate(FwdTune, Vector3.zero, Quaternion.identity);
                        oobj1.transform.parent = obj.transform;
                        break;
                    case MusicPointType.FwdTuneEnd:
                        obj.transform.tag = "FwdTuneEnd";
                        break;
                    case MusicPointType.BakTuneBegin:
                        obj.transform.tag = "BakTuneBegin";
                         var oobj2 = (GameObject)Instantiate(BakTune, Vector3.zero, Quaternion.identity);
                        oobj2.transform.parent = obj.transform;
                        break;
                    case MusicPointType.BakTuneEnd:
                        obj.transform.tag = "BakTuneEnd";
                        break;
                }

                DestroyImmediate(o);
            }

            
        }


        /// <summary>
        /// 场景生成物体
        /// </summary>
        private void CreateObjectToScene(Dictionary<float, MusicPointPrefab> dictionary)
        {
            foreach (var t in dictionary)
            {
                var o = new GameObject();
                var obj = (GameObject)Instantiate(o, t.Value.MusicPointV3, t.Value.MusicPointQ4);

                obj.name = t.Key.ToString(CultureInfo.InvariantCulture);
                obj.transform.SetParent(MusicConfigObj.transform, false);

                var singleTune = Resources.Load<GameObject>("Prefabs/SingleTune");

                var fwdTune = Resources.Load<GameObject>("Prefabs/FwdTune");

                var bakTune = Resources.Load<GameObject>("Prefabs/BwdTune");

                switch (t.Value.MusicType)
                {
                    case MusicPointType.SingleTune:
                        obj.transform.tag = "SingleTune";
                        var oobj = (GameObject)Instantiate(singleTune, Vector3.zero, Quaternion.identity);
                        oobj.transform.SetParent(obj.transform, false);
                   
                        break;
                    case MusicPointType.SerialTuneEnd:
                        obj.transform.tag = "SerialTuneEnd";
                        break;
                    case MusicPointType.SerialTuneBegin:
                        obj.transform.tag = "SerialTuneBegin";
                        break;
                    case MusicPointType.FwdTuneBegin:
                        obj.transform.tag = "FwdTuneBegin";
                        var oobj1 = (GameObject)Instantiate(fwdTune, Vector3.zero, Quaternion.identity);
                        oobj1.transform.SetParent(obj.transform, false);
                        break;
                    case MusicPointType.FwdTuneEnd:
                        obj.transform.tag = "FwdTuneEnd";
                        break;
                    case MusicPointType.BakTuneBegin:
                        obj.transform.tag = "BakTuneBegin";
                        var oobj2 = (GameObject)Instantiate(bakTune, Vector3.zero, Quaternion.identity);
                        oobj2.transform.SetParent(obj.transform, false);
                        break;
                    case MusicPointType.BakTuneEnd:
                        obj.transform.tag = "BakTuneEnd";
                        break;
                }

                DestroyImmediate(o);
            }
        }
        /// <summary>
        /// 平滑音频播放是否结束
        /// </summary>
        /// <returns></returns>
        protected bool IsSmoothAudioTimeOff()
        {
            //音频播放时间延迟
            if (_smoothAudioTime < 0)
            {
                return false;
            }

            //检测平滑时间和实际播放时间是否差距在0.1之内
            return Mathf.Abs(_smoothAudioTime - MusicConfigObj.GetComponent<AudioSource>().time) > 0.1f;
        }

        /// <summary>
        /// 校正音频播放时间
        /// </summary>
        protected void CorrectSmoothAudioTime()
        {
            _smoothAudioTime = MusicConfigObj.GetComponent<AudioSource>().time;
        }
    }
}