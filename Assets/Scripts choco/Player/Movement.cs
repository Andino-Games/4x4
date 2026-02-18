using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

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
        baseScale = transform.localScale;
        StartBreathing();
    }
    
    private Vector3 baseScale;
    private Tween breathingTween;

    public void StartBreathing()
    {
        // Ensure we start from base scale to avoid drift
        if (breathingTween != null && breathingTween.IsActive()) breathingTween.Kill();
        transform.localScale = baseScale;
        
        // Scale up to 1.1x of base scale
        breathingTween = transform.DOScale(baseScale * 1.1f, 1.2f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    public void StopBreathing()
    {
        breathingTween?.Kill();
        transform.localScale = baseScale;
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
        speed = 8f;
    }


}
