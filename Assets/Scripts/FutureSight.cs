using System;
using System.Collections;
// using System.Diagnostics;
using UnityEngine;

public class FutureSight : MonoBehaviour
{

    private EnemySpawner spawnerScript;
    private string enemySequence;
    private string typeSequence;



    [SerializeField] private Color redFlashColor = Color.red;
    [SerializeField] private Color blueFlashColor = Color.blue;
    [SerializeField] private float delay = 1f;
    [SerializeField] private float flashDuration = 0.4f;
    [SerializeField] private Renderer[] spawnPoints; //0 is top, 1,2 ,3,  top right down left (clockwise)
    private Color originalColor;

    private AudioSource audioSrc;
    [SerializeField] private AudioClip beep;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        originalColor = spawnPoints[0].material.color;
        spawnerScript = GetComponent<EnemySpawner>();
    }

    public void SetEnemySequence(string sequence, string types)
    {
        enemySequence = sequence;
        typeSequence = types;
        StartCoroutine(SeeFuture());
    }

    private IEnumerator Flash(Renderer rend, Color flashColor)
    {
        audioSrc.PlayOneShot(beep);
        rend.material.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        rend.material.color = originalColor;
    }

    private IEnumerator SeeFuture()
    {
        for(int i = 0;i < enemySequence.Length; i++)
        {
            double index = char.GetNumericValue(enemySequence[i]);
            bool isBlue = typeSequence[i] == '1';
            Color flashColor = isBlue ? blueFlashColor : redFlashColor;
            StartCoroutine(Flash(spawnPoints[(int) index], flashColor));
            yield return new WaitForSeconds(delay);
        }
        StartCoroutine(spawnerScript.StartSpawning());
    }
}
