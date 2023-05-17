using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Testing : MonoBehaviour
{
    Follower follower;

    private void Start()
    {
        follower = GetComponent<Follower>();
        InvokeRepeating("StingerAttack", 2, 5);
    }

    void StingerAttack()
    {
        StartCoroutine(AttackStart(2f));
        Debug.Log("Hit");
    }
    
    IEnumerator AttackStart(float timeDelay)
    {
        follower.isMoving = !follower.isMoving;
        yield return new WaitForSeconds(timeDelay);
        print("waiting");
        follower.isMoving = !follower.isMoving;
    }

}