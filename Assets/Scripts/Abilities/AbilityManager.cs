using System.Collections.Generic;
using UnityEngine;
using Attack.AttackType;

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

        private const string SPECIAL_ABILITY_KEY = "Area Increased";
        private readonly string[] ESPICULAS = { "BasicAttack", "DoubleTip", "Area Increased" };

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
                    if(ability.abilityName == SPECIAL_ABILITY_KEY && !HasAbilityObtained("DoubleTip"))
                    {
                        continue; // Skip if the special ability is not already obtained
                    }
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

            if(ability.abilityName == SPECIAL_ABILITY_KEY)
            {
                DisablePreviousHabilities();
            }
            abilitiesObtained.Add(ability);
            EventManager.Instance.ResetHealth();

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
                // Resetear rotaciones de todos los ataques hijos a su posición original
                for (int i = 0; i < playerTransform.childCount; i++)
                {
                    playerTransform.GetChild(i).localRotation = Quaternion.identity;
                }

                if (ability.abilityPrefab != null)
                {
                    GameObject newAttack = Instantiate(ability.abilityPrefab, playerTransform);
                    newAttack.transform.localPosition = Vector3.zero;

                    // Copiar la rotación local del BasicAttack + 90 grados
                    BasicAttack baseAttack = playerTransform.GetComponentInChildren<BasicAttack>();
                    if (baseAttack != null)
                    {
                        float currentAngleZ = baseAttack.transform.localEulerAngles.z;
                        float newAngleZ = currentAngleZ + 90f;
                        newAttack.transform.localRotation = Quaternion.Euler(0, 0, newAngleZ);
                    }
                    else
                    {
                        // Fallback: alinear a la rotación local del player
                        newAttack.transform.localRotation = Quaternion.identity;
                    }
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

        private bool HasAbilityObtained(string abilityName)
        {
            foreach (var ability in abilitiesObtained)
            {
                if (ability.abilityName == abilityName)
                    return true;
            }
            return false;
        }

        private void DisablePreviousHabilities()
        {
            if (playerTransform == null || abilitiesObtained.Count == 0)
                return;

            // Encontrar todas las habilidades de la familia de bastones en abilitiesObtained
            List<AbilityData> staffAbilitiesToRemove = new List<AbilityData>();
            List<int> childIndicesToDisable = new List<int>();

            // Buscar habilidades de la familia en abilitiesObtained (excluyendo la que se acaba de agregar)
            for (int i = 0; i < abilitiesObtained.Count - 1; i++) // -1 para excluir la última agregada
            {
                AbilityData ability = abilitiesObtained[i];
                if (IsStaffAbility(ability.abilityName))
                {
                    staffAbilitiesToRemove.Add(ability);
                }
            }

            // Si hay habilidades de bastones previas, desactivarlas
            if (staffAbilitiesToRemove.Count > 0)
            {
                // Desactivar los GameObjects correspondientes en playerTransform
                foreach (var ability in staffAbilitiesToRemove)
                {
                    // Buscar el hijo que corresponde a esta habilidad (por nombre)
                    for (int i = playerTransform.childCount - 1; i >= 0; i--)
                    {
                        Transform child = playerTransform.GetChild(i);
                        if (child.gameObject.name.Contains(ability.abilityName))
                        {
                            child.gameObject.SetActive(false);
                            Debug.Log($"Desactivada instancia de {ability.abilityName}");
                            break;
                        }
                    }
                }

                // Remover de abilitiesObtained
                foreach (var ability in staffAbilitiesToRemove)
                {
                    abilitiesObtained.Remove(ability);
                }

                Debug.Log($"Desactivadas {staffAbilitiesToRemove.Count} habilidades de bastones previas.");
            }
            else
            {
                Debug.Log("No hay habilidades de bastones previas para desactivar.");
            }
        }

        private bool IsStaffAbility(string abilityName)
        {
            foreach (var staffName in ESPICULAS)
            {
                if (abilityName == staffName)
                    return true;
            }
            return false;
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