using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : Attack
{
    private void Awake()
    {
        attackType = AttackType.Range;
        range = Mathf.Infinity;
    }
    public override void Execute()
    {
        Vector2 direction;
        Vector2 origin = attackPoint.transform.position;
        if (transform.localScale.x > 0) direction = Vector2.left;
        else direction = Vector2.right;
         
        RaycastHit2D hitInfo = Physics2D.Raycast(origin, direction, range, enemyLayer);

        if (hitInfo.collider != null)
        {
            //Debug.Log(hitInfo.collider.gameObject.name);
            hitInfo.collider.GetComponent<Enemy>().TakeDamage();
        }
    }
}
