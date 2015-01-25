using UnityEngine;
using System.Collections;

public class CharcterState : MonoBehaviour {


    private float maxHealth;
    public float curHealth;

    public int power;
    public int agility;

    public float defaultDamage;

    public int defaultArmor;

    public enTypePerk perck;

    public float crit;
    public float critChance;
    public int pCrit;
    public int missP;

    public float destroyBlockChance;
    public float pDesBlock;

    public float blockChance;
    public float pBlock;

    public enum enTypePerk { Krit, Block, DestroyBlock}
    public Inventory inventory;


    void Start()
    {

        Stats();

    }

    void Stats() {

        destroyBlockChance = power; //шанс пробить блок
        defaultDamage = power; //урон от силы
        defaultArmor = power / 5;//броня от силы
        missP = agility;//шанс мисса от ловкости
        missP -= 2 * defaultArmor;//тоже шанс мисса
        crit = 1 + 0.1f * agility; //множетель крит
        critChance = agility; //шанс крита
        //blockChance = inventory.GetPBlock();

    }

   
    

    void CritChance()
    {

        pCrit = Random.Range(0, 100);//если шанс крита больше чем переменная pCrit то выпадает крит 

        if (critChance > pCrit)
        {

            defaultDamage = defaultDamage * crit;
        
        }


    }


    

}
