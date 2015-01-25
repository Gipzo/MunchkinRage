using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RangeItem : MonoBehaviour
{

    public Transform[] cells = new Transform[20];//ячейки 

    public ItemInfo[] curItem = new ItemInfo[20];// обънекты в ячейках

    public Image[] iconeCell = new Image[20]; //иконки 

    

    void Update() { 

       SelectCells();

        SelectIcon();

       DebugIcon();
       SetCellState();

    }


    //Присваевам ячейки
    void SelectCells (){

        for (int b = 0; b < cells.Length; b++ ) {

            cells[b] = gameObject.transform.GetChild(b);
        
        }
    
    }


    //выбор иконок
    void SelectIcon()
    {

        for (int b = 0; b < iconeCell.Length; b++)
        {

            iconeCell[b] = cells[b].GetChild(0).gameObject.GetComponent<Image>();

        }

    }

    


    //присваевание иконки
    void DebugIcon()
    {



        for (int b = 0; b < iconeCell.Length; b++)
        {
            if (curItem[b] != null)
            {
                iconeCell[b].sprite = curItem[b].GetComponent<ItemInfo>().icon;
            }
            else {

                print("Sf");

            }
        }

    }

    //установка cellstate 
    void SetCellState()
    {

        for(int b = 0; b < cells.Length; b++){
            cells[b].GetComponent<CellState>().obj = curItem[b];
        }


    }

    
}