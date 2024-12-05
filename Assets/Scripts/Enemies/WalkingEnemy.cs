using System.Linq;
using UnityEngine;

public class WalkingEnemy : Enemy
{

    private EnemyJump jump;
    [SerializeField] private float jumpForce = 10f;

    protected override void Start()
    {
        base.Start();
        jump = new(jumpForce, rb);
    }

    protected override void Update()
    {
        if (detectObjects.Detect(detectRange, layer).Count() > 0)
        {
            jump.targets = detectObjects.Detect(detectRange, layer);
            jump.RequestJump();
        }
        base.Update();

    }

    protected override void FixedUpdate()
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
}
