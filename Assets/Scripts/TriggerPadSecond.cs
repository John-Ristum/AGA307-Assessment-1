using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPadSecond : GameBehaviour
{
    public GameObject sphere;   //The object we wish to change
    public Color sphereDefaultColour;   //The original colour of the sphere
    public Vector3 sphereDefaultSize;   //The original size of the sphere
    public GameObject playerCamera;     //reference to player's camera for raycast
    private Color[] colourRotation = { Color.red, Color.blue, Color.yellow };   //Colours that the sphere rotates through when "E" key is pressed
    private int currentRotation;    //Current colour in rotation
    private bool sphereIsHit;

    void Start()
    {
        //Gets original colour of the sphere
        sphereDefaultColour = sphere.GetComponent<Renderer>().material.color;
        //Gets original size of the sphere
        sphereDefaultSize = sphere.transform.localScale;

        currentRotation = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown("e") && sphereIsHit)
            ChangeSphereColour();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Trigger a raycast function in the FiringPoint script
            RaycastToSphere();
            //Increas the spheres scale by 0.01 on all axis if hit by raycast
            if (sphereIsHit)
            {
                sphere.transform.localScale += Vector3.one * 0.01f;
            }
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

    void RaycastToSphere()
    {
        //Create the ray
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        //Create a reference to hold the info on what we hit
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) //Mathf.Infinity makes ray go forever. out hit will output the results to the hit variable
        {
            if (hit.collider.CompareTag("TriggerZone2Object"))
            {
                sphereIsHit = true;
            }
            else
            {
                sphereIsHit = false;
            }
        }
    }

    void ChangeSphereColour()
    {
        //Changes colour of sphere
        sphere.GetComponent<Renderer>().material.color = colourRotation[currentRotation];
        //Sets next colour in rotation
        currentRotation++;
        //Resets rotation back to it's original colour once the last colour has been used
        if (currentRotation > 2)
            currentRotation = 0;
    }
}
