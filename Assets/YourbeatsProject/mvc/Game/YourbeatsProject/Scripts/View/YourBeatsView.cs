//-----------------------------------------------【Function Indroduction】----------------------------------------------
//	  YourBeatsView：  
//    Language：  C#
//    IDE：VS2013
//    2015.11.14  Created by RaymondMG  
//    2015.11.27  LastEdited by RaymondMG  
//---------------------------------------------------------------------------------------------------------------------
using thelab.mvc;

public class YourBeatsView : View<YourBeatsApplication>
{

    void Awake()
    {
        //游戏Awake初始化
        Notify("game.awake.init");
    }

    void Start()
    {
        //游戏Start初始化
        Notify("game.start.init");
    }

    void Update()
    {
        //游戏每帧进行检测
        Notify("game.update");
    }
}

