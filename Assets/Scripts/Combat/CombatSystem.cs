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
    [SerializeField] private ComboSystem _comboSystem;
    private int _score;
    [SerializeField] private Hero _hero;
    
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            KeyCode[] allowedKeys = { KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.LeftShift };
            foreach (KeyCode key in allowedKeys)
            {
                if (Input.GetKeyDown(key))
                {
                    switch (key)
                    {
                        case KeyCode.Mouse0:
                            ExecuteAttack(_meleeAttack);
                            break;
                        case KeyCode.Mouse1:
                            ExecuteAttack(_rangeAttack);
                            break;
                    }
                }
            }
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

