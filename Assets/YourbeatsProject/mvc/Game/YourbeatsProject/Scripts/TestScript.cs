using UnityEngine;

using System.Collections;

public class TestScript : MonoBehaviour {
    ////单音
    public GameObject Singletune;
    public GameObject FWDTune;
    public GameObject BWDTune;

    void Update () {
        if (Input.GetKeyUp(KeyCode.Z) )
        Instantiate(Singletune, new Vector3(0, 0, 0), Quaternion.identity);

        if (Input.GetKeyUp(KeyCode.X))  
            Instantiate(FWDTune, new Vector3(0, 0, 0), Quaternion.identity);

        if (Input.GetKeyUp(KeyCode.C))
            Instantiate(BWDTune, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
