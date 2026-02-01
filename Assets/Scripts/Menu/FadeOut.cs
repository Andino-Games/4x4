using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public MainMenu mainMenu;
    public Animator anim;

    private void Start()
    {
        this.gameObject.SetActive(false);
    }
    public void PlayAnim()
    {
        this.gameObject.SetActive(true);
        anim.SetTrigger("FadeOut");
    }   

    public void StartRoom()
    {
        mainMenu.StartGame();
    }
}
