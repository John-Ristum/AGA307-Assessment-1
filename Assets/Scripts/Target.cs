using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : GameBehaviour
{
    public static event Action<GameObject> OnTargetHit = null;
    public static event Action<GameObject> OnTargetDestroy = null;

    public TargetSize targetSize;
    public int health = 2;
    float scaleFactor;      //Needed for target size
    public float baseSpeed = 5f;
    float mySpeed;
    public float moveDistance = 1000f;
    public Transform moveToPos;
    Vector3 originalScale;


    private void Awake()
    {
        originalScale = transform.localScale;
        SetUp();
        StartCoroutine(MoveRandom3());
    }

    public void SetUp()
    {
        switch (targetSize)
        {
            case TargetSize.Small:
                scaleFactor = 0.5f;
                mySpeed = baseSpeed * 2f;
                this.gameObject.GetComponent<Renderer>().material.color = Color.red;
                break;
            case TargetSize.Medium:
                scaleFactor = 1f;
                mySpeed = baseSpeed;
                this.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
                break;
            case TargetSize.Large:
                scaleFactor = 2f;
                mySpeed = baseSpeed / 2;
                this.gameObject.GetComponent<Renderer>().material.color = Color.green;
                break;
        }

        transform.localScale = originalScale * scaleFactor;
    }

    public int CauseDamage(int damage)
    {
        //Invokes OnTargetHit event
        OnTargetHit?.Invoke(this.gameObject);
        //Subtracts damage recieved from helth
        health -= damage;
        //Destroys the target if health is 0
        if (health <= 0)
            Destroy();
        return health;
    }

    void Destroy()
    {
        StopAllCoroutines();
        OnTargetDestroy?.Invoke(this.gameObject);
    }

    /// <summary>
    /// Moves target to a random position
    /// </summary>
    /// <returns></returns>
    IEnumerator Move()
    {
        moveToPos = _TM.GetRandomSpawnPoint();
        transform.LookAt(moveToPos);
        while(Vector3.Distance(transform.position, moveToPos.position) > 0.3f)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveToPos.position, Time.deltaTime * mySpeed);
            yield return null;
        }
    }

    /// <summary>
    /// Moves target to random new position every 3 seconds
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveRandom3()
    {
        StartCoroutine(Move());

        yield return new WaitForSeconds(3f);

        StopAllCoroutines();

        StartCoroutine(MoveRandom3());
    }
}
