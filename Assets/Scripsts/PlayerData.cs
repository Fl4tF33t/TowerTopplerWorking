using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerData : MonoBehaviour
{
    public event EventHandler<OnHealthChangeEventArgs> OnHealthChange;
    public class OnHealthChangeEventArgs : EventArgs
    {
        public int health;
    }
    [SerializeField]
    int playerHealth;
    

    private void Start()
    {
    }


    public int CurrentHealth()
    {
        return playerHealth;
    }

    public void HealthDamaged(int damage)
    {
        playerHealth -= damage;

        OnHealthChange?.Invoke(this, new OnHealthChangeEventArgs
        {
            health = CurrentHealth()
        }); ;
    }
}
