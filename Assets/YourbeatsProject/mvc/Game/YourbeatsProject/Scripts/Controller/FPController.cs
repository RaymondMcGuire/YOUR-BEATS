//-----------------------------------------------【Function Indroduction】----------------------------------------------
//	  JudgementSystem：  第一人称相机 CONTROLLER 部分
//    Language：  C#
//    IDE：VS2015
//    2015.12.03  Created by 史礼华  
//---------------------------------------------------------------------------------------------------------------------
using UnityEngine;
using thelab.mvc;

public class FPController : Controller<YourBeatsApplication> {

    /// <summary>
    /// 处理应用程序发送来的通知
    /// </summary>
    /// <param name="p_event"></param>
    /// <param name="p_target"></param>
    /// <param name="p_data"></param>
    public override void OnNotification(string p_event, Object p_target, params object[] p_data)
    {
        switch (p_event)
        {

            
            case "fpcamera.update":


                var center = app.model.FpModel.DynamicPos.GetComponent<Transform>().position;
                var ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
                if (Input.GetKey(KeyCode.Space)&& !Physics.Raycast(ray))
                {
                    app.model.FpModel.DynamicEffect.SetActive(true);
                    Instantiate(app.model.FpModel.DynamicEffect, center, GetComponent<Transform>().rotation);
                }

                //var rayUI= Camera.main.ScreenPointToRay(Input.mousePosition);
                //if (Physics.Raycast(rayUI, out hitInfo))
                //{
                //    Debug.Log(hitInfo.transform.name);
                //}

                ////计数变量
                //int i=0;
                ////粒子数组声明
                //GameObject[] DynamicEffectArry;
                //DynamicEffectArry =new GameObject[20] ;
                //if (Input.GetKey(KeyCode.Space) && i < 20)
                //{
                //    app.model.fpModel.DynamicEffect.SetActive(true);
                //    DynamicEffectArry[i] =(GameObject) Instantiate(app.model.fpModel.DynamicEffect, center, this.GetComponent<Transform>().rotation);
                //    DynamicEffectArry[System.Math.Abs( i -5)]=null;
                //    i++;
                //}
                //else if(Input.GetKey(KeyCode.Space) && i==20)
                //{ i = 0;
                //    app.model.fpModel.DynamicEffect.SetActive(true);
                //    DynamicEffectArry[i] = (GameObject)Instantiate(app.model.fpModel.DynamicEffect, center, this.GetComponent<Transform>().rotation);
                //    Destroy (DynamicEffectArry[System.Math.Abs(i - 15)]);
                //    DynamicEffectArry[System.Math.Abs(i - 5)] = null;
                //    i++;
                //}
                break;

        }
    }

}
