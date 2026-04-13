using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    int currentHearts;
    [SerializeField] private Transform heartsParent;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    void Start()
    {
        currentHearts = 5;
    }

    public void LoseHeart()
    {
        if (currentHearts > 0)
        {
            if (heartsParent.GetChild(currentHearts - 1).GetComponent<Image>() != null)
                heartsParent.GetChild(currentHearts - 1).GetComponent<Image>().sprite = emptyHeart;
        }
        
        /*
        if (currentHearts == 5)
        {
            Heart5.SetActive(false);
        }
        else if (currentHearts == 4)
        {
            Heart4.SetActive(false);
        }
        else if (currentHearts == 3)
        {
            Heart3.SetActive(false);
        }
        else if (currentHearts == 2)
        {
            Heart2.SetActive(false);
        }
        else if (currentHearts == 1)
        {
            Heart1.SetActive(false);
        }*/
        currentHearts--;
    }
}
