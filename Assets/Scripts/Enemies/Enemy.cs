using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jump = new(jumpForce, rb);
        detectObjects = GetComponentInChildren<DetectObjects>();
        movement = new(speed, rb);
    }

    private void Update()
    {
        Debug.Log(jump.jumpRequest);
        Debug.Log(detectObjects.Detect().Count());
        if (detectObjects.Detect().Count() > 0)
        {
            jump.targets = detectObjects.Detect();
            jump.RequestJump();
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
    public void TakeDamage()
    {
        Debug.Log("Нанесен урон!");
    }
    

}
