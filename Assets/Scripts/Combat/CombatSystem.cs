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
    [SerializeField] private MeleeAttack meleeAttack;
    [SerializeField] private RangeAttack rangeAttack;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!meleeAttack.IsOnCooldown)
            {
                meleeAttack.Execute();
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (!rangeAttack.IsOnCooldown)
            {
                rangeAttack.Execute();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(meleeAttack.attackPoint.transform.position, meleeAttack.range);
    }
}

