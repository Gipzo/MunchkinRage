using UnityEngine;
using System.Collections;

public class EnemyState : MonoBehaviour
{


    public enType EnemyType;

    public enum enType { Orc, Skeleton, Goblin }

    public enTypeClass EnemyClass;

    public enum enTypeClass { Sworld, Mace, Dagger}

    public int power; //сила
    public int agility;//ловкость


    public float curHealth;//тексущее здоровье
    public float maxHealth;//максимальное здоровье

    public int armor; //броня

    public float damage; //урон

    public float crit; //множетель крита
    public float critChance; // шанс крита
    public int pCrit; //процент скрипта
    public int missP;// шанс уклона

    public float destroyBlockChance; //шанс разрушить блок

    public string nameEnemy; //имя моба

    public enTypePerk perck;// перк

    public enum enTypePerk { Krit, Block, DestroyBlock }


    void Start()
    {



        curHealth = maxHealth;


        Stats();

    }


    void Stats()
    {

        destroyBlockChance = power; //шанс пробить блок
        damage = power; //урон от силы
        armor = power / 5;//броня от силы
        missP = agility;//шанс мисса от ловкости
        missP -= 2 * armor;//тоже шанс мисса
        crit = 1 + 0.1f * agility; //множетель крит
        critChance = agility; //шанс крита


    }

  


    

}