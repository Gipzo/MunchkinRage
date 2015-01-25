using UnityEngine;
using System.Collections;

public class ToMenuScript : MonoBehaviour {

	public bool act=false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (act && Input.anyKeyDown) {
			act=false;
			transform.position=new Vector3(-12f,-51.4f,-10f);
			(GameObject.Find("Level").GetComponent("LevelGenerator") as LevelGenerator).Generate();
		}
	
	}

	void OnMouseDown() {
		if (act) {
			act=false;
			transform.position=new Vector3(-12f,-51.4f,-10f);
			(GameObject.Find("Level").GetComponent("LevelGenerator") as LevelGenerator).Generate();
		}
	}
}
