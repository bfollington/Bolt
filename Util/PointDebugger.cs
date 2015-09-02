

using UnityEngine;
using System.Collections;

public class PointDebugger : MonoBehaviour {

	public float x;
	public float y;

	// Use this for initialization
	void Start () {
	
	}

	public void OnDrawGizmos() {

		Gizmos.color = new Color (0f, 1f , 0f, 0.5f);
		Gizmos.DrawWireCube (new Vector3 (gameObject.transform.position.x + x, gameObject.transform.position.y + y, 0),
		                     new Vector3 (1, 1, 1));
	}


	// Update is called once per frame
	void Update () {
	
	}
}
