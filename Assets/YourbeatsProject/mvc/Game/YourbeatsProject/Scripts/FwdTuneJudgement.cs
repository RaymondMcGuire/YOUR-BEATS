//-----------------------------------------------【Function Indroduction】----------------------------------------------
//	  JudgementSystem：  前仰音音符动画
//    Language：  C#
//    IDE：VS2015
//    2015.12.07  Created by 史礼华    
//---------------------------------------------------------------------------------------------------------------------

using System.Linq;
using UnityEngine;
using thelab.mvc;


public class FwdTuneJudgement : View<YourBeatsApplication>
{
    //秒表
    System.Diagnostics.Stopwatch _timer;

    //秒表时间
    double _timertime;

    //确保只判定一次
    public bool Judgeonce = true;

    //判定所需参数
    bool _insideNote;
    double _beatsSigm;
    readonly double _codeBias = 0.0f;
    //是否前仰
    bool _isFwd;

    //结果
//    int _result = 4;
	JudgementEnum resultJudgement;

    //显示评级
    public GameObject TuneStatusCo;


    //为了给动画系统传递参数
    Animator _animator;

    //相机controller（为保持镜头在前仰时不动）
    GameObject _fpController;

    void Start () {
        _timer = new System.Diagnostics.Stopwatch();
        _timer.Start();
        _animator = TuneStatusCo.GetComponent<Animator>();
        _fpController = GameObject.Find("FPController");
    }
	

	void Update () {

        //计时
        _timertime = _timer.Elapsed.TotalMilliseconds;

        //判断是否前仰，，，用鼠标左键代替
        _isFwd = Input.GetMouseButton(0);
        //暂时去掉鼠标左键
	    _isFwd = true;
        //判断是否在音符内
        var ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        _insideNote = Physics.Raycast(ray);

        if (Input.GetKey(KeyCode.Space) && _insideNote  && _isFwd)
        {
            if (_fpController != null && _fpController.activeSelf) _fpController.SetActive(false);
        }
        else
        {
            if (_fpController != null && !_fpController.activeSelf)
                _fpController.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && Judgeonce && _isFwd)
        {
            //产生单次音符敲击动画
            var center = app.model.FpModel.DynamicPos.GetComponent<Transform>().position;
            app.model.FpModel.DynamicEffect.SetActive(true);
            Instantiate(app.model.FpModel.DynamicEffect, center,GetComponent<Transform>().rotation);
            //按键时间
            _beatsSigm = _timertime / 1000;
            _timer.Stop();
            var a = new JudgementSystem();
			resultJudgement = a.Judgement(_insideNote, _beatsSigm, _codeBias);

            if (resultJudgement == JudgementEnum.NotClick) return;
           // TuneStatusCO.SetActive(true);
            //animator.SetInteger("TuneStatues", result);

#region ADDSCORE

			Debug.Log("前仰音点击时间 " + _beatsSigm+"评级 "+ resultJudgement.ToString());

			Debug.Log("此次得分："+TuneScoreSheet.GetInstance.ReadScore(TunesTypeEnum.FwdTune,resultJudgement,3.0f,0.5f));

			ScoreSystem.GetInstance.CalculateCurrentScore(float.Parse(TuneScoreSheet.GetInstance.ReadScore(TunesTypeEnum.FwdTune,resultJudgement,3.0f,0.5f)));

			Debug.Log("当前分数 ：" + ScoreSystem.GetInstance.CurrentScore);

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
            if (SongModel.HandlerDictionary.Count != 0)
                SongModel.Curtunetime = SongModel.HandlerDictionary.Keys.Min();
            SongModel.Curover = true;
           // judgeonce = false;
            _timer.Start();
           
        }

        if (_timertime > 1000 && Judgeonce == true)
        {

            //TuneStatusCO.SetActive(true);
            //animator.SetInteger("TuneStatues", 4);
        }

        if (Input.GetKeyUp(KeyCode.Space) && Judgeonce == true && _isFwd == true&& _insideNote==true)
        {
            //产生单次音符敲击动画
            var center = app.model.FpModel.DynamicPos.GetComponent<Transform>().position;
            app.model.FpModel.DynamicEffect.SetActive(true);
            Instantiate(app.model.FpModel.DynamicEffect, center, this.GetComponent<Transform>().rotation);
            //按键时间
            _beatsSigm = (_timertime-1000) / 1000;
            _timer.Stop();
            var a = new JudgementSystem();
			resultJudgement = a.Judgement(_insideNote, _beatsSigm, _codeBias);
            TuneStatusCo.SetActive(true);
			_animator.SetInteger("TuneStatues", (int)resultJudgement);
			print("点击时间" + _beatsSigm + "评级" + resultJudgement);
            Judgeonce = false;
            if (SongModel.HandlerDictionary.Count != 0)
                SongModel.Curtunetime = SongModel.HandlerDictionary.Keys.Min();
            SongModel.Curover = true;
            _timer.Start();
        }

        if (_timertime > 3000 && Judgeonce == true)
        {
            if (SongModel.HandlerDictionary.Count != 0)
                SongModel.Curtunetime = SongModel.HandlerDictionary.Keys.Min();
            SongModel.Curover = true;
            TuneStatusCo.SetActive(true);
			_animator.SetInteger("TuneStatues", (int)JudgementEnum.Miss);

			resultJudgement=JudgementEnum.Miss;

			if(resultJudgement==JudgementEnum.Miss)
			{
				ComboSystem.GetInstance.ResetCurrentCombo();
			}
        }
        if (_timertime >= 4200)
        {
            if (SongModel.HandlerDictionary.Count != 0)
                SongModel.Curtunetime = SongModel.HandlerDictionary.Keys.Min();
            SongModel.Curover = true;
            Destroy(gameObject);
            //print("DestroygameObject");
        }
    }

}
