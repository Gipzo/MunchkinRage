using UnityEngine;
using System.Collections;

public class RoomInfo : MonoBehaviour {


    public Transform[] transformEnemy; //готовые enemy

    public Transform[] spawnPosition; //spawn позиция

    public Transform[] enemySet; //enemy в уровне

    public Transform[] prefabsEnemy; //enemy все виды

    public int maxEnemyLvl; //максимальное кол-во enemy на всём уровне
    
    public int maxEnemyinRoom; //максимальное кол-во enemy в комноте

    public int curLvl; //текущй уровень


    void Start()
    {
        SelectLvl();

        ChoceEnemy();

        SpawEnemy();

    }

    void SelectLvl() {

        switch (curLvl) { 
        
            case 1:

                maxEnemyinRoom = 1;
                break;

            case 2:

                maxEnemyinRoom = 2;
                break;

            case 3:

                maxEnemyinRoom = 3;
                break;

        
        }

    }

    void ChoceEnemy (){

        transformEnemy = new Transform[maxEnemyinRoom];
        enemySet = new Transform[maxEnemyLvl];

        for (int i = 0; i < enemySet.Length; i++ ) {

            if (enemySet[i] == null)
            {

                enemySet[i] = prefabsEnemy[Random.Range(0, prefabsEnemy.Length)];

            }

        }

    }


    void SpawEnemy() {

        for (int i = 0; i < transformEnemy.Length; i++ ) {

            if (transformEnemy[i] == null)
            {

                transformEnemy[i] = (Transform)Instantiate(enemySet[i], spawnPosition[i].transform.position, spawnPosition[i].transform.rotation);

                enemySet[i] = null;

            }

        }

    }

   

}
