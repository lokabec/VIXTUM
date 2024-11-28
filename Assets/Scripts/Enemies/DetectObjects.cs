using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DetectObjects : MonoBehaviour
{
    public Collider2D[] Detect(float range, LayerMask layer)
    {
        Collider2D[] findObjects = Physics2D.OverlapCircleAll(transform.position, range, layer);
        return findObjects;
        
    }
    public Collider2D[] DetectForAttack(float range, LayerMask layer)
    {
        Collider2D[] currentObjects = Physics2D.OverlapBoxAll(transform.position, new(range, range), 0, layer.value);
        return currentObjects;

    }
}
