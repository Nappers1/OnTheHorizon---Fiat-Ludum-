using System;
// using System.Diagnostics;
using UnityEngine;

public enum EnemyType { Red, Blue, Green}

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody2D rb;
    [SerializeField] private int startPoint;
    [SerializeField] private float speed;
    [SerializeField] private int score = 10;
    ScoreCounter scoreScript;
    PlayerController player;
    public EnemyType enemyType = EnemyType.Red;
    
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        scoreScript = FindAnyObjectByType<ScoreCounter>(); 
    }

    // public void setDirection(int directionIndex)
    // {
    //     rb = GetComponent<Rigidbody2D>();
    //     startPoint = directionIndex;
    //     switch (directionIndex)
    //     {
    //         case 0:
    //             rb.linearVelocity = -Vector2.up*speed;
    //             break;
    //         case 1:
    //             rb.linearVelocity = -Vector2.right* speed;
    //             break;
    //         case 2:
    //             rb.linearVelocity = -Vector2.down * speed;
    //             break;
    //         case 3:
    //             rb.linearVelocity = -Vector2.left * speed;
    //             break;
    //     }
    // }

    public void setDirection(int directionIndex)
    {
        rb = GetComponent<Rigidbody2D>(); 
        startPoint = directionIndex;
        Debug.Log("Setting enemy direction. Start point: " + startPoint + ", Speed: " + speed);
        switch (directionIndex)
        {
            case 0: rb.linearVelocity = -Vector2.up * speed;    break;
            case 1: rb.linearVelocity = -Vector2.right * speed; break;
            case 2: rb.linearVelocity = -Vector2.down * speed;  break;
            case 3: rb.linearVelocity = -Vector2.left * speed;  break;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (enemyType == EnemyType.Red)
        {
            HandleRedCollision(other);
        }
        else if (enemyType == EnemyType.Green)
        {
            HandleGreenCollision(other);
        }
    }

    void HandleRedCollision(Collider2D other)
    {
        if (other.CompareTag("Shield"))
        {
            PlayerController player = FindAnyObjectByType<PlayerController>();
            if (player != null && IsBlockedByShield(player))
            {
                SuccessfulPlayer();
                // correctly blocked
            }
            else
            {
                HitPlayer(); // wrong direction
            }
        }
        else if (other.CompareTag("Player"))
        {
            HitPlayer();
        }
    }

    void HandleBlueCollision(Collider2D other)
    {
        if (other.CompareTag("Shield") || other.CompareTag("Player"))
        {
            HitPlayer();
        }
    }

    void HandleGreenCollision(Collider2D other)
    {
        if (other.CompareTag("Lightsaber"))
        {
            PlayerController player = FindAnyObjectByType<PlayerController>();
            if (player != null && IsBlockedByShield(player))
            {
                Destroy(gameObject); // correctly blocked with lightsaber
            }
            else
            {
                HitPlayer(); // wrong direction
            }
        }
        else if (other.CompareTag("Player"))
        {
            HitPlayer();
        }
    }

    bool IsPlayerOffPath(PlayerController player)
    {
        // check if player has moved away from the enemy's travel axis
        Vector3 pos = player.transform.position;
        float threshold = 0.8f;

        switch (startPoint)
        {
            case 0: // enemy from top, moving down — dodge left or right
            case 2: // enemy from bottom, moving up — dodge left or right
                return Mathf.Abs(pos.x) > threshold;
            case 1: // enemy from right, moving left — dodge up or down
            case 3: // enemy from left, moving right — dodge up or down
                return Mathf.Abs(pos.y) > threshold;
            default: return false;
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

    void Update()
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();
        if (player == null) return;

        if (enemyType == EnemyType.Red || enemyType == EnemyType.Green)
        {
            HandleTrackingMovement();
        }

        Vector2 checkPosition = (enemyType == EnemyType.Red || enemyType == EnemyType.Green)
            ? (Vector2)player.transform.position
            : Vector2.zero;

        if (Vector2.Distance(transform.position, checkPosition) < 0.3f)
        {
            if (enemyType == EnemyType.Blue)
            {
                if (player.isDodging && IsPlayerOffPath(player))
                {
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Dodge unsuccessful! Player hit by blue enemy!");
                    HitPlayer();
                }
            }
            else
            {
                HitPlayer();
            }
        }
    }

    void HandleTrackingMovement()
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();
        if (player == null) return;

        if (player.isDodging)
        {
            // chase player when dodging
            Vector2 direction = (player.transform.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;
        }
        else
        {
            // player back at center, resume original direction
            switch (startPoint)
            {
                case 0: rb.linearVelocity = -Vector2.up * speed;    break;
                case 1: rb.linearVelocity = -Vector2.right * speed; break;
                case 2: rb.linearVelocity = -Vector2.down * speed;  break;
                case 3: rb.linearVelocity = -Vector2.left * speed;  break;
            }
        }
    } 
}
