using UnityEngine;
using UnityEngine.UIElements;

public class EnemyJump : Jump
{
    public Collider2D[] targets;
    [SerializeField] private float horizontalJumpMultiplier = 1.5f;
    public EnemyJump(float jumpForce, Rigidbody2D rb) : base(jumpForce, rb)
    {
    }

    public override void JumpLogic()
    {
        if (targets != null)
        {
            foreach (Collider2D objectCollider in targets)
            {
                if (jumpRequest && (isGrounded || _jumpBuffer > 0))
                {
                    //_rb.AddForce(new Vector2((objectCollider.GetComponent<Transform>().position.x - _rb.transform.position.x), _jumpForce));
                    _rb.linearVelocity = new((objectCollider.GetComponent<Transform>().position.x - _rb.transform.position.x) * horizontalJumpMultiplier, _jumpForce);
                    jumpRequest = false;
                    _jumpBuffer--;
                }
                if (isGrounded) _jumpBuffer = 1;
            }
        }
        
        
    }
}
