using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {


	void Start ()
	{

	    switch (Application.loadedLevelName)
	    {
	        case "LOGO":
	            LoadingScene("Manual");
	            break;
	        case "Manual":
	            LoadingScene("Main");
	            break;
	    }
	}


    private void LoadingScene(string name)
    {
        StartCoroutine("LoadAnimator", name);
       
    }


    IEnumerator LoadAnimator(string name)
    {
        yield return new WaitForSeconds(2);

        SMGameEnvironment.Instance.SceneManager.LoadScreen(name);
    }
}
