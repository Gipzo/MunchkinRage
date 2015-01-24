using UnityEngine;
using System.Collections;

public class CharcterBattle : MonoBehaviour {

    

    public int armor;

    public float damage;

    public bool iHit;

    public bool hitToMe;

    public EnemyState systemB;

    public Inventory inventory;

    public CharcterState state;

    public EnemyBattle enemyB;

    void CalculateDamage() {

      // damage = state.GetComponent<CharcterState>().defaultDamage + inventory.damage;///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    }


    void CalculateDamageCrit()
    {

        CalculateDamage();

        damage = damage * state.GetComponent<CharcterState>().crit;

    }

    void BlockChance()
    {


        state.GetComponent<CharcterState>().pDesBlock = Random.Range(0, 100);
       

    }

    void    Block() {

        enemyB.GetComponent<EnemyBattle>().blocked = true;
    
    }


    void IHit() {

        string CurrentWeapon = inventory.GetCurrentWeapon();
        systemB.GetComponent<EnemyState>().curHealth -= inventory.damage ;


        if (state.GetComponent<CharcterState>().critChance > state.GetComponent<CharcterState>().pCrit)
        {

            CalculateDamageCrit();
            systemB.GetComponent<EnemyState>().curHealth -= damage;

        }
        else {

            CalculateDamage();
            systemB.GetComponent<EnemyState>().curHealth -= damage;
        
        
        }

    }

}
