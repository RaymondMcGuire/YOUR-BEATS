//-----------------------------------------------【Function Indroduction】----------------------------------------------
//	  UIController：  
//    Language：  C#
//    IDE：VS2015
//    2015.12.16  Created by RaymondMG  
//---------------------------------------------------------------------------------------------------------------------
using UnityEngine;
using thelab.mvc;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using JetBrains.Annotations;
using System;
using DG.Tweening;
public class UIController : Controller<YourBeatsApplication>
{
    /// <summary>
    /// 处理应用程序发出的通知
    /// </summary>
    /// <param name="pEvent"></param>
    /// <param name="pTarget"></param>
    /// <param name="pData"></param>
    public override void OnNotification(string pEvent, UnityEngine.Object pTarget, params object[] pData)
    {
        switch (pEvent)
        {
            case "ui.awake":
                InitAwakeUiData();
                break;
            case "ui.start":
                InitStartUiData();
                break;
            case "ui.pressbutton":

                if (!SongModel.IsSongPlaying && !app.model.ui.IsAnimator)
                CheckButton();

                break;
        }
    }


    private void CheckButton()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (app.model.ui.UiPanel[app.model.ui._currentUiName].ChildDictionary.ContainsKey(UIDirecrion.Right))
            SwitchUiLayer(app.model.ui.UiPanel[app.model.ui._currentUiName].ChildDictionary[UIDirecrion.Right]);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (app.model.ui.UiPanel[app.model.ui._currentUiName].ChildDictionary.ContainsKey(UIDirecrion.Up))
            SwitchUiLayer(app.model.ui.UiPanel[app.model.ui._currentUiName].ChildDictionary[UIDirecrion.Up]);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (app.model.ui.UiPanel[app.model.ui._currentUiName].ChildDictionary.ContainsKey(UIDirecrion.Left))
            SwitchUiLayer(app.model.ui.UiPanel[app.model.ui._currentUiName].ChildDictionary[UIDirecrion.Left]);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (app.model.ui.UiPanel[app.model.ui._currentUiName].ChildDictionary.ContainsKey(UIDirecrion.Down))
            SwitchUiLayer(app.model.ui.UiPanel[app.model.ui._currentUiName].ChildDictionary[UIDirecrion.Down]);
        }
    }


    private void InitAwakeUiData()
    {
        if (app.model.ui.UiCanvas == null) return;


        //初始化UI数据信息
        foreach (var ulayer in app.model.ui.UiCanvas)
        {
            var u = new UILayer
            {
                UiName = ulayer.transform.name,
                UiCanvas = ulayer,
                UiImageList = ulayer.transform.GetComponentsInChildren<Image>(),
                UiState = false,
                ParentList = new List<string>(),
                ChildDictionary=new Dictionary<UIDirecrion,string>()
            };

            app.model.ui.UiPanel.Add(u.UiName, u);
        }
    }


    /// <summary>
    /// 初始化UI数据、、、、扩展为JSON
    /// </summary>
    private void InitStartUiData()
    {
        app.model.ui.UiPanel["CanvasQuitGame"].ChildDictionary.Add(UIDirecrion.Right, "CanvasMain");
        app.model.ui.UiPanel["CanvasQuitGame"].ChildDictionary.Add(UIDirecrion.Left, "Quit");
        app.model.ui.UiPanel["CanvasQuitGame"].ParentList.Add("CanvasMain");
        app.model.ui.UiPanel["CanvasQuitGame"].ParentList.Add("Quit");
        app.model.ui.UiPanel["CanvasQuitGame"].ParentList.Add("CanvasEndSong");


        app.model.ui.UiPanel["CanvasMain"].ChildDictionary.Add(UIDirecrion.Right, "CanvasIntroduction");
        app.model.ui.UiPanel["CanvasMain"].ChildDictionary.Add(UIDirecrion.Left, "CanvasQuitGame");
        app.model.ui.UiPanel["CanvasMain"].ParentList.Add("CanvasEndSong");
        app.model.ui.UiPanel["CanvasMain"].ParentList.Add("CanvasQuitGame");


        app.model.ui.UiPanel["CanvasIntroduction"].ChildDictionary.Add(UIDirecrion.Right, "CanvasPlayMode");
        app.model.ui.UiPanel["CanvasIntroduction"].ChildDictionary.Add(UIDirecrion.Left, "CanvasMain");
        app.model.ui.UiPanel["CanvasIntroduction"].ParentList.Add("CanvasMain");



        app.model.ui.UiPanel["CanvasPlayMode"].ParentList.Add("CanvasIntroduction");
        app.model.ui.UiPanel["CanvasPlayMode"].ChildDictionary.Add(UIDirecrion.Up,"CanvasUpload");
        app.model.ui.UiPanel["CanvasPlayMode"].ChildDictionary.Add(UIDirecrion.Right,"CanvasPlayExistSong");
        app.model.ui.UiPanel["CanvasPlayMode"].ChildDictionary.Add(UIDirecrion.Left, "CanvasIntroduction");

        app.model.ui.UiPanel["CanvasUpload"].ParentList.Add("CanvasPlayMode");
        app.model.ui.UiPanel["CanvasUpload"].ChildDictionary.Add(UIDirecrion.Left, "CanvasPlayMode");

        app.model.ui.UiPanel["CanvasPlayExistSong"].ParentList.Add("CanvasPlayMode");
        app.model.ui.UiPanel["CanvasPlayExistSong"].ChildDictionary.Add(UIDirecrion.Left, "CanvasPlayMode");
        app.model.ui.UiPanel["CanvasPlayExistSong"].ChildDictionary.Add(UIDirecrion.Right, "CanvasInGame");

        app.model.ui.UiPanel["CanvasInGame"].ParentList.Add("CanvasPlayExistSong");
        app.model.ui.UiPanel["CanvasInGame"].ChildDictionary.Add(UIDirecrion.Right,"CanvasEndSong");

        app.model.ui.UiPanel["CanvasEndSong"].ParentList.Add("CanvasInGame");
        app.model.ui.UiPanel["CanvasEndSong"].ParentList.Add("CanvasMain");
        app.model.ui.UiPanel["CanvasEndSong"].ChildDictionary.Add(UIDirecrion.Left, "CanvasMain");
        app.model.ui.UiPanel["CanvasEndSong"].ChildDictionary.Add(UIDirecrion.Right, "CanvasPlayMode");

        app.model.ui.UiPanel["CanvasMain"].UiState = true;
    }


    /// <summary>
    /// 切换UI
    /// </summary>
    /// <param name="canvasName"></param>
    public void SwitchUiLayer(string canvasName)
    {
        app.model.ui.IsAnimator = true;

        //识别需要切换的UI层级与当前UI层级关系
        if (app.model.ui.UiPanel[app.model.ui._currentUiName].ParentList.Contains(canvasName) && app.model.ui.UiPanel[app.model.ui._currentUiName].ChildDictionary[UIDirecrion.Left].Contains(canvasName))
        {
            //切换为父级
            app.model.ui._currentLayerState = UILayerState.ParentLayer;
        }
        else if (app.model.ui.UiPanel[app.model.ui._currentUiName].ChildDictionary[UIDirecrion.Right].Contains(canvasName) || app.model.ui.UiPanel[app.model.ui._currentUiName].ChildDictionary[UIDirecrion.Up].Contains(canvasName) || app.model.ui.UiPanel[app.model.ui._currentUiName].ChildDictionary[UIDirecrion.Down].Contains(canvasName))
        {
            //切换为子级
            app.model.ui._currentLayerState = UILayerState.ChildLayer;
        }
        else
        {
            Debug.Log(canvasName +"quit");
            //退出
            return;
        }

        app.model.ui._lastUiName = app.model.ui._currentUiName;
        app.model.ui._currentUiName = canvasName;

        UIEventHandler();


        switch (app.model.ui._currentLayerState)
        {
            case UILayerState.ChildLayer:
                {

                    if (app.model.ui._lastUiName != "")
                    {
                        var cui = app.model.ui.UiPanel[app.model.ui._lastUiName];
                        //退出当前UI
                        StartCoroutine("DoExitAnimator", cui);
                    }

                    var ui = app.model.ui.UiPanel[app.model.ui._currentUiName];
                    ui.UiState = true;

                    //显示下一个UI
                    StartCoroutine("DoAnimator", ui);
                }
                break;
            case UILayerState.ParentLayer:
                {
                    if (app.model.ui._currentUiName != "")
                    {
                        var cui = app.model.ui.UiPanel[app.model.ui._currentUiName];
                        cui.UiState = true;
                        //退出当前UI
                        StartCoroutine("P_DoExitAnimator", cui);
                    }

                    var ui = app.model.ui.UiPanel[app.model.ui._lastUiName];
                    //显示下一个UI
                    StartCoroutine("P_DoAnimator", ui);
                }
                break;
        }


    }

    /// <summary>
    /// UI事件绑定与触发
    /// </summary>
    private void UIEventHandler()
    {

        switch (app.model.ui._currentUiName)
        {
            case "CanvasUpload":
                app.view.Assert<UIView>("UI").UpLoadMusicFile();
                break;
            case "CanvasInGame":
                app.view.Assert<UIView>("UI").SelectMusic();
                app.view.Assert<SongView>("SongView").PlaySong();
                break;
            case "CanvasPlayMode":
                app.view.Assert<UIView>("UI").BackSelectMusic();
                break;
            case "Quit":
                Application.Quit();
                break;
        }
    }

    /// <summary>
    /// Child-UI显示
    /// </summary>
    /// <returns></returns>
    IEnumerator DoAnimator([NotNull] UILayer u)
    {
        if (u == null) throw new ArgumentNullException("u");
        u.Refresh();
        var images = u.UiImageList;
        var words = u.UiWordList;

        foreach (var i in words)
        {
            i.DOFade(1, app.model.ui.EnterTime).SetEase(Ease.OutExpo);
        }

        u.UiCanvas.transform.localScale = new Vector3(app.model.ui.InitScale, app.model.ui.InitScale, app.model.ui.InitScale);
        u.UiCanvas.transform.DOScale(app.model.ui.EnterScale, app.model.ui.EnterTime).SetEase(Ease.OutExpo);


        foreach (var i in images)
        {
            i.DOFade(1, app.model.ui.EnterTime).SetEase(Ease.OutExpo);
        }

       

        yield return null;
    }

    /// <summary>
    /// Child-UI退出
    /// </summary>
    /// <param name="u"></param>
    /// <returns></returns>
    IEnumerator DoExitAnimator([NotNull] UILayer u)
    {
        if (u == null) throw new ArgumentNullException("u");
        u.Refresh();
        var images = u.UiImageList;
        var words = u.UiWordList;

        foreach (var i in words)
        {
            i.DOFade(0, app.model.ui.ExitTime).SetEase(Ease.OutExpo);
        }

        u.UiCanvas.transform.DOScale(app.model.ui.ExitScale, app.model.ui.ExitTime).SetEase(Ease.OutExpo);

        foreach (var i in images)
        {
            i.DOFade(0, app.model.ui.ExitTime).SetEase(Ease.OutExpo);
        }

      

        yield return new WaitForSeconds(app.model.ui.EnterTime);
        u.UiState = false;
        app.model.ui.IsAnimator = false;
    }


    /// <summary>
    /// Parent-UI显示
    /// </summary>
    /// <returns></returns>
    IEnumerator P_DoAnimator([NotNull] UILayer u)
    {
        if (u == null) throw new ArgumentNullException("u");


        u.Refresh();
        var images = u.UiImageList;
        var words = u.UiWordList;



        foreach (var i in words)
        {
            i.DOFade(0, app.model.ui.EnterTime).SetEase(Ease.OutExpo);
        }

        u.UiCanvas.transform.localScale = new Vector3(app.model.ui.EnterTime, app.model.ui.EnterTime, app.model.ui.EnterTime);
        u.UiCanvas.transform.DOScale(app.model.ui.InitScale, app.model.ui.EnterTime).SetEase(Ease.OutExpo);


        foreach (var i in images)
        {
            i.DOFade(0, app.model.ui.EnterTime).SetEase(Ease.OutExpo);
        }
     

        yield return new WaitForSeconds(app.model.ui.EnterTime);
        u.UiState = false;
        app.model.ui.IsAnimator = false;
    }

    /// <summary>
    /// Parent-UI退出
    /// </summary>
    /// <param name="u"></param>
    /// <returns></returns>
    IEnumerator P_DoExitAnimator([NotNull] UILayer u)
    {
        if (u == null) throw new ArgumentNullException("u");
        u.Refresh();
        var images = u.UiImageList;
        var words = u.UiWordList;

        foreach (var i in words)
        {
            i.DOFade(1, app.model.ui.ExitTime).SetEase(Ease.OutExpo);
        }

        u.UiCanvas.transform.localScale = new Vector3(app.model.ui.ExitScale, app.model.ui.ExitScale, app.model.ui.ExitScale);

        u.UiCanvas.transform.DOScale(app.model.ui.EnterScale, app.model.ui.ExitTime).SetEase(Ease.OutExpo);

        foreach (var i in images)
        {
            i.DOFade(1, app.model.ui.ExitTime).SetEase(Ease.OutExpo);
        }
       
        yield return null;
    }
}
