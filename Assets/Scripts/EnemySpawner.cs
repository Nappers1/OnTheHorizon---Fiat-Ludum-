using System;
using UnityEngine;
using System.Collections;
// using System.Diagnostics;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints; //0 is top, 1,2 ,3,  top right down left (clockwise
    [SerializeField] private GameObject redEnemyPrefab;
    [SerializeField] private GameObject blueEnemyPrefab;
    

    private int waveCount = 0;
    private int numDirections = 4; 
    [SerializeField] private int enemyCount = 3;
    [SerializeField] private float timeBetweenEnemy;
    [SerializeField] private float timeBetweenWave;
    private float timer = 0;
    private bool spawning = false;
    private int enemyLeft = 0;
    [SerializeField] private int[] waveEnemies;

    private FutureSight futureSight;
    private string enemySequence;
    private string typeSequence;

    private void Start()
    {
        waveCount = 0;
        futureSight = GetComponent<FutureSight>();
        WaveStart();
    }
    private void Spawn(int posIndex, int typeIndex)
    {
        GameObject prefab = typeIndex == 1 ? blueEnemyPrefab : redEnemyPrefab;
        GameObject newEnemy = Instantiate(prefab, spawnPoints[posIndex].position, Quaternion.identity);
        Debug.Log("Spawned at position: " + spawnPoints[posIndex].position);
        newEnemy.GetComponent<Enemy>().setDirection(posIndex);
        
    }

    public IEnumerator StartSpawning()
    {
        for (int i = 0; i < enemySequence.Length; i++)
        {
            int posIndex = (int)char.GetNumericValue(enemySequence[i]);
            int typeIndex = (int)char.GetNumericValue(typeSequence[i]);
            Spawn(posIndex, typeIndex);
            yield return new WaitForSeconds(timeBetweenEnemy);
        }

        //checks if enemies are left, recursively calls the wave start again. 
        // while (enemyLeft != 0)
        // {
        //     enemyLeft = 0;
        //     for (int i = 0; i < 4; i++)
        //     {
        //         enemyLeft += spawnPoints[i].transform.childCount;
        //     }
        // }

        while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(timeBetweenWave);
        nextWave();

    }

    private void WaveStart()
    {
        string newSequence = "";
        string newTypeSequence = "";
        for(int i = 0;i < enemyCount; i++)
        {
            newSequence += UnityEngine.Random.Range(0, numDirections).ToString();
            newTypeSequence += UnityEngine.Random.Range(0, 2).ToString(); // 0 = red, 1 = blue
        }
        enemySequence = newSequence;
        typeSequence = newTypeSequence;
        futureSight.SetEnemySequence(newSequence, newTypeSequence);
    }

    private void nextWave()
    {
        WaveStart();
        waveCount++;
        
        Debug.Log("WAVE #" + waveCount);
        /*
        for (int i = 0; i < waveEnemies.Length; i++)
        {
            SetEnemySequence(waveEnemies[i]);
        }
        Debug.Log("NEXT WAVE");
        */
    }
}
