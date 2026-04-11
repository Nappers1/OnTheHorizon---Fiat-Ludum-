using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints; //0 is top, 1,2 ,3, 4 top right down left (clockwise
    [SerializeField] private GameObject enemy;
    //[SerializeField] private Transform centerPos; 

    private int waveCount = 0;
    private int numDirections = 4; 
    [SerializeField] private int enemyCount = 4;
    private void Start()
    {
        waveCount = 0;
        WaveStart();
    }

    private void WaveStart()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            int posIndex = UnityEngine.Random.Range(0, numDirections - 1);
            GameObject newEnemy = Instantiate(enemy, spawnPoints[posIndex]);
            newEnemy.GetComponent<Enemy>().setDirection(posIndex);
            Debug.Log(posIndex);
        }
        waveCount++;
    }
}
