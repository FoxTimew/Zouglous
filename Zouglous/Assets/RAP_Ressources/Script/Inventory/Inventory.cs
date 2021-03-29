using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    //   public bool []IsFull;  //ancienne version

    //    public GameObject[] Slots; 

   GameObject inventoryBacground;

    public int[] Slot;

    public Text Number;


    private void Start()
    {
        inventoryBacground = transform.GetChild(0).gameObject;
        Slot = new int[inventoryBacground.transform.childCount];
    }

    void Update()
    {

    }

public void UpdateTxt (int Slotnb,string Number )
    {
        inventoryBacground.transform.GetChild(Slotnb).GetChild(1).GetComponent<Text>().text = Number;
    }

}
