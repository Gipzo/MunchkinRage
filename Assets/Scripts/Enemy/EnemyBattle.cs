using UnityEngine;
using System.Collections;

public class EnemyBattle : MonoBehaviour {

    public bool iHit;

    public bool hitToMe;

    public EnemyState enemyState;

    public CharcterBattle player;

    public bool blocked;

    void Hits() {
    
   

    }


    void HitSelect() {

        if (player.GetComponent<CharcterBattle>().iHit == true)
        {

            iHit = false;
            hitToMe = true;

        }
        else
        {

            iHit = true;
            hitToMe = false;

        }
    
    }

    void IHit() {

        if (!blocked)
        {
            player.GetComponent<CharcterState>().curHealth -= enemyState.damage;

            iHit = false;

            player.GetComponent<CharcterBattle>().iHit = true;

            hitToMe = true;
        }
        else {

            iHit = false;

            player.GetComponent<CharcterBattle>().iHit = true;

            hitToMe = true;
        
        }
    
    }

}
