using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {


	public Transform Room1=null;
	public Transform Room2=null;

	public bool GoForward = false;
	public bool GoBackward = false;
	
	GameObject ForwardPath;
	GameObject BackwardPath;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (ForwardPath != null) {
			ForwardPath.renderer.enabled = GoForward;
		} else {
			
			foreach (Transform child in transform) {
			if (child.gameObject.name == "ForwardArrow") {
				ForwardPath = child.gameObject;
			}
			}
		}
		if( BackwardPath !=null) {
			BackwardPath.renderer.enabled = GoBackward;
		} else {
			foreach (Transform child in transform) {


				if (child.gameObject.name == "BackwardArrow") {
					BackwardPath = child.gameObject;
				}
			}

			//BackwardPath = transform.Find("BackPath").gameObject;
		}
		
	}
}
