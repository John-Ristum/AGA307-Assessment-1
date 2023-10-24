using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public TargetSize targetSize;
    public int health = 2;
    float scaleFactor;      //Needed for target size
    public float mySpeed = 1f;
    public float moveDistance = 1000f;

    private void Awake()
    {
        SetUp();
        StartCoroutine(Move());
    }

    void SetUp()
    {
        switch (targetSize)
        {
            case TargetSize.Small:
                scaleFactor = 0.5f;
                break;
            case TargetSize.Medium:
                scaleFactor = 1f;
                break;
            case TargetSize.Large:
                scaleFactor = 2f;
                break;
        }

        transform.localScale = transform.localScale * scaleFactor;
    }

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

    IEnumerator Move()
    {
        for (int i = 0; i < moveDistance; i++)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * mySpeed);
            yield return null;
        }
        transform.Rotate(Vector3.up * 180);
        yield return new WaitForSeconds(Random.Range(1, 3));
        StartCoroutine(Move());
    }
}
