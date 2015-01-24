using UnityEngine;
using System.Collections;

public class ItemInfo : MonoBehaviour
{

    public string nameItem;
   


    public enType Type = enType.Arms;


    public enum enType { Arms, Potions, Helmet, Body, Pants, Shoes, Shield}

	public enTypeSubstance substn;

	public enum enTypeSubstance {Rusty, Steel, Charmed6, Epic};

    public int pBlock;

    public float mindamage;

	public float maxdamage;

	private float damage;

    public int armors; //щит и вид брони

    public int rarity;

	public int power, agility;
	
	public float chanceblock;

	public float block; //блокируем урон

	public int percentCrit;

	public int addHelthFromArmor;

	public int chanceDrop;

}