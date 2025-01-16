using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [SerializeField] protected int points;
    [SerializeField] protected float damage;
    public float cooldown;
    [SerializeField] protected LayerMask enemyLayer;
    public GameObject attackPoint;
    public float range;
    [SerializeField] protected ActionType actionType;

    public bool IsOnCooldown { get; private set; }

    [System.Obsolete]
    public virtual void Execute()
    {
        FindObjectOfType<ComboSystem>().RegisterAttack(actionType);
    }

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
