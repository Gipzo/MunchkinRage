using UnityEngine;
using System.Collections;

public class BattleSystem : MonoBehaviour {

    public bool isBattle;

    public Transform target;

    public RoomInfo room;

    private float distance1, distance2, distance3;


    void Start() {

        distance1 = 0;
        distance2 = 0;
        distance3 = 0;

    }


    void Update() {

        if (isBattle) {


            if (target == null)
            {

                SelectNearestTarget();

            }
            else if(Vector2.Distance(gameObject.transform.position, target.transform.position) > 0.3f ){

                GoToTarget();

            }

        
        }
    
    }

    void SelectNearestTarget() {




        if (room.GetComponent<RoomInfo>().transformEnemy.Length <= 2)
        {

            distance1 = Vector2.Distance(gameObject.transform.position, room.GetComponent<RoomInfo>().transformEnemy[0].transform.position);
            print(distance1);

        }

        if (room.GetComponent<RoomInfo>().transformEnemy.Length >= 2)
        {

            distance2 = Vector2.Distance(gameObject.transform.position, room.GetComponent<RoomInfo>().transformEnemy[1].transform.position);
            print(distance2);

        }


        if (room.GetComponent<RoomInfo>().transformEnemy.Length >= 3)
        {

            distance3 = Vector2.Distance(gameObject.transform.position, room.GetComponent<RoomInfo>().transformEnemy[2].transform.position);
            print(distance3);

        }


        SelectTarget();


    }


    void SelectTarget() {

        if (room.GetComponent<RoomInfo>().transformEnemy.Length <= 2)
        {

            
            

                target = room.GetComponent<RoomInfo>().transformEnemy[0];

               // print(distance1);

            
        }



        if (room.GetComponent<RoomInfo>().transformEnemy.Length >= 2 && room.GetComponent<RoomInfo>().transformEnemy.Length <= 3)
        {

            if (distance2 < distance1 && distance2 < distance3)
            {

                target = room.GetComponent<RoomInfo>().transformEnemy[1];

               // print(distance2);

            }
        }



        if (room.GetComponent<RoomInfo>().transformEnemy.Length >= 3)
        {

            if (distance3 < distance2 && distance3 < distance1)
            {

                target = room.GetComponent<RoomInfo>().transformEnemy[2];

               // print(distance3);

            }
        }
    
    }


    void GoToTarget() {

        transform.position = Vector2.Lerp(gameObject.transform.position, target.transform.position, 0.5f*Time.deltaTime);
    
    }

}
