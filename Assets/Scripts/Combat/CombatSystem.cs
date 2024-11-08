using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;


public class CombatSystem : MonoBehaviour
{
    [SerializeField] private MeleeAttack _meleeAttack;
    [SerializeField] private RangeAttack _rangeAttack;
    [SerializeField] private int _score;
    [SerializeField] private Hero _hero;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ExecuteAttack(_meleeAttack);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ExecuteAttack(_rangeAttack);
        }
    }

    private void ExecuteAttack(Attack attack)
    {
        if (!attack.IsOnCooldown)
        {
            attack.Execute();
            //if (!_hero._isGrounded)
            //{

            //    StartCoroutine(_hero.LockupInAirWhileAttack(attack));
            //}
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_meleeAttack.attackPoint.transform.position, _meleeAttack.range);
    }
}

