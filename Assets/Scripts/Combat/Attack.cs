using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [SerializeField] protected int points;
    [SerializeField] protected float damage;
    [SerializeField] protected float cooldown;
    [SerializeField] protected LayerMask enemyLayer;
    public GameObject attackPoint;
    public float range;
    [SerializeField] protected AttackType attackType;

    public bool IsOnCooldown { get; private set; }

    public abstract void Execute();

    protected virtual void Cancel()
    {
        
    }

    protected IEnumerator CooldownRoutine()
    {
        IsOnCooldown = true;
        yield return new WaitForSeconds(cooldown);
        IsOnCooldown = false;
    }
}
