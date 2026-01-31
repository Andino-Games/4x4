using UnityEngine;
using UnityEngine.InputSystem;

public class PCharacter : MonoBehaviour
{
    public PlayerInputActions playerInput;
    public Vector2 mov;

    private void Awake()
    {
        playerInput = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Update()
    {
        mov = playerInput.Player.Move.ReadValue<Vector2>();
    }


}
