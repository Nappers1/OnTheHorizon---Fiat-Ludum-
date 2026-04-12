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
    [SerializeField] private float timeBetweenWave;
    private float timer = 0;
    private bool spawning = false;
    private int enemyLeft = 0;
    [SerializeField] private int[] waveEnemies;

    private FutureSight futureSight;
    private string enemySequence;
    private void Start()
    {
        waveCount = 0;
        futureSight = GetComponent<FutureSight>();
        WaveStart();
    }
    private void Spawn(int posIndex)
    {
        GameObject newEnemy = Instantiate(enemy, spawnPoints[posIndex]);
        newEnemy.GetComponent<Enemy>().setDirection(posIndex);
    }

    public IEnumerator StartSpawning()
    {
        for (int i = 0; i < enemySequence.Length; i++)
        {
            Spawn((int) char.GetNumericValue(enemySequence[i]));
            yield return new WaitForSeconds(timeBetweenEnemy);
        }

        //checks if enemies are left, recursively calls the wave start again. 
        while (enemyLeft != 0)
        {
            enemyLeft = 0;
            for (int i = 0; i < 4; i++)
            {
                enemyLeft += spawnPoints[i].transform.childCount;
            }
        }
        yield return new WaitForSeconds(timeBetweenWave);
        nextWave();

    }

    private void WaveStart()
    {
        string newSequence = "";
        for(int i = 0;i < enemyCount; i++)
        {
            newSequence += UnityEngine.Random.Range(0, numDirections).ToString();
        }
        enemySequence = newSequence;
        futureSight.SetEnemySequence(newSequence);
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
