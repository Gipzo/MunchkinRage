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
	ArrayList rooms=null;
	System.Random random;

	// Use this for initialization
	void Start ()
	{
		Generate ();
		DrawLevel ();
	}

	void DrawLevel() {
		if (rooms.Count > 0) {
			
			foreach(BSPRoom room in rooms) {
				GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
				go.transform.parent=this.transform;
				go.transform.position=new Vector3 (transform.position.x - room.Position.x*10-room.Size.x / 2 * 10, transform.position.y, transform.position.z + (room.Position.y-0.5f)*10 +room.Size.y/ 2 * 10);
				go.transform.localScale = new Vector3 (room.Size.x * 10, 3, room.Size.y * 10);
				

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
			new Vector2 (Width, Height))
		);
		ArrayList newrooms = new ArrayList ();

		for (int i=0; i<Iterations; i++) {
			foreach (BSPRoom room in rooms) {

				if (room.GetSize () > MinRoomSize) {
					// Splitting room into two

					float split = (float)random.NextDouble ();
					split = 0.4f * split + 0.4f; // Ensure split is not near border

					bool horizontal = true;

					if (room.Size.x/room.Size.y > MinAspect ) {
						horizontal=false;
					}


					if (horizontal) {//random.Next (0, 2) == 0) { // Horizontal split
						BSPRoom room1 = new BSPRoom (
						room.Position,
						new Vector2 (room.Size.x, room.Size.y * split)
						);
						BSPRoom room2 = new BSPRoom (
						new Vector2 (room.Position.x, room.Position.y + room.Size.y * split),
						new Vector2 (room.Size.x, room.Size.y * (1.0f - split))
						);
						newrooms.Add (room1);
						newrooms.Add (room2);
					} else { // Vertical split
						BSPRoom room1 = new BSPRoom (
						room.Position,
						new Vector2 (room.Size.x * split, room.Size.y)
						);
						BSPRoom room2 = new BSPRoom (
						new Vector2 (room.Position.x + room.Size.x * split, room.Position.y),
						new Vector2 (room.Size.x * (1.0f - split), room.Size.y)
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
			new Vector3 (transform.position.x - Width / 2 * 10, transform.position.y, transform.position.z + Height / 2 * 10),
			new Vector3 (Width * 10, 6, Height * 10)
		);


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