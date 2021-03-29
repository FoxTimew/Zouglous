using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    Inventory inventory;


    private void Start()
    {
        inventory = GameObject.Find("InventoryBackground").GetComponent<Inventory>();
    }

    public void Selection()
    {
       //    Debug.Log(transform.parent.GetSiblingIndex());
        int Slotnb = transform.parent.GetSiblingIndex();    // indique le numero du slot
       if(inventory.Slot[Slotnb]>0)
        {
            inventory.Slot[Slotnb] -= 1;
            inventory.UpdateTxt(Slotnb, inventory.Slot[Slotnb].ToString());
        }
       
       if (inventory.Slot[Slotnb]<0)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
