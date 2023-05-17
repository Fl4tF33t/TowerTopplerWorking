using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossVisual : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    BossController bossController;

    private void Start()
    {
        animator = GetComponent<Animator>();
        bossController.OnSmashAttack += BossController_OnSmashAttack;
    }

    private void BossController_OnSmashAttack(object sender, System.EventArgs e)
    {
        animator.SetTrigger("isSmashing");
    }
}
