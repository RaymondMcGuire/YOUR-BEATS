//-----------------------------------------------【Function Indroduction】----------------------------------------------
//	  EditorTools：  
//    Language：  C#
//    IDE：VS2013
//    2015.12.04  Created by RaymondMG  
//    2015.12.04  LastEdited by RaymondMG  
//---------------------------------------------------------------------------------------------------------------------
using UnityEngine;
using UnityEditor;


namespace EncognitaTools
{

    /// <summary>
    /// 编辑器工具
    /// </summary>
    public class EditorTools : MonoBehaviour
    {
        /// <summary>
        /// 音乐类型配置工具
        /// </summary>
        [MenuItem("EncognitaTools/MusicTypeConfig")]
        public static void MusicTypeConfig()
        {
            EditorToolsUI.GetInstance().Init();
        }
    }


    /// <summary>
    /// 隐藏场景物体
    /// </summary>
    public class UIToolbar : EditorWindow
    {

        [MenuItem("EncognitaTools/UI Tooblar")]
        static void Init()
        {
            EditorWindow.GetWindow(typeof(UIToolbar));
        }
        private int previousLayers;
        void OnGUI()
        {
            bool visibleUI = ((Tools.visibleLayers & (1 << 5)) == 1);
            if (GUILayout.Button(visibleUI ? "Hide UI" : "Show UI"))
            {
                Tools.visibleLayers = Tools.visibleLayers ^ (1 << 5);
            }
            if (GUILayout.Button("Toggle All"))
            {
                int layers = Tools.visibleLayers;
                for (int i = 0; i < 32; i++)
                {
                    layers ^= (1 << i);
                }
                Tools.visibleLayers = layers;
            }
        }
    }


   
}

