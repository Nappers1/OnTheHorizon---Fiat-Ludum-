using System;
using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
// using System.Diagnostics;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints; //0 is top, 1,2 ,3,  top right down left (clockwise
    [SerializeField] private GameObject redEnemyPrefab;
    [SerializeField] private GameObject blueEnemyPrefab;
    [SerializeField] private GameObject greenEnemyPrefab;

    private int waveCount = 1;
    private int numDirections = 4; 
    [SerializeField] private float enemyCount = 3;
    [SerializeField] private float timeBetweenEnemy;
    [SerializeField] private float timeBetweenWave;
    private float timer = 0;
    private bool spawning = false;
    private int enemyLeft = 0;
    [SerializeField] private int[] waveEnemies;

    private FutureSight futureSight;
    private string enemySequence;
    private string typeSequence;

    [SerializeField] private Player player;
    [SerializeField] private GameObject nextWaveButton;
    [SerializeField] private TMP_Text waveText;

    [SerializeField] private int blueWaveStart = 3;
    [SerializeField] private int greenWaveStart = 5;
    [SerializeField] private int startAddingEnemyCount = 8;
    [SerializeField] private float addEnemyFrequency = 0.5f; //adds one enemy every 2 waves



    private void Start()
    {
        waveCount = 1;
        nextWaveButton.SetActive(false);
        futureSight = GetComponent<FutureSight>();
        WaveStart();
    }
    private void Spawn(int posIndex, int typeIndex)
    {
        GameObject prefab;
        switch (typeIndex)
        {
            case 1:  prefab = blueEnemyPrefab;  break;
            case 2:  prefab = greenEnemyPrefab; break;
            default: prefab = redEnemyPrefab;   break;
        }
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
            yield return new WaitForSeconds(0.1f);
        }
        //yield return new WaitForSeconds(timeBetweenWave);

        if(!player.isGameOver())
        {
            if (player.redoWave == false)
            {
                nextWaveButton.SetActive(true);
                //nextWave();
            }
            else
            {
                player.redoWave = false;
                futureSight.SetEnemySequence(enemySequence, typeSequence);
            }
        }
        

    }

    //public so button can press
    private void WaveStart()
    {

        string newSequence = "";
        string newTypeSequence = "";

        int lastPos = -1; // track last spawn position
        for (int i = 0; i < Mathf.Floor(enemyCount); i++)
        {
            // keep rolling until we get a different position than last
            int newPos;
            do
            {
                newPos = UnityEngine.Random.Range(0, numDirections);
            } while (newPos == lastPos);

            lastPos = newPos;
            newSequence += newPos.ToString();
            //control which enemies statr
            if (waveCount <= 3)
                newTypeSequence += '0';
            else if (waveCount <= 5)
                newTypeSequence += UnityEngine.Random.Range(0, 2).ToString();
            //else
              //  newTypeSequence += UnityEngine.Random.Range(0, 3).ToString();
        }
        enemySequence = newSequence;
        typeSequence = newTypeSequence;
        futureSight.SetEnemySequence(newSequence, newTypeSequence);
    }

    //button press calls this
    public void nextWave()
    {
        Debug.Log("PRESSED");
        waveCount++;
        //only add # of enemies after wave
        if (waveCount >= startAddingEnemyCount)
        {
            enemyCount += addEnemyFrequency;
            //enemy count is a float, whenever we spawn we use the floor value
        }
        waveText.text = "WAVE " + waveCount;
        nextWaveButton.SetActive(false);
        WaveStart();
        
        /*
        for (int i = 0; i < waveEnemies.Length; i++)
        {
            SetEnemySequence(waveEnemies[i]);
        }
        Debug.Log("NEXT WAVE");
        */
    }


}
