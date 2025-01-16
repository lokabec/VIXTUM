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

    private Coroutine _coroutine;// ”ƒ¿À»“‹  Œ√ƒ¿ ¡”ƒ”“ ÕŒ–Ã¿À‹Õ€≈ ¿Õ»Ã¿÷»»

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
                            // ”ƒ¿À»“‹  Œ√ƒ¿ ¡”ƒ”“ ÕŒ–Ã¿À‹Õ€≈ ¿Õ»Ã¿÷»»
                            if (_coroutine != null)
                            {
                                StopCoroutine(_coroutine);
                            }
                           _coroutine = StartCoroutine(nameof(ShowSword));
                            // ”ƒ¿À»“‹  Œ√ƒ¿ ¡”ƒ”“ ÕŒ–Ã¿À‹Õ€≈ ¿Õ»Ã¿÷»»

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

    /// <summary>
    /// ”ƒ¿À»“‹  Œ√ƒ¿ ¡”ƒ”“ ÕŒ–Ã¿À‹Õ€≈ ¿Õ»Ã¿÷»»
    /// </summary>
    /// <returns></returns>
   private IEnumerator ShowSword()
    {
        _hero.GetComponentInChildren<Transform>().Find("Sprite").GetComponentInChildren<Transform>().Find("anim_1").gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        _hero.GetComponentInChildren<Transform>().Find("Sprite").GetComponentInChildren<Transform>().Find("anim_1").gameObject.SetActive(false);
    }
}

