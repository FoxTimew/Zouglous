using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("InventoryBackground").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Finish":
                inventory.Slot[0] += 1;
                inventory.UpdateTxt(0, inventory.Slot[0].ToString());
                Destroy(collision.gameObject);
                break;

            case "Respawn":
                inventory.Slot[1] += 1;
                inventory.UpdateTxt(1, inventory.Slot[1].ToString());
                Destroy(collision.gameObject);
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
