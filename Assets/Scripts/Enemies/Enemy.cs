using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    //[SerializeField] private float range = 3f;
    [SerializeField] private LayerMask layer;
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 10f;
    private Rigidbody2D rb;
    private EnemyMovement movement;
    private EnemyJump jump;
    private DetectObjects detectObjects;
    private bool attackFlag = true;

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
        if (detectObjects.Detect().Count() > 0)
        {
            jump.targets = detectObjects.Detect();
            jump.RequestJump();
        }
        if(attackFlag) StartCoroutine(CollisionCheck());
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

    private IEnumerator CollisionCheck()
    {
        attackFlag = false;
        float checkRadius = 0.5f;
        Vector2 position = transform.position;
        LayerMask targetLayer = LayerMask.GetMask("Player", "House");

        yield return new WaitForSeconds(1);

        Collider2D[] hits = Physics2D.OverlapCircleAll(position, checkRadius, targetLayer);

        foreach (var hit in hits)
        {
            if (hit.gameObject.TryGetComponent<Entity>(out var entity))
            {
                Debug.Log($"Объект {hit.gameObject.name} на слое {LayerMask.LayerToName(hit.gameObject.layer)}");
                entity.TakeDamage();
            }
        }

        attackFlag = true;
    }

}
