using System;
using System.Collections;
using UnityEngine;

public class ThanksForPlaying : MonoBehaviour
{
    public FadeOut fadeOut;
    public float autoPlayDelay = 1f;

    private void Start()
    {
        Invoke(nameof(AutoPlay), autoPlayDelay);
    }

    private void AutoPlay()
    {
        fadeOut.LoadTargetScene(0); // Assuming 0 is the index for the main menu scene
    }
}
