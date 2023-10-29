using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeSpan = 5f;     //Seconds until projectile is destroyed
    public int damage = 1;       //Damage projectile causes to targets
    public bool raycastProjectile;

    void Start()
    {
        Destroy(this.gameObject, lifeSpan);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target") && !raycastProjectile)
        {
            //Change the colour of the target
            //collision.gameObject.GetComponent<Renderer>().material.color = Color.red;
            //Cause damage to the target
            collision.gameObject.GetComponent<Target>().CauseDamage(damage);
            //Destroy this object
            Destroy(this.gameObject);
        }
    }
}
