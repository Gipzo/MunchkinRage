﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Inventory : MonoBehaviour {

    public GameObject[] cells = new GameObject[20];

    void Start()
    {
        SelecteMassive();
    }
	void OpenInv(){

	}

    void SelecteMassive() { 
    
        for(int i = 0; i < cells.Length; i++ ){
            cells[i] = gameObject.transform.GetChild(i).gameObject;

            Inv.cells = cells;
    }

    }

	//Вывод информации на ячейки
	void DebugInv(){

		Inv.cells = new GameObject[20];

		Inv.item = new GameObject[20];

		for (int i=0; i<Inv.cells.Length; i++) {

			Inv.cells[i].transform.FindChild("Name").GetComponent<Text>().text=Inv.item[i].GetComponent<ItemInfo>().nameItem;

            if (Inv.item[i].GetComponent<ItemInfo>().Type == ItemInfo.enType.Sword)
            {
				
               // Inv.cells[i].GetComponent("Param1").GetComponent<Text>().text="Dam: "+(Inv.item[i].GetComponent<ItemInfo>().Damage*0.9f).ToString()+" - "+(Inv.item[i].GetComponent<ItemInfo>().Damage*1.1f).ToString();
			}
		}
	}
}


public static class Inv{
	public static GameObject[] item;
	public static GameObject[] cells;
	public static string itemname;
	public static string Parameter1Name;
}
