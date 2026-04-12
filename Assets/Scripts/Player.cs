using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    float movex;
    float movey;
    // [SerializeField] float speed = 5f;
    [SerializeField] float stepSize = 3f;

    int health = 500;

    [SerializeField] PlayerUI PlayerUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            transform.position += Vector3.up * stepSize;
        }
        if (Keyboard.current.wKey.wasReleasedThisFrame)
        {
            transform.position = transform.position -= Vector3.up * stepSize;
        }

        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            transform.position += Vector3.down * stepSize;
        }
        if (Keyboard.current.sKey.wasReleasedThisFrame)
        {
            transform.position = transform.position -= Vector3.down * stepSize;
        }

        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            transform.position += Vector3.left * stepSize;
        }
        if (Keyboard.current.aKey.wasReleasedThisFrame)
        {
            transform.position = transform.position -= Vector3.left * stepSize;
        }

        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            transform.position += Vector3.right * stepSize;
        }

        if (Keyboard.current.dKey.wasReleasedThisFrame)
        {
            transform.position = transform.position -= Vector3.right * stepSize;
        }

        // Vector2 movement = new Vector2(movex, movey);
        // transform.position += (Vector3)(movement * speed * Time.deltaTime);
    }
    void OnMove(InputValue value)
    {
        /*
        // MOVEMENT
        Vector2 v = value.Get<Vector2>();
        movex = v.x;
        movey = v.y;
        */
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Took damage!" + other.gameObject.name);
        Destroy(other.gameObject);
        health -= 100;
        PlayerUI.LoseHeart();

        if (health <= 0)
        {
            Debug.Log("GAME OVER!");
        }
    }
}
