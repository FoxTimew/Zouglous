using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    public float SpeedTrap;

  public  TrapDetection trapDetection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Update()
    {
        if (trapDetection.ishere == true)
        {
            transform.Translate(0, -SpeedTrap * Time.deltaTime, 0);
        }
    }
    // Update is called once per frame
    public void Gotrap(GameObject g)
    {
        transform.Translate(0, -SpeedTrap * Time.deltaTime, 0);
    }

}
