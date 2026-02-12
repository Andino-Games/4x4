using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public class AbilityManager : MonoBehaviour
    {
        public List<AbilityData> poolAbilities;
        public List<AbilityData> abilitiesObtained;
        public Transform playerTransform;

        [Header("UI References")]
        public GameObject abilitiesPanel;
        public Animator panelAnimation;
        public AbilityButton[] UIButtons;
        public UnityEngine.UI.Image[] obtainedAbilityIcons;



        public void ShowAbilities()
        {
            if (abilitiesObtained.Count >= 3)
            {
                Debug.Log("Ya estan el maximo de las habilidades x partida");
                GameManager.Instance.ChangeState(GameState.Playing);
                return;
            }
            abilitiesPanel.SetActive(true);
            
            // Filter out abilities that have already been obtained
            List<AbilityData> options = new List<AbilityData>();
            foreach (var ability in poolAbilities)
            {
                if (!abilitiesObtained.Contains(ability))
                {
                    options.Add(ability);
                }
            }

            // Shuffle or pick random unique
            for (int i = 0; i < UIButtons.Length; i++)
            {
                if (options.Count > 0)
                {
                    UIButtons[i].gameObject.SetActive(true);
                    int randomIndex = Random.Range(0, options.Count);
                    AbilityData selected = options[randomIndex];

                    UIButtons[i].SetButton(selected);
                    options.RemoveAt(randomIndex);
                }
                else
                {
                    UIButtons[i].gameObject.SetActive(false);
                }
            }
        }

        public void SelectAbility(AbilityData ability)
        {
            Debug.Log($"AbilityManager: Seleccionando habilidad: {ability.abilityName}");
            abilitiesObtained.Add(ability);
            EventManager.Instance.MaxHealthIncreased();

            // Update HUD Icons
            int index = abilitiesObtained.Count - 1;
            if (obtainedAbilityIcons != null && index < obtainedAbilityIcons.Length)
            {
                if (obtainedAbilityIcons[index] != null)
                {
                    obtainedAbilityIcons[index].sprite = ability.abilityIcon;
                    obtainedAbilityIcons[index].gameObject.SetActive(true);
                }
            }

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

            abilitiesPanel.SetActive(false);
            GameManager.Instance.ChangeState(GameState.Playing);
        }

        public void StartAnim()
        { 
            
            if (panelAnimation != null)
            {
                panelAnimation.SetTrigger("Open");
            }
        }
    }
}