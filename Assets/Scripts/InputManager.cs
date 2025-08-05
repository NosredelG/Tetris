using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    Vector2 movementInput;
    bool flipInput;
    float countFlip;
    float flipTime;
    public static InputManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        flipTime = 0.01f;
    }

    void Update()
    {
        if (countFlip <= 0)
        {
            flipInput = false;
        }
        if (countFlip > 0)
        {
            countFlip -= Time.deltaTime;
        }
    }

    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    public void OnFlip(InputValue value)
    {
        countFlip = flipTime;
        flipInput = value.isPressed;

        if (GameControl.instance.IsGameOver)
        {
            GameControl.instance.Restart();
        }
    }

    public void OnPause(InputValue value)
    {
        GameControl.instance.PauseGame();
    }

    public static Vector2 GetMovementInput()
    {
        return instance.movementInput;
    }

    public static bool GetFlipInput()
    {
        return instance.flipInput;
    }
}
