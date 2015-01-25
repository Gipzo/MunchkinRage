using UnityEngine;
using System;
using System.Collections;

public class RoomInfo : MonoBehaviour {


    public Transform[] transformEnemy; //готовые enemy

	public Transform[] nearRooms; // соседние комнаты

	public Transform Level;

	public Transform[] Doors = null;

    public int EnemyLevel;
	public string ChoosenEnemyType;
	public bool HasEnemy;
	public bool HasChest;
	public Vector2 Size;
	public Vector2 Position;

	public bool Open =false;

	public int numberCount = 0; 





	public GameObject GetEnemyPrefab() {
		GameObject prefab = null;
		if (ChoosenEnemyType == "Goblin") {
			prefab = (Level.GetComponent("LevelGenerator") as LevelGenerator).orcs[EnemyLevel];
		}
		if (ChoosenEnemyType == "Skeleton") {
			prefab = (Level.GetComponent("LevelGenerator") as LevelGenerator).skeletons[EnemyLevel];
		}
		if (ChoosenEnemyType == "Orc") {
			prefab = (Level.GetComponent("LevelGenerator") as LevelGenerator).goblins[EnemyLevel];
    }
    
			return Instantiate (prefab) as GameObject;
	}

	public Vector3 GetRandomPosition() {
		float padding = 3.0f;
		return new Vector3 (
			transform.position.x+Size.x/2.0f-UnityEngine.Random.Range(padding, Size.x-padding),

			transform.position.y-Size.y/2.0f+UnityEngine.Random.Range(padding, Size.y-padding),
			0
			);
	}

	public void SpawnEnemy() {
		if (HasEnemy) {
			GameObject enemy =GetEnemyPrefab();
			enemy.transform.parent = transform;
			enemy.transform.position = GetRandomPosition();
			enemy.AddComponent("EnemyBattle");
			transformEnemy = new Transform[1] {enemy.transform};
		}
	}

	public void SpawnChest() {
		GameObject prefab = (Level.GetComponent ("LevelGenerator") as LevelGenerator).chest;
		GameObject chest = Instantiate (prefab) as GameObject;
		chest.transform.parent = transform;
		chest.name = "Chest";
		chest.transform.position = GetRandomPosition ();
	}

	public void TurnLightOn() {
		foreach (Transform tr in transform) {
			if (tr.gameObject.name == "RoomLight") {
				tr.light.enabled=true;
			}
		}
	}

	public void OpenRoom(GameObject player) {
		if (!Open) {
			SpawnEnemy();
			if (HasChest) {
				SpawnChest();
			}
			Open=true;
		}
		TurnLightOn ();
	}

	public void AddDoor(Transform Door) {
		if (Doors == null) {
			Doors = new Transform[0];
		}

		
		foreach (Transform existingDoor in Doors) {
			if (existingDoor == Door) {
				return;
			}
		}

		Array.Resize (ref Doors, Doors.Length + 1);
		Doors [Doors.Length - 1] = Door;

	}

	public void AddRoom(Transform Room) {
		
		if (nearRooms == null) {
			nearRooms = new Transform[0];
		}

		foreach (Transform existingRoom in nearRooms) {
			if (existingRoom == Room) {
				return;
			}
		}

		Array.Resize (ref nearRooms, nearRooms.Length + 1);
		nearRooms [nearRooms.Length - 1] = Room;
	}

	void OnMouseDown(){
		//OpenRoom(this.gameObject);
		//Debug.Log ("Mouse down");
		LevelPathSelector lps = Level.gameObject.GetComponent ("LevelPathSelector") as LevelPathSelector;
		if (lps.SelectTime) {
			lps.ToggleRoom(this.transform);
		}
		}



    void Start() {

    }

}
