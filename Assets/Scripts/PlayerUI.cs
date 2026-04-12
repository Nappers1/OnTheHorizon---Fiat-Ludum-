using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject Heart1;
    [SerializeField] GameObject Heart2;
    [SerializeField] GameObject Heart3;
    [SerializeField] GameObject Heart4;
    [SerializeField] GameObject Heart5;
    int currentHearts;

    void Start()
    {
        currentHearts = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseHeart()
    {
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
        }
        currentHearts--;
    }
}
