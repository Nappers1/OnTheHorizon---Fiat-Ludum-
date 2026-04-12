using System.Collections;
using UnityEngine;

public class FutureSight : MonoBehaviour
{

    private string enemySequence;
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private float delay = 1f;
    [SerializeField] private float flashDuration = 0.3f;
    [SerializeField] private Renderer[] spawnPoints; //0 is top, 1,2 ,3,  top right down left (clockwise)
    private Color originalColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        originalColor = spawnPoints[0].material.color;
        StartCoroutine(Flash(spawnPoints[0]));
    }

    private IEnumerator Flash(Renderer rend)
    {
        rend.material.color = flashColor;
        Debug.Log("Flashed!");
        yield return new WaitForSeconds(flashDuration);
        rend.material.color = originalColor;
        Debug.Log("returned to normal");
    }

    public void SetEnemySequence(string sequence)
    {
        enemySequence = sequence;
        for(int i = 0;i < enemySequence.Length;i++)
        {
            
        }
    }
    private IEnumerator SeeFuture()
    {
        //Flash();
        yield return new WaitForSeconds(delay);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
