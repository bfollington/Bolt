using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float scale = 2;

	// Use this for initialization
	void Start () {

		
	
	}
	
	// Update is called once per frame
	void Update () {

		GetComponent<Camera>().orthographicSize = Screen.height / (scale * 2);
	
	}
}
