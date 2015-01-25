using UnityEngine;
using System.Collections;

public class FloatText : MonoBehaviour {

	public float timeToLive = 0.2f;
	public float moveSpeed = 0.02f;
	public bool enabled=false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!enabled) return;
		timeToLive -= Time.deltaTime;
		if (timeToLive < 0.0f) {
			Destroy(gameObject);
		}
		transform.position = new Vector3 (transform.position.x, transform.position.y+moveSpeed, transform.position.z);
	}
}
