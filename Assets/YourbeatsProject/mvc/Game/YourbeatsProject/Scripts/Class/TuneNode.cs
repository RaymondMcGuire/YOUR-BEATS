using UnityEngine;

namespace JudgementSystemManager
{
   public class TuneNode
    {
       //音符时间
       public float TuneNodetime { get; private set; }
       //音符物体
       public GameObject TuneNodeGameObj { get; private set; }

       public TuneNode(float time, GameObject tuneNodeGameObj)
       {
           TuneNodetime = time;
           TuneNodeGameObj = tuneNodeGameObj;
       }

       public TuneNode(){}
    }
}
