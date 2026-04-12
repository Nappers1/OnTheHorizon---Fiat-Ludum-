using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform shieldPivot;
    public GameObject shieldObject;
    public int health = 3;

    public bool isDodging = false;

    private Vector3 centerPos = Vector3.zero;
    public float dodgeDistance = 1.5f;

    private bool hasDodged = false;

    void Update()
    {
        isDodging = Keyboard.current.leftShiftKey.isPressed ||
                    Keyboard.current.rightShiftKey.isPressed;

        shieldObject.SetActive(!isDodging);

        if (isDodging)
        {
            HandleDodge();
        }
        else
        {
            // snap back to center when shift released
            transform.position = centerPos;
            hasDodged = false;
            HandleShield();
        }
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
        // teleport to direction on keypress, snap back on release
        if (Keyboard.current.wKey.isPressed)
            transform.position = new Vector3(0, dodgeDistance, 0);
        else if (Keyboard.current.sKey.isPressed)
            transform.position = new Vector3(0, -dodgeDistance, 0);
        else if (Keyboard.current.aKey.isPressed)
            transform.position = new Vector3(-dodgeDistance, 0, 0);
        else if (Keyboard.current.dKey.isPressed)
            transform.position = new Vector3(dodgeDistance, 0, 0);
        else
            transform.position = centerPos; // no key held = back to center
    }

    public void TakeDamage()
    {
        health--;
        Debug.Log("Health: " + health);
        if (health <= 0) Debug.Log("GAME OVER");
    }
}