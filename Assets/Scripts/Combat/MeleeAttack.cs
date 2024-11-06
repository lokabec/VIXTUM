using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : Attack
{
    private void Awake()
    {
        attackType = AttackType.Melee;
    }

    public override void Execute()
    {
        Collider2D[] hitEnimies = Physics2D.OverlapCircleAll(attackPoint.transform.position, range, enemyLayer);

        foreach (Collider2D enemyCollider in hitEnimies)
        {
            enemyCollider.GetComponent<Enemy>().TakeDamage();
        }
    }
}
