using System.Collections;
using System.Linq;
using Unity.VisualScripting;
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

    private void OnEnable()
    {
        WaveSystem.StartWave += StartSpawn;
        WaveSystem.StopWave += StopSpawn;
    }
    private void OnDisable()
    {
        WaveSystem.StopWave -= StopSpawn;
        WaveSystem.StartWave -= StartSpawn;
    }

    private void StartSpawn()
    {
        spawn = StartCoroutine(nameof(SpawnTimer));
    }
    private void StopSpawn()
    {
        if (spawn != null)
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
