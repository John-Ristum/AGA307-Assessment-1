using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPad : MonoBehaviour
{
    public GameObject sphere;   //The object we wish to change

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //change the spheres colour to green
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Increas the spheres scale by 0.01 on all axis
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //set the spheres size back to 1
            //Change the spheres colour to yellow
        }
    }
}

