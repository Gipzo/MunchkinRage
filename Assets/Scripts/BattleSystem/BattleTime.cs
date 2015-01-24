using UnityEngine;
using System.Collections;

public class BattleTime : MonoBehaviour {

	public EnemyBattle enemy;

    public CharcterBattle player;

    public bool isBattleStart;


    void StartBattle() {

        int firstHit;

        firstHit = Random.Range(1, 2);

        switch (firstHit) {
        
            case 1 :
                enemy.GetComponent<EnemyBattle>().iHit = true;
                player.GetComponent<CharcterBattle>().hitToMe = false;
                break;
            case 2:
                enemy.GetComponent<EnemyBattle>().iHit = false;
                player.GetComponent<CharcterBattle>().hitToMe = true;
                break;
        
        }

    }
	
}
