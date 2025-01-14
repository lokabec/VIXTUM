using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entity : MonoBehaviour
{
    public int Health;
    
    public void TakeDamage()
    {
        //Debug.Log($"Name: {name}, HP: {Health}");
        Health--;
        if (Health <= 0) Die();  
    }

    protected void Die()
    {
        if(name == "Hero" || name == "House")
        {
            SceneManager.LoadScene("SampleScene");
            TestBackgroundMusic.StopFront();
        }
        Destroy(gameObject);
    }
}
