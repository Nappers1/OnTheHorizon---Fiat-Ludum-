using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerMode { Shield, Dodge, Lightsaber }

public class PlayerController : MonoBehaviour
{
    public Transform shieldPivot;
    public GameObject shieldObject;
    public GameObject lightsaberObject;
    public int health = 3;

    public PlayerMode currentMode = PlayerMode.Shield;
    public bool isDodging => currentMode == PlayerMode.Dodge;

    private Vector3 centerPos = Vector3.zero;
    public float dodgeDistance = 1.5f;

    void Update()
    {
        HandleModeSwitch();
        UpdateVisuals();

        switch (currentMode)
        {
            case PlayerMode.Shield:   HandleShield(); break;
            case PlayerMode.Dodge:    HandleDodge();  break;
            case PlayerMode.Lightsaber: HandleShield(); break;
        }
    }

    void HandleModeSwitch()
    {
        if (Keyboard.current.backslashKey.wasPressedThisFrame)
        {
            if (currentMode == PlayerMode.Lightsaber)
                currentMode = PlayerMode.Shield;
            else
                currentMode = PlayerMode.Lightsaber;
        }

        // dodge overrides everything while shift held
        if (Keyboard.current.leftShiftKey.isPressed ||
            Keyboard.current.rightShiftKey.isPressed)
        {
            currentMode = PlayerMode.Dodge;
        }
        else if (currentMode == PlayerMode.Dodge)
        {
            // shift released, return to shield
            currentMode = PlayerMode.Shield;
            transform.position = centerPos;
        }
    }

    void UpdateVisuals()
    {
        shieldObject.SetActive(currentMode == PlayerMode.Shield);
        lightsaberObject.SetActive(currentMode == PlayerMode.Lightsaber);
    }

    void HandleShield()
    {
        if (Keyboard.current.wKey.isPressed)
            shieldPivot.rotation = Quaternion.Euler(0, 0, 90);
        else if (Keyboard.current.sKey.isPressed)
            shieldPivot.rotation = Quaternion.Euler(0, 0, -90);
        else if (Keyboard.current.aKey.isPressed)
            shieldPivot.rotation = Quaternion.Euler(0, 0, 180);
        else if (Keyboard.current.dKey.isPressed)
            shieldPivot.rotation = Quaternion.Euler(0, 0, 0);
    }

    void HandleDodge()
    {
        if (Keyboard.current.wKey.isPressed)
            transform.position = new Vector3(0, dodgeDistance, 0);
        else if (Keyboard.current.sKey.isPressed)
            transform.position = new Vector3(0, -dodgeDistance, 0);
        else if (Keyboard.current.aKey.isPressed)
            transform.position = new Vector3(-dodgeDistance, 0, 0);
        else if (Keyboard.current.dKey.isPressed)
            transform.position = new Vector3(dodgeDistance, 0, 0);
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