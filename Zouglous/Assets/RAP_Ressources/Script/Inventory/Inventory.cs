using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{
    private List<Item> ItemList;
    public event EventHandler OnItemListChanged;
    public Inventory()
    {
        ItemList = new List<Item>();
   /*     AddItem(new Item { itemType = Item.ItemType.Fruit, amout = 1 });
        AddItem(new Item { itemType = Item.ItemType.Annana, amout = 1 });
        AddItem(new Item { itemType = Item.ItemType.Tomate, amout = 1 });
     */   Debug.Log(ItemList.Count);
    }

    public void AddItem(Item item)
    {
        if (item.IsStackable()) 
        {
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in ItemList)
            {
                if(inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amout += item.amout;
                    itemAlreadyInInventory = true;
                }
            }
            if(!itemAlreadyInInventory)
            {
                ItemList.Add(item);
            }
        }
        else
        {
          ItemList.Add(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemsList()
    {
        return ItemList;
    }
}
