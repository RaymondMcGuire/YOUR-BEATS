using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine.UI;

public class TestDoTween : MonoBehaviour
{


    //void Awake()
    //{
    //    if (UiCanvas == null) return;


    //    //初始化UI数据信息
    //    foreach (var ulayer in UiCanvas.Select(u => new UILayer
    //    {
    //        UiName = u.transform.name,
    //        UiCanvas = u,
    //        UiImageList = u.transform.GetComponentsInChildren<Image>(),
    //        UiState = false,
    //        ParentList=new List<string>(),
    //        ChildList=new List<string>()
            
    //    }))
    //    {
    //        UiPanel.Add(ulayer.UiName, ulayer);
    //    }

      
    //}


    //void Start()
    //{
    //    UiPanel["CanvasMain"].ChildList.Add("CanvasPlayMode");
    //    UiPanel["CanvasPlayMode"].ParentList.Add("CanvasMain");
    //    UiPanel["CanvasMain"].UiState = true;
    //}


    ///// <summary>
    ///// 切换UI
    ///// </summary>
    ///// <param name="canvasName"></param>
    //public void SwitchUiLayer(string canvasName)
    //{
    //    //识别需要切换的UI层级与当前UI层级关系
    //    if (UiPanel[_currentUiName].ParentList.Contains(canvasName))
    //    {
    //        //切换为父级
    //        _currentLayerState = UILayerState.ParentLayer;
    //    }
    //    else if (UiPanel[_currentUiName].ChildList.Contains(canvasName))
    //    {
    //        //切换为子级
    //        _currentLayerState = UILayerState.ChildLayer;
    //    }
    //    else
    //    {
    //        //退出
    //        return;
    //    }

    //    _lastUiName = _currentUiName;
    //    _currentUiName = canvasName;

    //    switch (_currentLayerState)
    //    {
    //        case UILayerState.ChildLayer:
    //        {

    //            if (_lastUiName != "")
    //            {
    //                var cui = UiPanel[_lastUiName];
    //                //退出当前UI
    //                StartCoroutine("DoExitAnimator", cui);
    //            }

    //            var ui = UiPanel[_currentUiName];
    //            ui.UiState = true;

    //            //显示下一个UI
    //            StartCoroutine("DoAnimator", ui);
    //        }
    //            break;
    //        case UILayerState.ParentLayer:
    //        {
    //            if (_currentUiName != "")
    //            {
    //                var cui = UiPanel[_currentUiName];
    //                //退出当前UI
    //                StartCoroutine("P_DoExitAnimator", cui);
    //            }

    //            var ui = UiPanel[_lastUiName];
    //            ui.UiState = true;

    //            //显示下一个UI
    //            StartCoroutine("P_DoAnimator", ui);
    //        }
    //            break;
    //    }

      
    //}

    ///// <summary>
    ///// Child-UI显示
    ///// </summary>
    ///// <returns></returns>
    //IEnumerator DoAnimator([NotNull] UILayer u)
    //{
    //    if (u == null) throw new ArgumentNullException("u");

    //    var images = u.UiImageList;
    //    u.UiCanvas.transform.localScale = new Vector3(InitScale, InitScale, InitScale);
    //    u.UiCanvas.transform.DOScale(EnterScale, EnterTime).SetEase(Ease.OutExpo);
   

    //    foreach (var i in images)
    //    {
    //        i.DOFade(1, EnterTime).SetEase(Ease.OutExpo);
    //    }

    //    yield return null;
    //}

    ///// <summary>
    ///// Child-UI退出
    ///// </summary>
    ///// <param name="u"></param>
    ///// <returns></returns>
    //IEnumerator DoExitAnimator([NotNull] UILayer u)
    //{
    //    if (u == null) throw new ArgumentNullException("u");

    //    var images = u.UiImageList;

    //    u.UiCanvas.transform.DOScale(ExitScale, ExitTime).SetEase(Ease.OutExpo);

    //    foreach (var i in images)
    //    {
    //        i.DOFade(0, ExitTime).SetEase(Ease.OutExpo);
    //    }

    //    yield return ExitTime;

    //    //u.UiState = false;

    //}


    ///// <summary>
    ///// Parent-UI显示
    ///// </summary>
    ///// <returns></returns>
    //IEnumerator P_DoAnimator([NotNull] UILayer u)
    //{
    //    if (u == null) throw new ArgumentNullException("u");

    //    var images = u.UiImageList;
    //    u.UiCanvas.transform.localScale = new Vector3(EnterTime, EnterTime, EnterTime);
    //    u.UiCanvas.transform.DOScale(InitScale, EnterTime).SetEase(Ease.OutExpo);


    //    foreach (var i in images)
    //    {
    //        i.DOFade(0, EnterTime).SetEase(Ease.OutExpo);
    //    }

    //    yield return null;
    //}

    ///// <summary>
    ///// Parent-UI退出
    ///// </summary>
    ///// <param name="u"></param>
    ///// <returns></returns>
    //IEnumerator P_DoExitAnimator([NotNull] UILayer u)
    //{
    //    if (u == null) throw new ArgumentNullException("u");

    //    var images = u.UiImageList;
    //    u.UiCanvas.transform.localScale = new Vector3(ExitScale, ExitScale, ExitScale);
        
    //    u.UiCanvas.transform.DOScale(EnterScale, ExitTime).SetEase(Ease.OutExpo);

    //    foreach (var i in images)
    //    {
    //        i.DOFade(1, ExitTime).SetEase(Ease.OutExpo);
    //    }

    //    yield return null;
    //}
	
}
