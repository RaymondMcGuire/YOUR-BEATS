using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;




public enum UIDirecrion
{
    Left,
    Right,
    Up,
    Down
}



/// <summary>
/// UI层级界面
/// </summary>
public class UILayer
{
    private bool _uiState;

    /// <summary>
    /// 对应UI界面名称
    /// </summary>
    public string UiName { get; set; }

    /// <summary>
    /// 对应UI界面实体对象
    /// </summary>
    public Canvas UiCanvas { get ; set; }

    /// <summary>
    /// 对应UI界面中所有图片
    /// </summary>
    public Image[] UiImageList { get; set; }

    /// <summary>
    /// 刷新
    /// </summary>
    public void Refresh()
    {
        if (UiImageList != null && UiWordList != null)

        UiImageList = new Image[GetRescursive<Image>(UiCanvas.gameObject).Count];
        GetRescursive<Image>(UiCanvas.gameObject).CopyTo(UiImageList);
        UiWordList = new Text[GetRescursive<Text>(UiCanvas.gameObject).Count];
        GetRescursive<Text>(UiCanvas.gameObject).CopyTo(UiWordList);
    }


    /// <summary>
    /// 对应UI界面中所有文字
    /// </summary>
    public Text[] UiWordList { get; set; }

    /// <summary>
    /// 上一级菜单列表
    /// </summary>
    public List<string> ParentList { get; set; }

    /// <summary>
    /// 下一级菜单列表
    /// </summary>
    public Dictionary<UIDirecrion,string> ChildDictionary { get; set; }

    //初始化大小
    private const float InitScale = 1f;

    /// <summary>
    /// 当前UI状态
    /// </summary>
    public bool UiState
    {
        set
        {           
            if (!UiCanvas) return;
            _uiState = value;

            UiCanvas.gameObject.SetActive(_uiState);
            UiImageList = new Image[GetRescursive<Image>(UiCanvas.gameObject).Count];
            GetRescursive<Image>(UiCanvas.gameObject).CopyTo(UiImageList);
            UiWordList = new Text[GetRescursive<Text>(UiCanvas.gameObject).Count];
            GetRescursive<Text>(UiCanvas.gameObject).CopyTo(UiWordList);

            UiCanvas.transform.localScale = new Vector3(InitScale, InitScale, InitScale);
            //初始化设置大小
            foreach (var img in UiImageList)
            {
                img.color = new Color(img.color.r, img.color.r, img.color.r, 1);
            }
        }
        get  { return _uiState; }
    }

    /// <summary>
    /// 递归获取
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    private static List<T> GetRescursive<T>(GameObject obj)
    {
        var l = new List<T>();

        if (obj == null) return l;


        if (obj.transform.GetComponentsInChildren<T>().Length != 0)
                l.AddRange(obj.transform.GetComponentsInChildren<T>());
  

        if (obj.transform.childCount == 0) return l;

        foreach (var tr in obj.transform.Cast<Transform>().Where(tr => GetRescursive<T>(tr.gameObject).Count != 0))
            l.AddRange(GetRescursive<T>(tr.gameObject));

        return l;       
    }
}


/// <summary>
/// UI切换状态
/// </summary>
public enum UILayerState
{
    ParentLayer,
    ChildLayer
}
