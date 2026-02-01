using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAbilityManager : MonoBehaviour
{
    private void Update()
    {
        // For testing purposes, you can activate abilities with key presses
        if (Keyboard.current.rKey.isPressed)
        {
            ActivateRespawnAbility();
        }
        if (Keyboard.current.hKey.isPressed)
        {
            IncreaseMaxHealthAbility();
        }
        if (Keyboard.current.lKey.isPressed)
        {
            ActivateBetterSpeedAbility();
        }
    }
    public void ActivateRespawnAbility()
    {
        Debug.Log("Respawn Ability Activated");
        EventManager.Instance.RespawnAvailable();
    }

    public void IncreaseMaxHealthAbility()
    {
        Debug.Log("Max Health Increased Ability Activated");
        EventManager.Instance.MaxHealthIncreased();
    }

    public void ActivateBetterSpeedAbility()
    {
        Debug.Log("Better Speed Ability Activated");
        EventManager.Instance.BetterSpeedActivated();
    }


}
