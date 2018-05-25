using UnityEngine;
using System.Collections;

public class LookAtMainCamera : MonoBehaviour {

	void Update () {
        //transform.LookAt(Camera.main.transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Camera.main.transform.rotation, 10 * Time.deltaTime);

	}
}
