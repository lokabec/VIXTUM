using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : Attack
{
    private void Awake()
    {
        actionType = ActionType.MeleeAttack;
    }

    public override void Execute()
    {
        base.Execute();
        Collider2D[] hitEnimies = Physics2D.OverlapCircleAll(attackPoint.transform.position, range, enemyLayer);
        if(hitEnimies != null)
        {
            foreach (Collider2D enemyCollider in hitEnimies)
            {
                enemyCollider.GetComponent<Enemy>().TakeDamage(actionType);
            }
        }
        
    }
}
