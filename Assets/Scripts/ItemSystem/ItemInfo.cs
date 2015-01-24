using UnityEngine;
using System.Collections;

public class ItemInfo : MonoBehaviour
{

    public string nameItem;
   


    public enType Type = enType.Arms;


    public enum enType { Arms, Potions, Helmet, Body, Pants, Shoes}

    public int Damage;

    public int Armors;

    public int Rarity;



}
