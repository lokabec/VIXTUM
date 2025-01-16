using UnityEngine;

public class EnemyMovement : Movement
{
    public EnemyMovement(float speed, Rigidbody2D rb) : base(speed, rb)
    {
    }
    public override void HandleInput(float moveInput) => base.HandleInput(moveInput);
    public override void Move(float moveInput) => base.Move(moveInput);
    
}
