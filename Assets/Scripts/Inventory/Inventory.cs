using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Inventory : MonoBehaviour {

    public ItemInfo helmet;//это ячейчи снаряжения
    public ItemInfo body;
    public ItemInfo shoes;
    public ItemInfo arms;
    public ItemInfo shield;

    public Sprite defaultI;

    public Image iconCellHelmet;//иконки ячейки
    public Image iconCellBody;
    public Image iconCellShoes;
    public Image iconCellArms;
    public Image iconCellShield;


    public CharcterState playerStats;


  //  public int capacity;//сколько ячеек оталось свободно


    void Update()
    {
        
        DebugIcon();
        
    }


    void SetStats()
    {

        if(shield != null){

            playerStats.defaultArmor += shield.Armors;
            playerStats.blockChance += shield.pBlock;
            //playerStats.++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        
        }

        if(shoes != null){

            playerStats.agility += shoes.agility;
        
        }

        if(arms != null){

            playerStats.defaultDamage = Random.Range(arms.minDamage, arms.maxDamage);

            playerStats.power += arms.power;
            playerStats.agility += arms.agility;

        }

        if (helmet != null) {

            playerStats.power += helmet.power;
        
        }

        if(body != null){

            playerStats.power += body.power;
            playerStats.agility += body.agility;

            playerStats.defaultArmor += body.Armors;

        }



    }

    //вывод иконки
    void DebugIcon() {
        //щит

        if(shield != null){

            iconCellShield.sprite = shoes.icon;

        }
        else
        {

            iconCellShield.sprite = defaultI;

        }



        //обувь
        if (shoes != null)
        {

            iconCellShoes.sprite = shoes.icon;

        }
        else {

            iconCellShoes.sprite = defaultI;
        
        }


        //нагрудник
        if (body != null)
        {

            iconCellBody.sprite = body.icon;

        }
        else {

            iconCellBody.sprite = defaultI;
        
        }

        //шлем
        if (helmet != null)
        {

            iconCellHelmet.sprite = helmet.icon;

        }
        else {

            iconCellHelmet.sprite = defaultI;

        }


        //оружие
        if (arms != null)
        {

            iconCellArms.sprite = arms.GetComponent<ItemInfo>().icon;

        }
        else {

            iconCellArms.sprite = defaultI;
        
        }


        iconCellHelmet.sprite = helmet.icon;
        iconCellBody.sprite = body.icon;
        iconCellShoes.sprite = shoes.icon;
       
        iconCellShield.sprite = shield.icon;
    
    }


}
