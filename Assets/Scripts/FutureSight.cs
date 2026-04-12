using System.Collections;
using UnityEngine;

public class FutureSight : MonoBehaviour
{

    private EnemySpawner spawnerScript;
    private string enemySequence;
    private bool currentlySeeingFuture = false;
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private float delay = 0.5f;
    [SerializeField] private float flashDuration = 0.2f;
    [SerializeField] private Renderer[] spawnPoints; //0 is top, 1,2 ,3,  top right down left (clockwise)
    private Color originalColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        originalColor = spawnPoints[0].material.color;
        spawnerScript = GetComponent<EnemySpawner>();
    }

    public void SetEnemySequence(string sequence)
    {
        enemySequence = sequence;
        StartCoroutine(SeeFuture());
    }

    public bool ifSeeingFuture()
    {
        return currentlySeeingFuture;
    }

    private IEnumerator Flash(Renderer rend)
    {
        rend.material.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        rend.material.color = originalColor;
    }

    private IEnumerator SeeFuture()
    {
        currentlySeeingFuture = true;
        for(int i = 0;i < enemySequence.Length; i++)
        {
            double index = char.GetNumericValue(enemySequence[i]);
            StartCoroutine(Flash(spawnPoints[(int) index]));
            yield return new WaitForSeconds(delay);
        }
        StartCoroutine(spawnerScript.StartSpawning());
        currentlySeeingFuture = false;
    }
}
