using System.Collections;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    private Transform spawnPoint;
    private Coroutine spawn;
    [SerializeField] private GameObject Enemy;
    [SerializeField] private int spawnTime = 2;
    void Start()
    {
        spawnPoint = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            spawn = StartCoroutine("SpawnTimer");
        }
        if (spawn != null && Input.GetKeyDown(KeyCode.T))
        {
            StopCoroutine(spawn);
        }
    }
    
    private IEnumerator SpawnTimer()
    {
        while (true)
        {
            Debug.Log("Спавним врага");
            yield return new WaitForSeconds(spawnTime);
            Instantiate(Enemy, new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0f), Quaternion.identity);
            
        }
        

    }
}
