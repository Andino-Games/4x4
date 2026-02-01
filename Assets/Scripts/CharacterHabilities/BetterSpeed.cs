using UnityEngine;

public class BetterSpeed : MonoBehaviour
{
  public void ActivateBetterSpeed()
  {
      EventManager.Instance.BetterSpeedActivated();
    }
}
