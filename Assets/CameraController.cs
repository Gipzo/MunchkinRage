using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public bool isGame = false;
	public Transform level = null;

	public Vector3 targetPos = new Vector3(-12f,-16f,-24f);
	public float targetSize=20.0f;
	public float speed=5f;

	public float gameSize = 15.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!isGame) {
			if (level != null) {
				LevelGenerator lg = (level.gameObject.GetComponent ("LevelGenerator") as LevelGenerator);
				targetSize = lg.Height * 6.4f / 2f + 6.4f;
				targetPos = new Vector3 (
					-lg.Width * 3.2f,
					lg.Height * 3.2f,	
					-20.0f
				);
			}


		} else {
			targetSize = gameSize;
			targetPos = GameObject.Find ("Player").transform.position;
			targetPos = new Vector3(targetPos.x, targetPos.y, transform.position.z);
		}
		
		camera.orthographicSize = Mathf.Lerp (camera.orthographicSize, targetSize, speed*Time.deltaTime);
		transform.position = Vector3.Lerp (transform.position, targetPos, speed * Time.deltaTime);
	}
}
