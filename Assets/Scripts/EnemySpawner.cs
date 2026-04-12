using System;
using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints; //0 is top, 1,2 ,3,  top right down left (clockwise
    [SerializeField] private GameObject enemy;
    //[SerializeField] private Transform centerPos; 

    private int waveCount = 0;
    private int numDirections = 4; 
    [SerializeField] private int enemyCount = 4;
    [SerializeField] private float timeBetweenEnemy;
    private float timer = 0;
    private bool spawning = false;
    private int enemyLeft = 0;
    [SerializeField] private int[] waveEnemies;
    private void Start()
    {
        waveCount = 0;
        StartCoroutine(WaveStart());
    }

    void Update()
    {
    }

    private void Spawn()
    {
        int posIndex = UnityEngine.Random.Range(0, numDirections);
        GameObject newEnemy = Instantiate(enemy, spawnPoints[posIndex]);
        newEnemy.GetComponent<Enemy>().setDirection(posIndex);
    }

    IEnumerator WaveStart()
    {
        spawning = true; 
        for (int i = 0; i < enemyCount; i++)
        {
            yield return new WaitForSeconds(timeBetweenEnemy);
            Spawn();
        }
        waveCount++;

        while (enemyLeft != 0)
        {
            enemyLeft = 0;
            for (int i = 0; i < 4; i++)
            {
                enemyLeft += spawnPoints[i].transform.childCount;
            }
            yield return new WaitForSeconds(2f);
        }
        Debug.Log("NEXT WAVE");

        nextWave();
    }

    private void nextWave()
    {
        StartCoroutine(WaveStart());
        /*
        for (int i = 0; i < waveEnemies.Length; i++)
        {
            SetEnemySequence(waveEnemies[i]);
        }
        Debug.Log("NEXT WAVE");
        */
    }
}
