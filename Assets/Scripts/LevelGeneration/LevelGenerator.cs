using UnityEngine;
using System.Collections;
using System;

public class LevelGenerator : MonoBehaviour
{
	 public int Width = 5;
	 public int Height = 5;
	public int Seed = 0;
	 float MinRoomSize = 300.0f;
	 int Iterations = 2;
	float MinAspect = 1;
	 float SplitVariation = 0.6f;
	public float SplitStep = 6.4f;
	 float RoomThreshold = 500.0f;
	public bool GenerateLights = true;
	public string MainEnemy = "Orc";
	public string[] OtherTypes = {"Skeleton","Goblin"};
	public int CurrentLevel = 1;
	public Inventory Inventory;
	public int Orcs = 0;
	public int Skeletons = 0;
	public int Goblins = 0;
	public string ChiefType = "Orc";

	
	public GameObject wall = null;
	public GameObject door = null;
	public GameObject floor =null;
	public GameObject player = null;
	public GameObject exit =null;

	public GameObject chest = null;

	public GameObject[] orcs;
	public GameObject[] skeletons;
	public GameObject[] goblins;
	public GameObject damage;

	ArrayList rooms = null;
	System.Random random;

	// Use this for initialization
	void Start ()
	{
		
		(GameObject.Find("dead").GetComponent("ToMenuScript") as ToMenuScript).act=false;
		SetParams ();
		Clear ();
		Generate ();
		//DrawLevel ();
		Transfer2D ();
	}


	void Transfer2D() {
		//transform.Rotate (new Vector3 (0, 90, -90));
	}

	public void NextLevel() {
		if (CurrentLevel < 10)
			CurrentLevel++;

		Generate ();
		(GameObject.Find ("Camera").gameObject.GetComponent("CameraController") as CameraController).isGame=false;


	}

	void SetParams() {
		
		//transform.Rotate (new Vector3 (0, -90, 90));
		string[] Enemies = new string[] {"Orc", "Goblin", "Skeleton"};

		MainEnemy = Enemies[UnityEngine.Random.Range(0, Enemies.Length)];
		int c = 0;
		for (int i=0; i<Enemies.Length; i++) {
			if (MainEnemy!=Enemies[i]) {
				OtherTypes[c]=Enemies[i];
				c++;
			}
		}

		Orcs = 0;
		Goblins = 0;
		Skeletons = 0;

		switch (CurrentLevel) {
		case 1:
			Width=4;
			Height=5;
			MinRoomSize=300;
			Iterations=5;
			SplitVariation=0.7f;
			RoomThreshold=10;
			break;
		case 2:
			Width=5;
			Height=4;
			MinRoomSize=200;
			Iterations=6;
			SplitVariation=0.7f;
			RoomThreshold=10;
			break;
		case 3:
			Width=5;
			Height=5;
			MinRoomSize=200;
			Iterations=4;
			SplitVariation=0.7f;
			RoomThreshold=10;
			break;
		case 4:
			Width=5;
			Height=5;
			MinRoomSize=200;
			Iterations=5;
			SplitVariation=0.7f;
			RoomThreshold=10;
			break;
		case 5:
			Width=6;
			Height=5;
			MinRoomSize=200;
			Iterations=5;
			SplitVariation=0.7f;
			RoomThreshold=10;
			break;
		case 6:
			Width=6;
			Height=5;
			MinRoomSize=200;
			Iterations=5;
			SplitVariation=0.7f;
			RoomThreshold=10;
			break;
		case 7:
			Width=5;
			Height=6;
			MinRoomSize=200;
			Iterations=5;
			SplitVariation=0.7f;
			RoomThreshold=10;
			break;
		case 8:
			Width=7;
			Height=5;
			MinRoomSize=200;
			Iterations=6;
			SplitVariation=0.7f;
			RoomThreshold=10;
			break;
		case 9:
			Width=7;
			Height=6;
			MinRoomSize=200;
			Iterations=7;
			SplitVariation=0.7f;
			RoomThreshold=10;
			break;
		case 10:
			Width=5;
			Height=8;
			MinRoomSize=200;
			Iterations=7;
			SplitVariation=0.7f;
			RoomThreshold=10;
			break;
		}
	}

	void CreateLight(Vector3 Position, float Range, Transform Room) {
		GameObject lightGameObject = new GameObject ("RoomLight");
		lightGameObject.AddComponent ("Light");
		lightGameObject.transform.parent = Room;
		lightGameObject.light.type = LightType.Point;
		lightGameObject.light.enabled = true;
		lightGameObject.light.range = Range;
		lightGameObject.transform.position = Position;
	}
	

	void CreateLightsForRoom (BSPRoom room, GameObject roomGO)
	{
		if (room.Size.x / room.Size.y > 2f) { // Room is horizontal
			float lightsCount = (float)Math.Ceiling (room.Size.x / (room.Size.y)) - 1;
			
			for (int i=0; i< lightsCount; i++) {
				Vector3 lightPos = new Vector3 (
					transform.position.x - room.Position.x - room.Size.x / lightsCount * (i + 1) + 0.5f * room.Size.x / lightsCount,
					5.0f,
					transform.position.y + room.Position.y + room.Size.y / 2.0f
					);
				CreateLight(lightPos, room.GetSize () / 30.0f, roomGO.transform);
			}
			
			
		} else if (room.Size.y / room.Size.x > 2f) { // Room is vertical

			float lightsCount = (float)Math.Ceiling (room.Size.y / (room.Size.x)) - 1;
			
			for (int i=0; i< lightsCount; i++) {
				
				Vector3 lightPos = new Vector3 (
					transform.position.x - room.Position.x - room.Size.x / 2.0f,
					5.0f,
					transform.position.y + room.Position.y + room.Size.y / lightsCount * (i + 1) - 0.5f * room.Size.y / lightsCount
					);
				
				CreateLight(lightPos, room.GetSize () / 30.0f, roomGO.transform);	
			}
		} else { // Room is squarish
			Vector3 lightPos = new Vector3 (
				transform.position.x - room.Position.x - room.Size.x / 2.0f,
				5.0f + room.GetSize () / 100.0f,
				transform.position.y + room.Position.y + room.Size.y / 2.0f
				);
			CreateLight(lightPos, room.GetSize () / 20.0f, roomGO.transform);
		}
	}

	bool ValueInArray (Transform value, Transform[] array)
	{
		if (array == null) {
			return false;
		}
		for (int i=0; i<array.Length; i++) {
			if (array [i] == value)
				return true;
		}
		return false;
	}


	void AddWallsAndFloor(BSPRoom room, GameObject roomGO) {
		// Top wall
		for (int i=0; i<room.Size.y/6.4f-0.2f; i++) {
			GameObject walltop = GetPrefab ("wall");
			walltop.transform.parent = roomGO.transform;
			walltop.name="Wall";
			walltop.transform.Rotate (new Vector3 (0, 0, -90));
			walltop.transform.position = new Vector3 (transform.position.x - room.Position.x - room.Size.x,
			                                          transform.position.y + (room.Position.y) + 3.2f + 6.4f * i,
			                                          transform.position.z-0.5f);
		}
		// Left wall
		for (int i=0; i<room.Size.x/6.4f-0.1f; i++) {
			GameObject wallleft = GetPrefab ("wall");
			wallleft.transform.parent = roomGO.transform;
			wallleft.transform.Rotate (new Vector3 (0, 0, 0));
			wallleft.name="Wall";
			wallleft.transform.position = new Vector3 (transform.position.x - room.Position.x - 6.4f * i-3.2f,
			                                           transform.position.y + room.Position.y,
			                                           transform.position.z-1.0f);
		}
		// Bottom wall
		for (int i=0; i<room.Size.y/6.4f-0.2f; i++) {
			Vector3 v3 = new Vector3 (transform.position.x - room.Position.x,
			                          transform.position.y + (room.Position.y) + 3.2f + 6.4f * i,
			                          transform.position.z-0.5f);
			
			if (!Physics.CheckSphere (v3, 1.6f, 1 << LayerMask.NameToLayer ("Walls"))) {
				GameObject walltop = GetPrefab ("wall");
				walltop.transform.parent = roomGO.transform;
				walltop.transform.Rotate (new Vector3 (0, 0, 90));
				walltop.name="Wall";
				walltop.transform.position = v3;
			}
		}

		// Right wall
		for (int i=0; i<room.Size.x/6.4f-0.2f; i++) {
			Vector3 v3 = new Vector3 (transform.position.x - room.Position.x - 3.2f - 6.4f * i,
			                          transform.position.y + room.Position.y + room.Size.y,
			                          transform.position.z-1.0f);
			if (!Physics.CheckSphere (v3, 2.0f, LayerMask.NameToLayer ("Walls"))) {
				GameObject wallleft = GetPrefab ("wall");
				wallleft.transform.parent = roomGO.transform;
				wallleft.name="Wall";
				wallleft.transform.Rotate (new Vector3 (0, 0, 0));
				wallleft.transform.position = v3;
			}
		}
		

	}

	GameObject GetPrefab(string type) {
		GameObject prefab = null;
		switch (type) {
		case "wall":
			prefab= wall;
			break;
		case "door":
			prefab= door;
			break;
			
		case "floor":
			prefab= floor;
			break;
		case "exit":
			prefab= exit;
			break;
		case "player":
			prefab= player;
			break;
		}
		if (prefab == null)
			return null;
		return Instantiate (prefab) as GameObject;
	}

	void DrawLevel ()
	{
		// Create floor
		GameObject floor = GetPrefab("floor");
		floor.transform.parent=transform;
		floor.transform.position = new Vector3(-Width*6.4f/2f, Height*6.4f/2f,1.0f);
		floor.transform.localScale = new Vector3(Width, Height, 1.0f);
		floor.transform.Find("Quad").gameObject.renderer.sharedMaterial.mainTextureScale = new Vector2(Width*6, Height*6);
		


		if (rooms.Count > 0) {
			
			foreach (BSPRoom room in rooms) {
				
				GameObject roomGO = new GameObject ();

				// Set GameObject params
				room.GO = roomGO;
				roomGO.name = "Room_" + room.Position.x.ToString () + "_" + room.Position.y.ToString ();
				roomGO.transform.parent = this.transform;
				roomGO.transform.position = new Vector3 (transform.position.x - room.Position.x - room.Size.x / 2.0f,
				                                         transform.position.y + room.Position.y + room.Size.y / 2.0f,
				                                         transform.position.z - 1
				                                      );
				roomGO.layer = LayerMask.NameToLayer ("Rooms");


		


				// Set BoxCollider
					roomGO.AddComponent ("BoxCollider2D");
				(roomGO.GetComponent ("BoxCollider2D") as BoxCollider2D).size = new Vector2 (room.Size.x, room.Size.y);
				(roomGO.GetComponent ("BoxCollider2D") as BoxCollider2D).isTrigger = true;
					//(roomGO.GetComponent ("BoxCollider") as BoxCollider).name = "RoomTrigger";
				(roomGO.GetComponent ("BoxCollider2D") as BoxCollider2D).center = new Vector2(0,0);

				// Create lights
				if (1==0 && GenerateLights) {
					CreateLightsForRoom (room, roomGO);
				}

				// Adding RoomInfo
				roomGO.AddComponent("RoomInfo");
				RoomInfo roomInfo = roomGO.GetComponent ("RoomInfo") as RoomInfo;
				roomInfo.Size = room.Size;
				roomInfo.Position = room.Position;
				roomInfo.Level = this.transform;

				// Adding walls and floor
				AddWallsAndFloor(room, roomGO);

			}

			BSPRoom Start = rooms[0] as BSPRoom;
			BSPRoom End = rooms[1] as BSPRoom;

			// Populate neighbour rooms
			foreach (BSPRoom room in rooms) {
				if (Start.Position.x>room.Position.x && Start.Position.y>room.Position.y) {
					Start = room;
				}
				if (End.Position.x<room.Position.x + room.Size.x && End.Position.y<room.Position.y+room.Size.y) {
					End = room;
				}


				Vector3 testPoint = new Vector2 (transform.position.x - room.Position.x - room.Size.x / 2.0f,

				                                 transform.position.y + room.Position.y + room.Size.y / 2.0f);

				
				var colliders = Physics2D.OverlapCircleAll (testPoint, 1.0f,
				                                       1 << LayerMask.NameToLayer("Rooms"));




				if (colliders.Length > 0) {
					GameObject roomGO = colliders [0].gameObject;
					RoomInfo currentRoomInfo = roomGO.GetComponent ("RoomInfo") as RoomInfo;
					for (int ii=0; ii<Math.Ceiling(currentRoomInfo.Size.x)/10; ii++) {
						for (int jj=0; jj<Math.Ceiling (currentRoomInfo.Size.y)/10; jj++) {
							Vector2 currentPoint = new Vector2 (
								transform.position.x - currentRoomInfo.Position.x - 6.4f * ii - 3.2f,
								currentRoomInfo.Position.y + 6.4f * jj + 3.2f
							);


							Vector2 pointleft = new Vector2 (
								currentPoint.x - 6.4f,
								currentPoint.y);

							Vector2 pointright = new Vector2 (
								currentPoint.x + 6.4f,
								currentPoint.y);

							Vector2 pointtop = new Vector2 (
								currentPoint.x,
								currentPoint.y+6.4f);

							Vector2 pointbottom = new Vector2 (
								currentPoint.x,
								currentPoint.y - 6.4f);

							foreach (Vector2 point in new Vector2[] {pointleft, pointright, pointtop, pointbottom}) {
								var col = Physics2D.OverlapCircleAll (point, 3.0f, 1 << LayerMask.NameToLayer ("Rooms"));


								if (col.Length > 0) {	

									GameObject targetRoom = col [0].gameObject;
									RoomInfo targetRoomInfo = targetRoom.GetComponent("RoomInfo") as RoomInfo;
									if (targetRoom != roomGO && !ValueInArray (targetRoom.transform, currentRoomInfo.nearRooms)) {
										currentRoomInfo.AddRoom(targetRoom.transform);
										targetRoomInfo.AddRoom(roomGO.transform);
									

										// Clear wall
										Vector3 wallpoint = new Vector3 (
										(currentPoint.x + point.x) / 2.0f,
											(currentPoint.y + point.y) / 2.0f,
											-1.0f
										
										);


								
										var wallcol = Physics2D.OverlapCircleAll (wallpoint, 1.6f, 1<<LayerMask.NameToLayer("Walls"));



										if (wallcol.Length > 0) {
											Quaternion rotation = new Quaternion ();
											for (int w=wallcol.Length-1; w>=0; w--) {

												GameObject wallToDestroy = wallcol [w].gameObject;
												if (wallToDestroy.name == "Wall") {
													rotation = wallToDestroy.transform.rotation;
													DestroyImmediate(wallToDestroy);



												}
											}

											GameObject door = GetPrefab("door");
											door.name="Door";
											DoorScript doorScript = door.GetComponent("DoorScript") as DoorScript;
											door.transform.parent = roomGO.transform;
											door.transform.rotation = rotation;
											door.transform.position = new Vector3 (
												wallpoint.x,
												wallpoint.y,0.0f
												);
											
											if (doorScript != null) {
												doorScript.Room1 = roomGO.transform;
												doorScript.Room2 = targetRoom.transform;
												currentRoomInfo.AddDoor(door.transform);
												targetRoomInfo.AddDoor(door.transform);
											}

										}
									

									}
								

								}
							}
						}
					}

				}
			}


			LevelPathSelector lps = GetComponent("LevelPathSelector") as LevelPathSelector;
			lps.StartRoom=Start.GO.transform;
			lps.EndRoom = End.GO.transform;
			lps.CurrentRoom = lps.StartRoom;
			lps.EndReached = false;
			lps.SelectedRooms = new Transform[1] {lps.StartRoom};
			lps.SelectedDoors = new DoorScript[0] {};
			lps.RefreshMarkers();
			
			GameObject player =GetPrefab("player");
			player.AddComponent("CharcterBattle");
			BattleSystem bs = player.GetComponent("BattleSystem") as BattleSystem;
			bs.level=gameObject.transform;
			(Start.GO.GetComponent("RoomInfo") as RoomInfo).OpenRoom(this.gameObject);
			bs.room = Start.GO.GetComponent("RoomInfo") as RoomInfo;
			player.name = "Player";
			(player.GetComponent("CharcterState") as CharcterState).inventory = Inventory;
			player.transform.parent=this.transform;
			player.transform.position= new Vector3 (
				Start.GO.transform.position.x,
				Start.GO.transform.position.y+2.5f,
				Start.GO.transform.position.z
				);

			GameObject endMarker = GetPrefab("exit");
			endMarker.name="Exit";
			endMarker.transform.parent=this.transform;
			endMarker.transform.position= new Vector3 (
				End.GO.transform.position.x,
				End.GO.transform.position.y+1,
				End.GO.transform.position.z
				);


			int monstersCount = (Int32)Math.Round(0.75f*(rooms.Count-2));
			int chests = 1;

			while (monstersCount>0) {
				GameObject room = (rooms[UnityEngine.Random.Range(0,rooms.Count)] as BSPRoom).GO;
				RoomInfo roomInfo = room.GetComponent("RoomInfo") as RoomInfo;
				if (!roomInfo.HasEnemy && room!=Start.GO && room!=End.GO) {
					roomInfo.HasEnemy = true;
					roomInfo.EnemyLevel = CurrentLevel;

					if (UnityEngine.Random.Range (0, 1.0f) < 0.75f) {
						roomInfo.ChoosenEnemyType = MainEnemy; 
					} else {
						roomInfo.ChoosenEnemyType = OtherTypes [UnityEngine.Random.Range (0, OtherTypes.Length - 1)];
					}

					switch (roomInfo.ChoosenEnemyType) {
					case "Orc":
						Orcs++;
						break;
					case "Goblin":
						Goblins++;
						break;
					case "Skeleton":
						Skeletons++;
						break;
					
					}

					monstersCount--;

				}

				if (chests>0 && UnityEngine.Random.Range (0,1.0f)<0.4f && room!=Start.GO) {
					roomInfo.HasChest=true;
					chests--;
				}
			}
			ChiefType = "Orc";
			if (UnityEngine.Random.Range (0, 1.0f) < 0.75f) {
				ChiefType = MainEnemy; 
			} else {
				ChiefType = OtherTypes [UnityEngine.Random.Range (0, OtherTypes.Length - 1)];
			}

			RoomInfo endinfo = End.GO.GetComponent("RoomInfo") as RoomInfo;
			endinfo.HasEnemy=true;
			endinfo.EnemyLevel=CurrentLevel+1;
			endinfo.ChoosenEnemyType = ChiefType;

			foreach (BSPRoom room in rooms) {
				if (room == Start || room == End) continue;

				RoomInfo roomInfo = room.GO.GetComponent("RoomInfo") as RoomInfo;

				

			}

		}
	}

	public void Clear ()
	{
		int childs = transform.childCount;
		
		for (int i = childs - 1; i >= 0; i--) {
			//try {
			//	GameObject.Destroy(transform.GetChild (i).gameObject);
			//}
			//catch {
			GameObject.DestroyImmediate (transform.GetChild (i).gameObject);
			//}
		}
		try {
			rooms.Clear ();
		} catch {
		}
	}

	public void Generate ()
	{
		
		SetParams ();
		Clear ();
		if (Seed > 0) {
			random = new System.Random (Seed);
		} else {
			random = new System.Random ();
		}

		rooms = new ArrayList ();
		rooms.Add (new BSPRoom (
			new Vector2 (0, 0),
			new Vector2 (Width * 6.4f, Height * 6.4f))
		);
		ArrayList newrooms = new ArrayList ();

		for (int i=0; i<Iterations; i++) {
			foreach (BSPRoom room in rooms) {

				if (room.GetSize () > MinRoomSize) {
					// Splitting room into two

					float split = (float)random.NextDouble ();
					split = 0.5f + (split - 0.5f) * SplitVariation; // Ensure split is not near border

					bool horizontal = true;

					if (room.Size.x / room.Size.y > MinAspect) {
						horizontal = false;
					}


					if (horizontal) {//random.Next (0, 2) == 0) { // Horizontal split

						float newSizeY = (float)Math.Round (room.Size.y * split / SplitStep) * SplitStep;

						BSPRoom room1 = new BSPRoom (
						room.Position,
							new Vector2 (room.Size.x, newSizeY)
						);
						BSPRoom room2 = new BSPRoom (
							new Vector2 (room.Position.x, room.Position.y + newSizeY),
							new Vector2 (room.Size.x, room.Size.y - newSizeY)
						);
						newrooms.Add (room1);
						newrooms.Add (room2);
					} else { // Vertical split
						
						float newSizeX = (float)Math.Round (room.Size.x * split / SplitStep) * SplitStep;
						BSPRoom room1 = new BSPRoom (
						room.Position,
							new Vector2 (newSizeX, room.Size.y)
						);
						BSPRoom room2 = new BSPRoom (
							new Vector2 (room.Position.x + newSizeX, room.Position.y),
							new Vector2 (room.Size.x - newSizeX, room.Size.y)
						);
						newrooms.Add (room1);
						newrooms.Add (room2);
					}

				} else {
					newrooms.Add (room);
				}
			}
			rooms.Clear ();
			rooms.AddRange (newrooms);
			newrooms.Clear ();
		}

		FilterRooms ();

		DrawLevel ();
	}

	void FilterRooms ()
	{
		for (int i=rooms.Count-1; i>=0; i--) {
			if ((rooms [i] as BSPRoom).GetSize () < RoomThreshold) {
				rooms.Remove (rooms [i]);
			}
		}
	}

	void OnDrawGizmos ()
	{
		return;
		Gizmos.DrawWireCube (
			new Vector3 (transform.position.x - Width / 2.0f * 10.0f, transform.position.y, transform.position.z + Height / 2.0f * 10.0f),
			new Vector3 (Width * 10.0f, 6, Height * 10.0f)
		);

		try {
			foreach (BSPRoom room in rooms) {
				Gizmos.DrawWireCube (
					new Vector3 (transform.position.x - room.Position.x - room.Size.x / 2.0f,
				             transform.position.y - 1,
				             transform.position.z + room.Position.y + room.Size.y / 2.0f),
					new Vector3 (room.Size.x,
				             3,
				             room.Size.y)
				);
			}
		} catch {
		}

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

public class BSPRoom
{

	public Vector2 Position;
	public Vector2 Size;
	public GameObject GO;

	public BSPRoom (Vector2 position, Vector2 size)
	{
		Position = position;
		Size = size;
	}

	public float GetSize ()
	{
		return Size.x * Size.y;
	}

}