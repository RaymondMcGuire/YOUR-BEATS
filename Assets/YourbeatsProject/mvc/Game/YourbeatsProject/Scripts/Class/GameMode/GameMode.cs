//-----------------------------------------------【Function Indroduction】----------------------------------------------
//	  GameMode：  
//    Language：  C#
//    IDE：VS2013
//    2015.11.14  Created by RaymondMG  
//    2015.11.27  LastEdited by RaymondMG  
//---------------------------------------------------------------------------------------------------------------------
using System;
using DG.Tweening;
using UnityEngine;

namespace YourBeats.core.gamemode
{
    /// <summary>
    /// 游戏模式函数库
    /// </summary>
    public class GameMode : SingletonBase<GameMode> 
    {

        public SongModel Song;
        public UIController UICon;

        //当前用户所处的游戏模式
        private GameModeType _currentGameMode;

        /// <summary>
        /// 返回当前游戏模式
        /// </summary>
        /// <returns></returns>
        public GameModeType GetCurrentGameMode()
        {
            return _currentGameMode;
        }

        /// <summary>
        /// 切换游戏模式Api
        /// </summary>
        /// <param name="state"></param>
        public void GameModeStateSwitch(GameModeType state)
        {
            _currentGameMode = state;
            GameModeStateMachine(state);
        }

        /// <summary>
        /// 游戏模式状态机
        /// </summary>
        /// <param name="state"></param>
        public void GameModeStateMachine(GameModeType state)
        {

            switch (state)
            {
                case GameModeType.Awake:
                    AwakeDataInfoHandler();
                    break;
                case GameModeType.Start:
                    StartDataInfoHandler();
                    break;
                case GameModeType.Calibration:
                    CalibrationHandler();
                    break;
                case GameModeType.UpLoadLocalMusicFile:
                    UpLoadLocalMusicFileHandler();
                    break;
                case GameModeType.SelectMusic:
                    SelectMusicHandler();
                    break;
                case GameModeType.StartPlay:
                    StartPlayHandler();
                    break;
                case GameModeType.Playing:
                    PlayingHandler();
                    break;
                case GameModeType.Pause:
                    PauseHandler();
                    break;
                case GameModeType.Stop:
                    StopHandler();
                    break;
                case GameModeType.End:
                    EndHandler();
                    break;
                case GameModeType.Evalute:
                    EvaluteHandler();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("state", state, null);
            }
        }

        /// <summary>
        /// 评分状态
        /// </summary>
        private void EvaluteHandler()
        {

            UICon.SwitchUiLayer("CanvasEndSong");

            GameObject.FindObjectOfType<EndMaxComboScoreField>().UpdateScore(ComboSystem.GetInstance.maxCombo);

            GameObject.FindObjectOfType<EndScoreField>().UpdateScore(ScoreSystem.GetInstance.CalculateTotalScore());

            GameObject.FindObjectOfType<RankField>().SetRankImage(RankSystem.GetInstance.CalculateRank(ScoreSystem.GetInstance.CalculateTotalScore()));



            SongModel.SongAudioSource.clip = (AudioClip)Resources.Load("Music/BGM/Halo");
            SongModel.SongAudioSource.loop = true;
            SongModel.SongAudioSource.Play();

            Debug.Log("进入评分系统，给出评分结果,待用户给出操作后返回选择歌曲界面");
        }

        /// <summary>
        /// 游戏结束
        /// </summary>
        private void EndHandler()
        {
            //飞船离开
            Song.flydisk.transform.DOMove(new Vector3(794f, 108, 413f), 2).SetEase(Ease.InBack).OnComplete(FlyDiskExitAnimator);
            Song.flydisk.transform.DORotate(new Vector3(4, 4, 4), 1).SetEase(Ease.InBack);


            Debug.Log("结束音乐游戏，统计游戏数据，进入评估状态，返回评分界面");
            GameModeStateSwitch(GameModeType.Evalute);
        }

        /// <summary>
        /// 停止游戏
        /// </summary>
        private void StopHandler()
        {
            Debug.Log("强行终止音乐游戏，清空游戏中数据并，返回音乐选择界面");
            GameModeStateSwitch(GameModeType.SelectMusic);
        }

        /// <summary>
        /// 暂停游戏
        /// </summary>
        private void PauseHandler()
        {
            Debug.Log("弹出游戏暂停窗口，将正在进行游戏冻结");
        }

        /// <summary>
        /// 音乐游戏中
        /// </summary>
        private void PlayingHandler()
        {
            //确保音乐未播放完

            //读取json文件中时间点对应的音乐符号类型
           
            //根据smoothaudiotime（当前音乐时间点）获取是否有对应要出现的音乐符号并显示

            //判定系统并确保两秒后删除该物体

			if(SongModel.IsSongPlaying)
			{
				Song.CurrentSongTime=SongModel.SongAudioSource.time;

//				Debug.Log(Song.CurrentSongTime);

				if (SongModel.SongAudioSource.isPlaying)
				{
					if (SongModel.IsSmoothAudioTimeOff(Song.CurrentSongTime))
					{
						SongModel.CorrectSmoothAudioTime(Song.CurrentSongTime);
					}
					Song.DisplayPoint(Song.CurrentSongTime);
				}
				else
				{
					SongModel.IsSongPlaying=false;
				    Song.ClearInitedPoint();
					GameModeStateSwitch(GameModeType.End);
				}
			}
//           Debug.Log(_gametime);
//            _gametime += Time.deltaTime;

             //有时音频会发生跳跃或滞后情况，保证在发生这些情况时进行校正

        }

        /// <summary>
        /// 开始音乐游戏
        /// </summary>
        private void StartPlayHandler()
        {
            Song.flydisk.SetActive(true);

            //飞碟进入
            Song.flydisk.transform.DOMove(new Vector3(-51.5f, 108, 293.1f), 2).SetEase(Ease.OutBack);
            Song.flydisk.transform.DORotate(new Vector3(4, 4, 4), 1).SetEase(Ease.OutBack).OnComplete(FlyDiskEnterAnimator);

            Debug.Log("初始化用户选择音乐的游戏数据并进入游戏界面,开始游戏");
            GameModeStateSwitch(GameModeType.Playing);
        }

        /// <summary>
        /// disk进入
        /// </summary>
        private void FlyDiskEnterAnimator()
        {
             Song.flydisk.transform.DORotate(new Vector3(0, 0, 0), 1).SetEase(Ease.OutBack);
        }


        /// <summary>
        /// disk退出
        /// </summary>
        private void FlyDiskExitAnimator()
        {
            Song.flydisk.SetActive(false);
        }

        /// <summary>
        /// 选择音乐
        /// </summary>
        private void SelectMusicHandler()
        {
            Debug.Log("初始化并进入选择音乐游戏界面");
        }

        /// <summary>
        /// 上传本地音乐文件并声称自动鼓点音频文件
        /// </summary>
        private void UpLoadLocalMusicFileHandler()
        {
            Debug.Log("初始化并进入上传文件界面");
        }

        /// <summary>
        /// Oculus进行头盔准星校准
        /// </summary>
        private void CalibrationHandler()
        {
            Debug.Log("头盔准星校准完成，进入上传音乐界面");
            GameModeStateSwitch(GameModeType.UpLoadLocalMusicFile);
        }

        /// <summary>
        /// Start初始化整个项目数据
        /// </summary>
        private void StartDataInfoHandler()
        {
            Song.flydisk.SetActive(false);
            Debug.Log("Start初始化整个项目数据完成");
            GameModeStateSwitch(GameModeType.Calibration);
        }

        /// <summary>
        /// Awake初始化整个项目数据
        /// </summary>
        private void AwakeDataInfoHandler()
        {
            //初始化判定系统
            JudgementSystem.GetInstance.InitJudgementSystem();

            //初始化计分系统
            ScoreSystem.GetInstance.InitScoreSystem();

			//初始化分数表
			TuneScoreSheet.GetInstance.InitDataSheet();

            Debug.Log("Awake初始化整个项目数据完成");
        }
    }

}