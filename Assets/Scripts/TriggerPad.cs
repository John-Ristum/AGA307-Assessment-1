using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPad : MonoBehaviour
{
    public GameObject sphere;   //The object we wish to change
    public Color sphereDefaultColour;   //The original colour of the sphere
    public Vector3 sphereDefaultSize;   //The original size of the sphere

    private void Start()
    {
        //Gets original colour of the sphere
        sphereDefaultColour = sphere.GetComponent<Renderer>().material.color;
        //Gets original size of the sphere
        sphereDefaultSize = sphere.transform.localScale;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //change the spheres colour to green
            sphere.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Increas the spheres scale by 0.01 on all axis
            sphere.transform.localScale += Vector3.one * 0.01f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //set the spheres size back to 1
            sphere.transform.localScale = sphereDefaultSize;
            //Change the spheres colour back to it's original colour
            sphere.GetComponent<Renderer>().material.color = sphereDefaultColour;
        }
    }
}

