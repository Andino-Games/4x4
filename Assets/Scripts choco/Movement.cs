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
    }

    private void FixedUpdate()
    {
        rb2d.linearVelocity = pCharacter.mov*speed;
    }


}
