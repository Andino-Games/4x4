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

        private const string SPECIAL_ABILITY_KEY = "Aumento de area";
        private readonly Dictionary<string, string> ESPICULAS = new Dictionary<string, string>
        {
            { "Basic Attack", "Spiculas" },
            { "Doble espicula", "Spiculas" },
            { "Aumento de area", "Spiculas" }
        };

        public void ShowAbilities()
        {
            Debug.Log($"[ShowAbilities] Llamado. abilitiesObtained.Count = {abilitiesObtained.Count}");
            Debug.Log($"[ShowAbilities] Habilidades obtenidas: {string.Join(", ", System.Linq.Enumerable.Select(abilitiesObtained, a => a.abilityName))}");

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
                    Debug.Log($"[ShowAbilities] Evaluando: '{ability.abilityName}' (length={ability.abilityName.Length})");
                    Debug.Log($"[ShowAbilities] ¿Coincide con SPECIAL_ABILITY_KEY '{SPECIAL_ABILITY_KEY}'? {ability.abilityName == SPECIAL_ABILITY_KEY}");
                    Debug.Log($"[ShowAbilities] Comparación exacta: '{ability.abilityName}' == '{SPECIAL_ABILITY_KEY}' = {ability.abilityName.Equals(SPECIAL_ABILITY_KEY)}");

                    if (ability.abilityName == SPECIAL_ABILITY_KEY)
                    {
                        bool hasDoubleTip = HasAbilityObtained("DoubleTip");
                        Debug.Log($"[ShowAbilities] ¡MATCH ENCONTRADO! {SPECIAL_ABILITY_KEY}. ¿HasDoubleTip? {hasDoubleTip}");


                        if (!hasDoubleTip)
                        {
                            Debug.Log($"[ShowAbilities] {SPECIAL_ABILITY_KEY} SALTADA (falta DoubleTip)");
                            continue;
                        }
                    }

                    Debug.Log($"[ShowAbilities] {ability.abilityName} añadida a options");
                    options.Add(ability);
                }
            }
            Debug.Log($"[ShowAbilities] Total opciones disponibles: {options.Count}");


            // Shuffle or pick random unique
            for (int i = 0; i < UIButtons.Length; i++)
            {
                if (options.Count > 0)
                {
                    UIButtons[i].gameObject.SetActive(true);
                    int randomIndex = Random.Range(0, options.Count);
                    AbilityData selected = options[randomIndex];
                    Debug.Log($"[ShowAbilities] Button {i} asignado: {selected.abilityName}");
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
                Debug.Log($"[SelectAbility] {SPECIAL_ABILITY_KEY} detectada. Llamando DisablePreviousHabilities()");
                DisablePreviousHabilities(ability.abilityName);
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
                /*// Resetear rotaciones de todos los ataques hijos a su posición original
                for (int i = 0; i < playerTransform.childCount; i++)
                {
                    playerTransform.GetChild(i).localRotation = Quaternion.identity;
                }*/

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

        private void DisablePreviousHabilities(string newAbilityName)
        {
            if (playerTransform == null || abilitiesObtained.Count == 0)
            {
                Debug.Log($"[DisablePreviousHabilities] Salida anticipada: playerTransform={playerTransform}, count={abilitiesObtained.Count}");
                return;
            }
            // Obtener la familia de la nueva habilidad
            string newAbilityFamily = GetAbilityFamily(newAbilityName);
            if (string.IsNullOrEmpty(newAbilityFamily))
            {
                Debug.Log($"[DisablePreviousHabilities] Familia no encontrada para {newAbilityName}");
                return;
            }

                Debug.Log($"Desactivando habilidades de la familia: {newAbilityFamily}");

            // Desactivar todas las habilidades de la misma familia en playerTransform
            for (int i = playerTransform.childCount - 1; i >= 0; i--)
            {
                Transform child = playerTransform.GetChild(i);
                string childName = child.gameObject.name;

                // Verificar si pertenece a la familia (por nombre del GameObject)
                if (BelongsToFamily(childName, newAbilityFamily))
                {
                    child.gameObject.SetActive(false);
                    Debug.Log($"Desactivada instancia: {childName}");
                }
            }

            // Remover habilidades de la familia de abilitiesObtained (excepto la nueva)
            List<AbilityData> toRemove = new List<AbilityData>();
            for (int i = 0; i < abilitiesObtained.Count - 1; i++) // -1 para excluir la última agregada
            {
                AbilityData ability = abilitiesObtained[i];
                if (GetAbilityFamily(ability.abilityName) == newAbilityFamily)
                {
                    toRemove.Add(ability);
                }
            }

            foreach (var ability in toRemove)
            {
                abilitiesObtained.Remove(ability);
                Debug.Log($"Removida de lista: {ability.abilityName}");
            }
        }

        private string GetAbilityFamily(string abilityName)
        {
            if (ESPICULAS.ContainsKey(abilityName))
                return ESPICULAS[abilityName];

            Debug.LogWarning($"Habilidad '{abilityName}' no encontrada en diccionario de familias.");
            return null;
        }

        private bool BelongsToFamily(string gameObjectName, string family)
        {
            // Buscar si el nombre del GameObject contiene alguna habilidad de esa familia
            foreach (var kvp in ESPICULAS)
            {
                if (kvp.Value == family && gameObjectName.Contains(kvp.Key))
                {
                    return true;
                }
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