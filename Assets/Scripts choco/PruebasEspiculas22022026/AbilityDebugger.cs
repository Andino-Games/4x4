using Abilities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityDebugger : MonoBehaviour
{
    public List<AbilityData> poolAbilities;
    public Transform playerTransform;

    public void AppearAbility(AbilityData ability)
    {
        if (playerTransform != null)
        {
            if (ability.abilityPrefab != null)
            {
                GameObject newAttack = Instantiate(ability.abilityPrefab, playerTransform);
                newAttack.transform.localPosition = Vector3.zero;
                Debug.Log($"AbilityManager: Instanciada habilidad {ability.abilityName} en el jugador.");
            }
            else
            {
                Debug.LogWarning($"AbilityManager: La habilidad {ability.abilityName} no tiene prefab asignado.");
            }
        }
        else
        {
            Debug.LogError("AbilityManager: 'playerTransform' no está asignado en el Inspector.");
        }

    }

    private void Update()
    {
        if(Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            if(poolAbilities.Count > 0)
            {
                AppearAbility(poolAbilities[0]);
            }
        }
         if(Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            if(poolAbilities.Count > 1)
            {
                AppearAbility(poolAbilities[1]);
            }
        }
         if(Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            if(poolAbilities.Count > 2)
            {
                AppearAbility(poolAbilities[2]);
            }
        }
         if(Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            if(poolAbilities.Count > 3)
            {
                AppearAbility(poolAbilities[3]);
            }
        }
         if(Keyboard.current.digit5Key.wasPressedThisFrame)
        {
            if(poolAbilities.Count > 4)
            {
                AppearAbility(poolAbilities[4]);
            }
        }
         if(Keyboard.current.digit6Key.wasPressedThisFrame)
        {
            if(poolAbilities.Count > 5)
            {
                AppearAbility(poolAbilities[5]);
            }
        }


    }

}
