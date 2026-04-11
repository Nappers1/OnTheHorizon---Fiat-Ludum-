using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody2D rb;
    [SerializeField] private int startPoint;
    [SerializeField] private float speed;
    void Start()
    {
    }

    public void setDirection(int directionIndex)
    {
        rb = GetComponent<Rigidbody2D>();
        startPoint = directionIndex;
        switch (directionIndex)
        {
            case 0:
                rb.linearVelocity = -Vector2.up*speed;
                break;
            case 1:
                rb.linearVelocity = -Vector2.right* speed;
                break;
            case 2:
                rb.linearVelocity = -Vector2.down * speed;
                break;
            case 3:
                rb.linearVelocity = -Vector2.left * speed;
                break;
        }
    }
}
