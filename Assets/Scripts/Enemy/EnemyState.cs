using UnityEngine;
using System.Collections;

public class EnemyState : MonoBehaviour
{


    public enType EnemyType;

    public enum enType { Orc, Leprechaun, Goblin }

    public enTypeClass EnemyClass;

    public enum enTypeClass { Archer, Swordsman, Tank, Magician }

    public float curHealth;
    private float maxHealth;

    public int armor;

    public string nameEnemy;


    void Start()
    {

        SelectName();

        SelectHealthOrc();

        SelectHealthLeprechaun();

        SelectHealthGoblin();

        curHealth = maxHealth;

    }

  


    //селект хп фурий
    void SelectHealthGoblin()
    {


        if (nameEnemy.Equals("Goblin mage"))
        {

            maxHealth = 50;
            print(maxHealth);

        }
        else if (nameEnemy == "Goblin swordsman")
        {

            maxHealth = 70;
            print(maxHealth);

        }
        else if (nameEnemy == "Goblin berserker")
        {

            maxHealth = 100;
            print(maxHealth);

        }
        else if (nameEnemy.Equals("Goblin archer"))
        {

            maxHealth = 60;
            print(maxHealth);

        }


    }

   

    //селект хп лепреконов
    void SelectHealthLeprechaun()
    {

        if (nameEnemy == "Leprechaun archer")
        {

        maxHealth = 60;
        print(maxHealth);

        }



        if (nameEnemy == "Leprechaun mage")
        {

            maxHealth = 40;
            print(maxHealth);

        }


        if (nameEnemy == "Leprechaun swordsman")
        {

            maxHealth = 80;
            print(maxHealth);

        }

        if (nameEnemy == "Leprechaun berserker")
        {

            maxHealth = 100;
            print(maxHealth);

        } 

    }

    //селект хп орков
    void SelectHealthOrc()
    {

        if (nameEnemy == "Orc archer")
        {

            maxHealth = 90;
            print(maxHealth);

        }



        if (nameEnemy == "Orc mage")
        {

            maxHealth = 70;
            print(maxHealth);

        }


        if (nameEnemy == "Orc swordsman")
        {

            maxHealth = 120;
            print(maxHealth);

        }

        if (nameEnemy == "Orc berserker")
        {

            maxHealth = 140;
            print(maxHealth);

        }

    } 


    //установка имени
    void SelectName()
    {

        

            if (EnemyClass == enTypeClass.Archer)
            {

                nameEnemy = EnemyType.ToString() +" archer";

            }


            if (EnemyClass == enTypeClass.Magician)
            {

                nameEnemy = EnemyType.ToString() + " mage";

            }

            if (EnemyClass == enTypeClass.Swordsman)
            {

                nameEnemy = EnemyType.ToString() + " swordsman";

            }


            if (EnemyClass == enTypeClass.Tank)
            {

                nameEnemy = EnemyType.ToString() + " berserker";

            }

        


    }

}