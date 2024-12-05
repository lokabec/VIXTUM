using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static Unity.Cinemachine.IInputAxisOwner.AxisDescriptor;

public class Enemy : Entity
{
    [SerializeField] protected float detectRange = 3f;
    [SerializeField] protected LayerMask layer;
    [SerializeField] protected Transform target;
    [SerializeField] protected float speed = 10f;
    protected Rigidbody2D rb;
    protected EnemyMovement movement;
    protected DetectObjects detectObjects;
    int targetsCountInTriggerZone;
    private Coroutine check;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        detectObjects = GetComponentInChildren<DetectObjects>();
        movement = new(speed, rb);
    }

    protected virtual void FixedUpdate()
    {
        float moveInput = target.position.x - transform.position.x;
        movement.Move(moveInput);
    }

    protected virtual void Update()
    {
        if (detectObjects.DetectForAttack(transform.localScale.x, layer).Count() != targetsCountInTriggerZone)
        {
            
            UpdateCountOfTargets();
            if(check != null)
            {
                StopCoroutine(check);
            }
            check = StartCoroutine("CollisionCheck");

        }
    }

    private void UpdateCountOfTargets()
    {
        targetsCountInTriggerZone = detectObjects.DetectForAttack(transform.localScale.x, layer).Count();
    }

    protected IEnumerator CollisionCheck()
    {
        while (targetsCountInTriggerZone != 0)
        {
            foreach (Collider2D foudedTarget in detectObjects.DetectForAttack(transform.localScale.x, layer))
            {
                
                if (foudedTarget.gameObject.layer == 6 || foudedTarget.gameObject.layer == 9)
                {
                    foudedTarget.GetComponent<Entity>().TakeDamage();
                }
            }

            yield return new WaitForSeconds(1);
        }
    }

}
