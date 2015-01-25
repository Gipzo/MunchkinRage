using UnityEngine;
using System.Collections;

public class CellState : MonoBehaviour {

    public ItemInfo obj;

    public TypeCell TypeCells;

    public enum TypeCell {just, helmet, body, shoes, shield, arms }

    public Inventory inv; //инвентарь игрока



   

    //установка айтема в снарежение
    public void SetItemToEqui()
    {

        if (obj.GetComponent<ItemInfo>().Type == ItemInfo.enType.Arms)
        {

            if(inv.arms == null){

                inv.arms = obj;
            
            }

        }

        if(obj.Type == ItemInfo.enType.Body){

            if (inv.body == null)
            {

                inv.body = obj;

            }
        
        
        }


        if (obj.Type == ItemInfo.enType.Helmet)
        {

            if (inv.helmet == null)
            {

                inv.helmet = obj;

            }


        }


        if (obj.Type == ItemInfo.enType.Shield)
        {

            if (inv.shield== null)
            {

                inv.shield = obj;

            }


        }


        if (obj.Type == ItemInfo.enType.Shoes)
        {

            if (inv.shoes == null)
            {

                inv.shoes = obj;

            }


        }

    }

}
