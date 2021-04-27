using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Ui_inventory : MonoBehaviour
{  // affiche les objet visuellement 
    private Inventory inventory;

    public Transform ItemSlotContainer, ItemSlotTemplate;
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void Awake()
    {
        ItemSlotContainer = transform.Find("ItemSlotContainer");
        ItemSlotTemplate = ItemSlotContainer.Find("ItemSlotTemplate");
    }

    private void RefreshInventoryItems()
    {
        foreach (Transform child in ItemSlotContainer)
        {
            if (child == ItemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int x = 1;      // il s'agit de la position par rapport au canvas    x= horizontal
        int y = 1;        // y= vertical
        float ItemSlotCellSize = 30f;   // c'est la distance entre les slots
        foreach (Item item in inventory.GetItemsList())
        {
            RectTransform itemSlotRectTransform = Instantiate(ItemSlotTemplate, ItemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.anchoredPosition = new Vector2(x * ItemSlotCellSize, y * ItemSlotCellSize);
         Image image =  itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            TextMeshProUGUI uiText = itemSlotRectTransform.Find("Text").GetComponent<TextMeshProUGUI>();
            if (item.amout >1)
            {
                uiText.SetText(item.amout.ToString());
            }
            else
            {
                uiText.SetText("");
            }
            x++;
            if (x > 4)
            {
                x = 0;
                y++;
            }
        }
    }
}
