using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    float movex;
    float movey;
    // [SerializeField] float speed = 5f;
    [SerializeField] float stepSize = 3f;

    PlayerUI playerUI;
    AudioSource audioSrc;
    [SerializeField] AudioClip move;
    [SerializeField] AudioClip hit;


    int health = 500;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        playerUI = GetComponent<PlayerUI>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            transform.position += Vector3.up * stepSize;
            audioSrc.PlayOneShot(move);
        }
        if (Keyboard.current.wKey.wasReleasedThisFrame)
        {
            transform.position = transform.position -= Vector3.up * stepSize;
        }

        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            transform.position += Vector3.down * stepSize;
            audioSrc.PlayOneShot(move);

        }
        if (Keyboard.current.sKey.wasReleasedThisFrame)
        {
            transform.position = transform.position -= Vector3.down * stepSize;
        }

        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            transform.position += Vector3.left * stepSize;
            audioSrc.PlayOneShot(move);

        }
        if (Keyboard.current.aKey.wasReleasedThisFrame)
        {
            transform.position = transform.position -= Vector3.left * stepSize;
        }

        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            transform.position += Vector3.right * stepSize;
            audioSrc.PlayOneShot(move);

        }

        if (Keyboard.current.dKey.wasReleasedThisFrame)
        {
            transform.position = transform.position -= Vector3.right * stepSize;
        }*/

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
        //Debug.Log("Took damage!" + other.gameObject.name);
        audioSrc.PlayOneShot(hit);
        Destroy(other.gameObject);
        health -= 100;
        playerUI.LoseHeart();

        if (health <= 0)
        {
            Debug.Log("GAME OVER!");
        }
    }
}
