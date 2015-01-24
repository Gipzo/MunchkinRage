using UnityEngine;
using System.Collections;

public class ItemInfo : MonoBehaviour
{

    public string nameItem;



    public enType Type = enType.Sword;


    public enum enType { Sword, Potions, Helmet, Body, Pants, Shoes, Shield}

	public enTypeSubstance substn;

	public enum enTypeSubstance {Rusty, Steel, Charmed6, Epic};

    public int pBlock; //%

    public float minDamage;//минимальный урон

    public float maxDamage;//максимальный урон

    public int armors; //щит и вид брони

    public int rarity; //редкость дропа

	public int power, agility;//+ ловкости и силы
	
	public float chanceBlock;//шан блока от щита

	public float block; //блокируем урон

	public int percentCrit; //

	public int addHelthFromArmor;//+ хп от брони

}