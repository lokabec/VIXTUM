using System.Collections;
using System.Linq;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private ScoreSystem scoreSystem;
    private Transform spawnPoint;
    private Coroutine spawn;
    [SerializeField] private GameObject[] Enemy;
    [SerializeField] private int spawnTime = 2;
    void Start()
    {
        spawnPoint = GetComponent<Transform>();
        foreach(GameObject en in Enemy)
        {
            en.GetComponent<Enemy>().scoreSystem = scoreSystem;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            spawn = StartCoroutine(nameof(SpawnTimer));
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
            if (scoreSystem.Score < 500)
            {
                Instantiate(Enemy[Random.Range(0, Enemy.Count())], new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0f), Quaternion.identity);
            }
            else if(scoreSystem.Score >= 500 && scoreSystem.Score < 1000)
            {
                Instantiate(Enemy[Random.Range(0, Enemy.Count())], new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0f), Quaternion.identity);
                Instantiate(Enemy[Random.Range(0, Enemy.Count())], new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0f), Quaternion.identity);
            }
            else if (scoreSystem.Score >= 1000)
            {
                Instantiate(Enemy[Random.Range(0, Enemy.Count())], new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0f), Quaternion.identity);
                Instantiate(Enemy[Random.Range(0, Enemy.Count())], new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0f), Quaternion.identity);
                Instantiate(Enemy[Random.Range(0, Enemy.Count())], new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0f), Quaternion.identity);
            }

            yield return new WaitForSeconds(spawnTime);
        }
        

    }
}
