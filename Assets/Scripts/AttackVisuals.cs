using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackVisuals : MonoBehaviour
{
    [SerializeField]
    PlayerAttacks playerAttacks;
    Animator animator;
    

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerAttacks.OnLightAttack += PlayerAttacks_OnLightAttack;
        playerAttacks.OnHeavyAttack += PlayerAttacks_OnHeavyAttack;
        playerAttacks.OnSpecialAttack += PlayerAttacks_OnSpecialAttack;
    }

    private void PlayerAttacks_OnSpecialAttack(object sender, System.EventArgs e)
    {
        animator.SetTrigger("isGriddy");
    }

    private void PlayerAttacks_OnHeavyAttack(object sender, System.EventArgs e)
    {
        animator.SetTrigger("isHeavyAttack");
        Debug.Log("Animbegin");
    }

    private void PlayerAttacks_OnLightAttack(object sender, System.EventArgs e)
    {
        animator.SetTrigger("isLightAttack");
    }
}
