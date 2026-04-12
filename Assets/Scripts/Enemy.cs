using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody2D rb;
    [SerializeField] private int startPoint;
    [SerializeField] private float speed;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shield"))
        {
            // check if shield is facing the right direction to block
            PlayerController player = FindFirstObjectByType<PlayerController>();
            if (player != null && IsBlockedByShield(player))
            {
                Destroy(gameObject);
            }
            else
            {
                // shield is facing wrong way, still hits player
                HitPlayer();
            }
        }
        else if (other.CompareTag("Player"))
        {
            HitPlayer();
        }
    }

    bool IsBlockedByShield(PlayerController player)
    {
        // enemy coming from top (case 0) → shield must face up (W)
        // enemy coming from right (case 1) → shield must face right (D)  
        // enemy coming from bottom (case 2) → shield must face down (S)
        // enemy coming from left (case 3) → shield must face left (A)

        float shieldAngle = player.shieldPivot.eulerAngles.z;

        switch (startPoint)
        {
            case 0: return Mathf.Approximately(shieldAngle, 90f);   // W
            case 1: return Mathf.Approximately(shieldAngle, 0f);    // D
            case 2: return shieldAngle > 269f && shieldAngle < 271f; // S (-90 wraps to 270)
            case 3: return Mathf.Approximately(shieldAngle, 180f);  // A
            default: return false;
        }
    }

    void HitPlayer()
    {
        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player != null)
        {
            player.TakeDamage();
            Debug.Log("HIT! -1");
        }
        Destroy(gameObject);
    }
}
