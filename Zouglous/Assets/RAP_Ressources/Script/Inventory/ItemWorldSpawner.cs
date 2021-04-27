using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorldSpawner : MonoBehaviour
{    // associe le game object dans la sc�ne au bon item il faut parfois relancer le projet pour qu'un nouveau objet se cr�e
    // Start is called before the first frame update
    public Item item;

    private void Awake()
    {
        ItemWorld.SpawnItemWorld(transform.position, item);
        Destroy(gameObject);
    }
}
