using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
   public Rigidbody2D rb2d;
   public PCharacter pCharacter;
    public float speed = 5f;

    private void Start()
    {
         rb2d = GetComponent<Rigidbody2D>();
        pCharacter = GetComponent<PCharacter>();
        EventManager.Instance.OnBetterSpeedActivated += ActivateBetterSpeed;

    }
    private void OnDisable()
    {
        EventManager.Instance.OnBetterSpeedActivated -= ActivateBetterSpeed;
    }

    private void FixedUpdate()
    {
        rb2d.linearVelocity = pCharacter.mov*speed;
    }

    public void ActivateBetterSpeed()
    {
        speed = 10f;
    }


}
