using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAttacks : MonoBehaviour
{
    public event EventHandler<OnAttackEventArgs> OnAttack;
    public class OnAttackEventArgs : EventArgs
    {
        public int damageAttack;
    }

    public event EventHandler OnLightAttack;
    public event EventHandler OnHeavyAttack;
    public event EventHandler OnSpecialAttack;


    //AttackNumbers
    [SerializeField]
    int lightAttack = 5;
    [SerializeField]
    int heavyAttack = 15;
    [SerializeField]
    int specialAttack = 15;

    [SerializeField]
    private KeyCode key;
    bool canAttack = false;
    public bool canHeavyAttack = false;
    [SerializeField]
    float whenAttackDoesDamage;

    private float timeDelay = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        canAttack = true;
        if (canHeavyAttack && BossController.isAttackable)
        {
            Debug.Log("small attack");
            OnAttack?.Invoke(this, new OnAttackEventArgs
            {
                damageAttack = specialAttack
            });
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canAttack = false;
    }

    private void Update()
    {
        AttackControls();

        timeDelay -= Time.deltaTime;
    }

    private void AttackControls()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnLightAttack?.Invoke(this, EventArgs.Empty);
            if (canAttack && BossController.isAttackable)
            {
                OnAttack?.Invoke(this, new OnAttackEventArgs
                {
                    damageAttack = lightAttack
                });
            } 
        }
        if (Input.GetMouseButtonDown(1))
        {
            
            if (timeDelay < 0)
            {
                OnHeavyAttack?.Invoke(this, EventArgs.Empty);
                StartCoroutine(CanDoDamage());

            }

        }
        if (Input.GetKeyDown(key))
        {
            OnSpecialAttack?.Invoke(this, EventArgs.Empty);
            if (canAttack && BossController.isAttackable)
            {
                Debug.Log("small attack");
                OnAttack?.Invoke(this, new OnAttackEventArgs
                {
                    damageAttack = specialAttack
                });
            }
        }
    }
    IEnumerator CanDoDamage()
    {
        timeDelay = 2.5f;
        yield return new WaitForSeconds(whenAttackDoesDamage);
        canHeavyAttack = true;
        yield return new WaitForSeconds(.7f);
        canHeavyAttack = false;
    }
}
