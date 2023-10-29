using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringPoint : MonoBehaviour
{
    [Header("Rigidbody Projectiles")]
    public GameObject[] projectilePrefabs;     //the projectile we wish to instantiate
    public float projectileSpeed = 1000f;   //The speed that our projectile fires at
    [Header("Raycast Projectiles")]
    public GameObject raycastProjectile;
    public GameObject hitSparks;
    public LineRenderer laser;
    private int currentProjectile = 0;


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            FireRigidbody();
        if (Input.GetButtonDown("Fire2"))
            FireRaycast();
        if (Input.GetKeyDown("q"))
            CycleWeapons();
    }

    void FireRigidbody()
    {
        //Create a reference to hold our instantiated object
        GameObject projectileInstance;
        //Instantiate the projectile prefab at the firing point's position and rotation
        projectileInstance = Instantiate(projectilePrefabs[currentProjectile], transform.position, transform.rotation);
        //Get the rigidbody component of the projectile and add force to "fire" it
        projectileInstance.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);
    }

    void FireRaycast()
    {
        //Instantiate the raycastProjectile prefab at the firing point's position and rotation
        GameObject projectileInstance = Instantiate(raycastProjectile, transform.position, transform.rotation);
        //Get the rigidbody component of the raycastProjectile and add force to "fire" it
        projectileInstance.GetComponent<Rigidbody>().AddForce(transform.forward * 10000f);

        //Create the ray
        Ray ray = new Ray(transform.position, transform.forward);
        //Create a reference to hold the info on what we hit
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) //Mathf.Infinity makes ray go forever. out hit will output the results to the hit variable
        {
            if (hit.collider.CompareTag("Target"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }

    void CycleWeapons()
    {
        currentProjectile++;
        if (currentProjectile > 2)
            currentProjectile = 0;
    }
}
