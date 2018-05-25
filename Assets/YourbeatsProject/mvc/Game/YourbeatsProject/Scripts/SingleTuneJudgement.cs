//-----------------------------------------------【Function Indroduction】----------------------------------------------
//	  JudgementSystem：  单音音符动画
//    Language：  C#
//    IDE：VS2015
//    2015.11.06  Created by 史礼华  
//    2015.12.24  Edited by 茅伟  
//---------------------------------------------------------------------------------------------------------------------


using System.Linq;
using UnityEngine;
using thelab.mvc;

public class SingleTuneJudgement : View<YourBeatsApplication>
{
    //秒表
     System.Diagnostics.Stopwatch _timer;


    private static SingleTuneJudgement _instance;

    //秒表时间
     double _timertime;

    //确保只判定一次
    public bool Judgeonce = true;

    //判定所需参数
    bool _insideNote;
    double _beatsSigm;

    readonly double _codeBias=0.0f;

    //结果
//    int _result=4;
	JudgementEnum resultJudgement;

    //显示评级
    public GameObject  TuneStatusCo;

	public float time;

	public MusicPointPrefab musicPointPrefab;

    //为了给动画系统传递参数
    Animator _animator;

    void Awake () {
        _timer = new System.Diagnostics.Stopwatch();
        _timer.Start();
        _animator = TuneStatusCo.GetComponent<Animator>();
        
    }



    private void Update()
    {

        //计时
        _timertime = _timer.Elapsed.TotalMilliseconds;

        //判断是否在音符内
        var ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2, Screen.height/2));
        RaycastHit hitInfo;
        string myname = transform.name;

        if (Physics.Raycast(ray, out hitInfo) && hitInfo.collider.name == myname)
            _insideNote = true;
        else
            _insideNote = false;

        if (Input.GetKeyDown(KeyCode.Space) && Judgeonce)
        {
            //产生单次音符敲击动画
            var center = app.model.FpModel.DynamicPos.GetComponent<Transform>().position;
            app.model.FpModel.DynamicEffect.SetActive(true);
            Instantiate(app.model.FpModel.DynamicEffect, center, this.GetComponent<Transform>().rotation);
            //按键时间
            _beatsSigm = _timertime/1000;
            _timer.Stop();
            var a = new JudgementSystem();
            resultJudgement = a.Judgement(_insideNote, _beatsSigm, _codeBias);
            TuneStatusCo.SetActive(true);
            _animator.SetInteger("TuneStatues", (int) resultJudgement);
            #region ADDSCORE

            Debug.Log("单音点击时间 " + _beatsSigm + "评级 " + resultJudgement.ToString());

            Debug.Log("此次得分：" + TuneScoreSheet.GetInstance.ReadScore(TunesTypeEnum.SingleTune, resultJudgement));

            //通知ScoreSystem增加对应得分
            ScoreSystem.GetInstance.CalculateCurrentScore(
                float.Parse(TuneScoreSheet.GetInstance.ReadScore(TunesTypeEnum.SingleTune, resultJudgement)));

            Debug.Log("当前分数 ：" + ScoreSystem.GetInstance.CurrentScore);

            //找到分数显示区域，通知它改变总分
            FindObjectOfType<ScoreField>()
                .GetComponent<ScoreField>()
                .UpdateScore(ScoreSystem.GetInstance.CurrentScore);


            #endregion

            #region ADDCOMBO

//			if(resultJudgement!=JudgementEnum.Miss)
//			{				
//				ComboSystem.GetInstance.AddCurrentCombo(time,musicPointPrefab);
//			}

            if (resultJudgement != JudgementEnum.Miss)
            {
                ComboSystem.GetInstance.AddCurrentCombo();
            }
			else if(resultJudgement==JudgementEnum.Miss)
			{
				ComboSystem.GetInstance.ResetCurrentCombo();
			}


            #endregion

            Judgeonce = false;

            if(SongModel.HandlerDictionary.Count!=0)
            SongModel.Curtunetime = SongModel.HandlerDictionary.Keys.Min();
            SongModel.Curover = true;
            _timer.Start();
        }

        if (_timertime > 2000 && Judgeonce)
        {
            TuneStatusCo.SetActive(true);
            _animator.SetInteger("TuneStatues", (int) JudgementEnum.Miss);

            Judgeonce = false;

            if (SongModel.HandlerDictionary.Count != 0)
            SongModel.Curtunetime = SongModel.HandlerDictionary.Keys.Min();
            SongModel.Curover = true;
            //ComboSystem.GetInstance.ResetCurrentCombo();

			resultJudgement=JudgementEnum.Miss;

			if(resultJudgement==JudgementEnum.Miss)
			{
				ComboSystem.GetInstance.ResetCurrentCombo();
			}

        }
        if (_timertime >= 3300)
        {
            if (SongModel.HandlerDictionary.Count != 0)
            SongModel.Curtunetime = SongModel.HandlerDictionary.Keys.Min();
            SongModel.Curover = true;
            Destroy(gameObject);
        }
    }
}



