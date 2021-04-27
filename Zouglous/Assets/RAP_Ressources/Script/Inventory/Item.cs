using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ce script permet de donner des attribution d'item au objet
[Serializable]
public class Item
{
    public enum ItemType
    {
        Fruit,
        Annana,
        Tomate,
    }
    public ItemType itemType;
    public int amout;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Annana:     return ItemAssets.Instance.annanaSprite;
            case ItemType.Fruit:      return ItemAssets.Instance.fruitSprite;
            case ItemType.Tomate:      return ItemAssets.Instance.TomateSprite;
        }
    } 
    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Fruit:
            case ItemType.Annana:
            case ItemType.Tomate:
                return true;
        }
    }
}
