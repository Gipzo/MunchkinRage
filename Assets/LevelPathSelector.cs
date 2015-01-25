using UnityEngine;
using System;
using System.Collections;

public class LevelPathSelector : MonoBehaviour {

	public bool SelectTime = true;
	public Transform StartRoom=null;
	public Transform EndRoom=null;
	public Transform CurrentRoom=null;
	public Transform PathMarkers = null;
	public Transform[] SelectedRooms;
	public DoorScript[] SelectedDoors;
	public bool EndReached = false;
	DoorScript lastDoor = null;

	public GameObject[] numbers;

	bool lastForward = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	public GameObject GetNumberPrefab(int i) {
		GameObject prefab = numbers [i - 1];
		return Instantiate (prefab) as GameObject;
	}

	void ClearMarkers() {
		int childs = PathMarkers.transform.childCount;
		
		for (int i = childs - 1; i >= 0; i--) {
			//try {
			//	GameObject.Destroy(transform.GetChild (i).gameObject);
			//}
			//catch {
			GameObject.DestroyImmediate (PathMarkers.transform.GetChild (i).gameObject);
			//}
		}
		
		for (int i=0; i<SelectedRooms.Length; i++) {
			RoomInfo ri = (SelectedRooms[i].gameObject.GetComponent("RoomInfo") as RoomInfo);
			ri.numberCount = 0;
		}
	}

	public void RefreshMarkers() {
		ClearMarkers ();
		for (int i=0; i<SelectedRooms.Length; i++) {
			RoomInfo ri = (SelectedRooms[i].gameObject.GetComponent("RoomInfo") as RoomInfo);
			ri.numberCount++;
			GameObject text = GetNumberPrefab(i+1);
			text.transform.parent=PathMarkers.transform;
			text.transform.position=new Vector3(SelectedRooms[i].transform.position.x+ri.numberCount*4.2f-6.4f, SelectedRooms[i].transform.position.y,-2.0f);
		}
	}


	DoorScript[] GetDoors() {
		DoorScript[] doors = new DoorScript[0];
		for (int i=0; i<SelectedRooms.Length-1; i++) {
			var FirstRoom = SelectedRooms[i];
			RoomInfo CurrentRoomInfo = SelectedRooms[i].gameObject.GetComponent("RoomInfo") as RoomInfo;
			Transform Room = SelectedRooms[i+1];
			foreach (Transform door in CurrentRoomInfo.Doors) {
				DoorScript doorInfo = door.gameObject.GetComponent("DoorScript") as DoorScript;
				if (doorInfo.Room1 == FirstRoom && doorInfo.Room2==Room) {
					Array.Resize(ref doors, doors.Length+1);
					doors[doors.Length-1]=doorInfo;
				}
				else if (doorInfo.Room2 == FirstRoom && doorInfo.Room1==Room) {
					Array.Resize(ref doors, doors.Length+1);
					doors[doors.Length-1]=doorInfo;
				}
			}
		}
		return doors;
	}
	

	public void ToggleRoom(Transform Room) {

		if (Room == StartRoom)
			return;


		if (SelectedRooms.Length>0 && Room == SelectedRooms [SelectedRooms.Length - 1]) {
			if (Room == EndRoom) EndReached=false;
			Array.Resize(ref SelectedRooms, SelectedRooms.Length-1);
			CurrentRoom = SelectedRooms[SelectedRooms.Length-1];
			RefreshMarkers();

			return;
		}

		if (EndReached)
			return;
		bool AllowedRoom = false;

		RoomInfo CurrentRoomInfo = (CurrentRoom.gameObject.GetComponent ("RoomInfo") as RoomInfo);

		foreach (Transform roomTransform in CurrentRoomInfo.nearRooms) {
			if (Room == roomTransform) {
				AllowedRoom = true;
				break;
			}
		}

		if (AllowedRoom) {
			Array.Resize(ref SelectedRooms, SelectedRooms.Length+1);
			SelectedRooms [SelectedRooms.Length - 1] = Room;




			CurrentRoom = Room;


			if (Room == EndRoom) {
				EndReached=true;
				SelectedDoors = GetDoors();
				
				(GameObject.Find("Player").gameObject.GetComponent("BattleSystem") as BattleSystem).Doors=SelectedDoors;
				(GameObject.Find("Player").gameObject.GetComponent("BattleSystem") as BattleSystem).isActive=true;
				(GameObject.Find ("Camera").gameObject.GetComponent("CameraController") as CameraController).isGame=true;
				ClearMarkers();
				return;
			}
		}

		
		RefreshMarkers ();


	}


}
