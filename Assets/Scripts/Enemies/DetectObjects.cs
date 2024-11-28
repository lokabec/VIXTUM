using UnityEngine;

public class DetectObjects : MonoBehaviour
{
    [SerializeField] private float range = 4;
    [SerializeField] private LayerMask layer;

    public Collider2D[] Detect()
    {
        Collider2D[] findObjects = Physics2D.OverlapCircleAll(transform.position, range, layer);
        return findObjects;
        
    }
}
