using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringPoint : MonoBehaviour
{
    [Header("Rigidbody Projectiles")]
    public GameObject projectilePrefab;     //the projectile we wish to instantiate
    public float projectileSpeed = 1000f;   //The speed that our projectile fires at
    [Header("Raycast Projectiles")]
    public GameObject hitSparks;
    public LineRenderer laser;


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            FireRigidbody();
        if (Input.GetButtonDown("Fire2"))
            FireRaycast();
    }

    void FireRigidbody()
    {
        //Create a reference to hold our instantiated object
        GameObject projectileInstance;
        //Instantiate our projectile prefab at the firing point's position and rotation
        projectileInstance = Instantiate(projectilePrefab, transform.position, transform.rotation);
        //Get the rigidbody component of the projectile and add force to "fire" it
        projectileInstance.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);
    }

    void FireRaycast()
    {
        //Create the ray
        Ray ray = new Ray(transform.position, transform.forward);
        //Create a reference to hold the info on what we hit
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) //Mathf.Infinity makes ray go forever. out hit will output the results to the hit variable
        {
            laser.SetPosition(0, transform.position);
            laser.SetPosition(1, hit.point);
            StopAllCoroutines();
            StartCoroutine(StopLaser());

            GameObject particles = Instantiate(hitSparks, hit.point, hit.transform.rotation);

            Destroy(particles, 1);

            if (hit.collider.CompareTag("Target"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }

    IEnumerator StopLaser()
    {
        laser.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        laser.gameObject.SetActive(false);
    }
}
