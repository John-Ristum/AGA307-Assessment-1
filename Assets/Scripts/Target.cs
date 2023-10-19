using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int health = 2;


    public int CauseDamage(int damage)
    {
        //Subtracts damage recieved from helth
        health -= damage;
        //Destroys the target if health is 0
        if (health <= 0)
            DestroyTarget();
        return health;
    }

    void DestroyTarget()
    {
        Destroy(this.gameObject);
    }
}
