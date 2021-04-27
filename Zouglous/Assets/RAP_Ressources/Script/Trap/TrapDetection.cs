using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDetection : MonoBehaviour
{
 

   public bool ishere = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collisiontrap)
    {
        if(collisiontrap.CompareTag("Player"))
        {
            //    print("fuck u");
            ishere = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
