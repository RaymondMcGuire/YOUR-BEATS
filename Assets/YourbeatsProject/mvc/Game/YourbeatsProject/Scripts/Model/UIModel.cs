//-----------------------------------------------【Function Indroduction】----------------------------------------------
//	  UIController：  
//    Language：  C#
//    IDE：VS2015
//    2015.12.16  Created by RaymondMG  
//---------------------------------------------------------------------------------------------------------------------
using UnityEngine;
using thelab.mvc;
using System.Collections.Generic;


public class UIModel : Model<YourBeatsApplication>
{
    public readonly Dictionary<string, UILayer> UiPanel = new Dictionary<string, UILayer>();

    public string _currentUiName = "CanvasMain";
    public string _lastUiName = "CanvasMain";

    public UILayerState _currentLayerState = UILayerState.ChildLayer;

    //初始化大小
    public  float InitScale = 0.75f;

    //进入时间
    public  float EnterTime = 1;

    //退出大小
    public  float ExitScale = 3.5f;
    //进入大小
    public  float EnterScale = 1f;

    //退出时间
    public  float ExitTime = 1;

    public bool IsAnimator = false;

    public Canvas[] UiCanvas;

    public UIView UiView { get { return _uiView = Assert<UIView>(_uiView); } }
    private UIView _uiView;

    public SongView SongView { get { return _songView = Assert<SongView>(_songView); } }
    private SongView _songView;

}
