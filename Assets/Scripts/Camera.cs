using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance;

    public float duration = 0f;
    public float magnitude = 0.2f;

    private Vector3 originalPosition;

    void Awake()
    {
        Instance = this;
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (duration > 0)
        {
            transform.localPosition = originalPosition + Random.insideUnitSphere * magnitude;
            duration -= Time.deltaTime;
        }
        else
        {
            duration = 0f;
            transform.localPosition = originalPosition;
        }
    }

    public void TriggerShake(float shakeDuration, float shakeMagnitude)
    {
        duration = shakeDuration;
        magnitude = shakeMagnitude;
    }
}