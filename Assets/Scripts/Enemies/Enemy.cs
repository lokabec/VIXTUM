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
    [SerializeField] private float detectRange = 3f;
    [SerializeField] private LayerMask layer;
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 10f;
    private Rigidbody2D rb;
    private EnemyMovement movement;
    private EnemyJump jump;
    private DetectObjects detectObjects;
    private bool attackFlag = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jump = new(jumpForce, rb);
        detectObjects = GetComponentInChildren<DetectObjects>();
        movement = new(speed, rb);
        Health = 3;
    }

    private void Update()
    {
        if (detectObjects.Detect(detectRange, layer).Count() > 0)
        {
            jump.targets = detectObjects.Detect(detectRange, layer);
            jump.RequestJump();
        }
        if(!attackFlag && detectObjects.DetectForAttack(1f, layer).Count() > 0)
        {
            attackFlag = true;
            StartCoroutine(CollisionCheck());
            
        }
        
    }

    private void FixedUpdate()
    {
        jump.GroundCheck(GetComponent<BoxCollider2D>(), LayerMask.GetMask("Ground"));
        if (jump.jumpRequest)
        {
            jump.JumpLogic();
        }
        else
        {
            float moveInput = target.position.x - transform.position.x;
            movement.Move(moveInput);
        }
        
        
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.TryGetComponent<Entity>(out var entity))
    //    {
    //        Debug.Log(collision.gameObject.name);
    //        entity.TakeDamage();
    //    }
    //}

    //private void CollisionCheck()
    //{
    //    attackFlag = false;
    //    float checkRadius = 0.5f;
    //    Vector2 position = transform.position;
    //    LayerMask targetLayer = LayerMask.GetMask("Player", "House");

    //    //yield return new WaitForSeconds(1);

    //    Collider2D[] hits = Physics2D.OverlapCircleAll(position, checkRadius, targetLayer);

    //    foreach (var hit in hits)
    //    {
    //        if (hit.gameObject.TryGetComponent<Entity>(out var entity))
    //        {
    //            Debug.Log($"Объект {hit.gameObject.name} на слое {LayerMask.LayerToName(hit.gameObject.layer)}");
    //            entity.TakeDamage();
    //        }
    //    }

    //    attackFlag = true;
    //}
    private IEnumerator CollisionCheck()
    {
        while (detectObjects.DetectForAttack(1f, layer).Count() != 0)
        {
            foreach (Collider2D foudedTarget in detectObjects.DetectForAttack(1f, layer))
            {
                //Debug.Log($"Слой найденный: {foudedTarget.gameObject.layer}, Нужные слои {layer}");
                if (foudedTarget.gameObject.layer == 6 || foudedTarget.gameObject.layer == 9)
                {
                    foudedTarget.GetComponent<Entity>().TakeDamage();
                }
            }

            yield return new WaitForSeconds(1);
        }

        attackFlag = false;
        


    }

}
