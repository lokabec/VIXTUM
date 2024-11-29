using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entity : MonoBehaviour
{
    public int Health = 100;
    
    public void TakeDamage()
    {
        Debug.Log($"Name: {name}, HP: {Health}");
        Health--;
        if (Health <= 0) Die();  
    }

    private void Die()
    {
        if(name == "Hero" || name == "House")
        {
            SceneManager.LoadScene("SampleScene");
        }
        Destroy(gameObject);
    }
}
