using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stinger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
        {
            PlayerData playerData = other.GetComponent<PlayerData>();
            playerData.HealthDamaged(15);
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if(transform.position.y < -5)
        {
            Destroy(this.gameObject);
        }
    }
}
