using UnityEngine;
using System.Collections;
using System;

public class LevelGenerator : MonoBehaviour
{
	public int Width = 5;
	public int Height = 5;
	public int Seed = 0;
	public float MinRoomSize = 3;
	public int Iterations = 2;
	public float MinAspect = 4;
	public float SplitVariation = 0.6f;
	public float SplitStep = 5.0f;
	ArrayList rooms=null;
	System.Random random;

	// Use this for initialization
	void Start ()
	{
		Generate ();
		DrawLevel ();
	}

	void DrawLevel() {
		GameObject wall = (GameObject)Resources.LoadAssetAtPath<GameObject>("Assets/Prefabs/Rooms/TestWall.prefab");

		if (rooms.Count > 0) {
			
			foreach(BSPRoom room in rooms) {
				
				GameObject floor = new GameObject();
				floor.name = "Room_"+room.Size.x.ToString()+"_"+room.Size.y.ToString();
				floor.transform.parent=this.transform;
				floor.transform.position=new Vector3 (transform.position.x - room.Position.x-room.Size.x / 2.0f,
				                                      transform.position.y-1,
				                                      transform.position.z + room.Position.y +room.Size.y/ 2.0f);

			
				//floor.transform.localScale = new Vector3 (room.Size.x * 10, 1, room.Size.y * 10);

				// Top wall
				for (int i=0; i<room.Size.y/10; i++) {
					GameObject walltop = Instantiate(wall) as GameObject;
						walltop.transform.parent = floor.transform;
						walltop.transform.position = new Vector3 (transform.position.x - room.Position.x-room.Size.x,
							                                          transform.position.y,
							                                          transform.position.z + (room.Position.y)+5f+10.0f*i);
				}
				// Left wall
				
				for (int i=0; i<room.Size.x/10; i++) {
					GameObject wallleft = Instantiate(wall) as GameObject;
					wallleft.transform.parent = floor.transform;
					wallleft.transform.Rotate(new Vector3(0,-90,0));
					wallleft.transform.position = new Vector3 (transform.position.x - room.Position.x-5 - 10.0f*i,
						                                           transform.position.y,
						                                           transform.position.z + room.Position.y);
				}

				

			}
		}
	}

	public void Clear(){
		int childs = transform.childCount;
		
		for (int i = childs - 1; i >= 0; i--)
		{
			GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
		}
	}

	public void Generate ()
	{
		Clear ();
		if (Seed > 0) {
			random = new System.Random (Seed);
		} else {
			random = new System.Random();
		}

		rooms = new ArrayList ();
		rooms.Add (new BSPRoom (
			new Vector2 (0, 0),
			new Vector2 (Width * 10.0f, Height * 10.0f))
		);
		ArrayList newrooms = new ArrayList ();

		for (int i=0; i<Iterations; i++) {
			foreach (BSPRoom room in rooms) {

				if (room.GetSize () > MinRoomSize) {
					// Splitting room into two

					float split = (float)random.NextDouble ();
					split = 0.5f+(split-0.5f)*SplitVariation; // Ensure split is not near border

					bool horizontal = true;

					if (room.Size.x/room.Size.y > MinAspect ) {
						horizontal=false;
					}


					if (horizontal) {//random.Next (0, 2) == 0) { // Horizontal split

						float newSizeY = (float)Math.Round(room.Size.y*split/SplitStep)*SplitStep;

						BSPRoom room1 = new BSPRoom (
						room.Position,
							new Vector2 (room.Size.x, newSizeY)
						);
						BSPRoom room2 = new BSPRoom (
							new Vector2 (room.Position.x, room.Position.y + newSizeY),
							new Vector2 (room.Size.x, room.Size.y -newSizeY)
						);
						newrooms.Add (room1);
						newrooms.Add (room2);
					} else { // Vertical split
						
						float newSizeX = (float)Math.Round(room.Size.x*split/SplitStep)*SplitStep;
						BSPRoom room1 = new BSPRoom (
						room.Position,
							new Vector2 (newSizeX, room.Size.y)
						);
						BSPRoom room2 = new BSPRoom (
							new Vector2 (room.Position.x + newSizeX, room.Position.y),
							new Vector2 (room.Size.x-newSizeX, room.Size.y)
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
		DrawLevel ();
	}

	void OnDrawGizmos ()
	{
		Gizmos.DrawWireCube (
			new Vector3 (transform.position.x - Width / 2.0f * 10.0f, transform.position.y, transform.position.z + Height / 2.0f * 10.0f),
			new Vector3 (Width * 10.0f, 6, Height * 10.0f)
		);

		try {
			foreach (BSPRoom room in rooms) {
				Gizmos.DrawWireCube(
					new Vector3 (transform.position.x - room.Position.x-room.Size.x / 2.0f,
				             transform.position.y-1,
				             transform.position.z + room.Position.y +room.Size.y/ 2.0f),
					new Vector3 (room.Size.x,
				             3,
				             room.Size.y)
					);
			}
		}
		catch {
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