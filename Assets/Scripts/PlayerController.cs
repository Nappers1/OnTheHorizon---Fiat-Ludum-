using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform shieldPivot;
    public int health = 3;

    void Update()
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

    public void TakeDamage()
    {
        health--;
        Debug.Log("Health: " + health);
        if (health <= 0) Debug.Log("GAME OVER");
    }
}