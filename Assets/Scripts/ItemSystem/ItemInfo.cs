using UnityEngine;
using System.Collections;

public class ItemInfo : MonoBehaviour
{

    public string nameItem;

    public Sprite icon;


    public enType Type = enType.Arms;


    public enum enType { Arms,  Helmet, Body, Shoes, Shield}

    public int pBlock;
    public int block;

    public float minDamage;
    public float maxDamage;

    public int Armors;

    public int power;

    public int agility;

    public int Rarity;



}