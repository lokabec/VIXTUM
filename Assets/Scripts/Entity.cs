using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected int Health = 100;
    
    public void TakeDamage()
    {
        Debug.Log($"Name: {name}, HP: {Health}");
        Health--;
        if (Health <= 0) Die();  
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
