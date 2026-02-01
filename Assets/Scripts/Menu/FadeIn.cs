using UnityEngine;

 public class FadeIn : MonoBehaviour
 {
    
    public Animator anim;

    private void Awake()
    {
        this.gameObject.SetActive(true);
    }
    public void PlayAnim()
    {
        anim.Play("FadeInAnim");
    }
    public void EndAnim()
    {
        this.gameObject.SetActive(false);
    }

}
