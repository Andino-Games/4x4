using UnityEngine;

 public class FadeIn : MonoBehaviour
 {
    
    public Animator anim;

    private void Awake()
    {
        
        PlayAnim();
    }

    private void Start()
    {
        this.gameObject.SetActive(true);
    }
    public void PlayAnim()
    {
        anim.SetTrigger("FadeIn");
    }
    public void EndAnim()
    {
        this.gameObject.SetActive(false);
    }

}
