using UnityEngine;
using System.Collections;

public class PlannetRotation : MonoBehaviour {
	public int rotationAroundSpeed;
	public Vector3 rotationOne;
	public Vector3 rotationTwo;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.RotateAround (rotationOne, rotationTwo, rotationAroundSpeed / 5 * Time.deltaTime);
		transform.Rotate(new Vector3(0,1,0),5*Time.deltaTime);
	}
}
