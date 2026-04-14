using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerMode { Shield, Dodge, Lightsaber }

public class PlayerController : MonoBehaviour
{
    public Transform shieldPivot;
    public GameObject shieldObject;
    public GameObject lightsaberObject;
    public int health = 5;
    int lastPress = 0;
    bool fromDash = false;

    public PlayerMode currentMode = PlayerMode.Shield;
    public bool isDodging => currentMode == PlayerMode.Dodge;

    private Vector3 centerPos = Vector3.zero;
    public float dodgeDistance = 1.5f;

    [SerializeField] private Sprite[] shieldSprites; //0 is top, clockwise 
    [SerializeField] private Sprite[] dodgeAnims; //0 top, 1 is left 
    private SpriteRenderer render;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        shieldPivot.rotation = Quaternion.Euler(0, 0, 180);
    }
    void Update()
    {
        HandleModeSwitch();
        UpdateVisuals();

        HandleShield();

        if (currentMode == PlayerMode.Dodge)
        {
            HandleDodge();
        }
    }

    void HandleModeSwitch()
    {
<<<<<<< Updated upstream
=======
        currentMode = PlayerMode.Shield;
        /*
>>>>>>> Stashed changes
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (currentMode == PlayerMode.Lightsaber)
                currentMode = PlayerMode.Shield;
            else
                currentMode = PlayerMode.Lightsaber;
        }*/

        // dodge overrides everything while shift held
        if (Keyboard.current.leftShiftKey.isPressed ||
            Keyboard.current.rightShiftKey.isPressed)
        {
            currentMode = PlayerMode.Dodge;
        }
        else if (currentMode == PlayerMode.Dodge)
        {
            // shift released, return to shield

            render.flipY = false;
            currentMode = PlayerMode.Shield;
            transform.position = centerPos;
        }
    }

    void UpdateVisuals()
    {
        shieldObject.SetActive(currentMode == PlayerMode.Shield);
        lightsaberObject.SetActive(currentMode == PlayerMode.Lightsaber);
        shieldObject.GetComponent<Collider2D>().enabled = currentMode == PlayerMode.Shield;
        lightsaberObject.GetComponent<Collider2D>().enabled = currentMode == PlayerMode.Lightsaber;
    }

    void HandleShield()
    {
        render.flipY = false;
        if (Keyboard.current.wKey.isPressed)
        {
            shieldPivot.rotation = Quaternion.Euler(0, 0, 90);
            render.sprite = shieldSprites[0];
            render.flipX = false;
            fromDash = false;
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            shieldPivot.rotation = Quaternion.Euler(0, 0, -90);
            render.sprite = shieldSprites[2];
            render.flipX = false;
            fromDash = false;
        }
        else if (Keyboard.current.aKey.isPressed)
        {
            shieldPivot.rotation = Quaternion.Euler(0, 0, 180);
            render.sprite = shieldSprites[3];
            render.flipX = false;
            fromDash = false;
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            shieldPivot.rotation = Quaternion.Euler(0, 0, 0);
            render.sprite = shieldSprites[3];
            render.flipX = true;
            fromDash = false;
        }
    }

    void HandleDodge()
    {
        fromDash=true;
        if (Keyboard.current.wKey.isPressed)
        {
            lastPress = 0;
            transform.position = new Vector3(0, dodgeDistance, 0);
            //render.sprite = dodgeAnims[0];
            //render.flipY = false;
            //render.flipX = false;

        }
        else if (Keyboard.current.sKey.isPressed)
        {
            lastPress = 2;
            transform.position = new Vector3(0, -dodgeDistance, 0);
            //render.sprite = dodgeAnims[0];
            //render.flipY = true;
            //render.flipX = false;

        }
        else if (Keyboard.current.aKey.isPressed)
        {
            lastPress = 3;
            transform.position = new Vector3(-dodgeDistance, 0, 0);
            //render.sprite = dodgeAnims[1];
            //render.flipX = false;
            //render.flipY = false;
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            lastPress = 1;
            transform.position = new Vector3(dodgeDistance, 0, 0);
            //render.sprite = dodgeAnims[1];
            //render.flipX = true;
            //render.flipY = false;
        }
        else
            transform.position = centerPos;
    }

    public void TakeDamage()
    {
        health--;
        Debug.Log("Health: " + health);
        if (health <= 0) Debug.Log("GAME OVER");
    }
}