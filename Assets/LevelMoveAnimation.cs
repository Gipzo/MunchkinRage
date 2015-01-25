using UnityEngine;
using System.Collections;

public class LevelMoveAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float s = (float)Mathf.Sin (Time.time * 5.0f);
		float rot = 1;
		if (name == "BackPath")
			rot = -1;
		transform.localPosition = new Vector3 (
			rot*s*0.5f,
			8.0f,
			transform.localPosition.z
			);

	}
}
