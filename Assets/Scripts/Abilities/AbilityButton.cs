using UnityEngine;

namespace Abilities
{
    public class AbilityButton : MonoBehaviour
    {
        private AbilityData _actualData;
        public TMPro.TextMeshProUGUI textName;
        public TMPro.TextMeshProUGUI textDescription;
        public UnityEngine.UI.Image imageIcon;

        public void SetButton(AbilityData data)
        {
            if (data == null)
            {
                Debug.LogError("AbilityButton: Recibió data nula en SetButton.");
                return;
            }

            _actualData = data;

            if (textName != null) textName.text = data.abilityName;
            else Debug.LogWarning($"AbilityButton: 'textName' no está asignado en el Inspector en {gameObject.name}");

            if (imageIcon != null) imageIcon.sprite = data.abilityIcon;
            else Debug.LogWarning($"AbilityButton: 'imageIcon' no está asignado en el Inspector en {gameObject.name}");

            if (textDescription != null) textDescription.text = data.abilityDescription;
            else Debug.LogWarning($"AbilityButton: 'textDescription' no está asignado en el Inspector en {gameObject.name}");
        }

        public void OnClick()
        {
            Debug.Log($"AbilityButton: Click detectado en {gameObject.name}");
            var manager = FindFirstObjectByType<AbilityManager>();

            if (manager != null)
            {
                manager.SelectAbility(_actualData);
            }
            else
            {
                Debug.LogError("AbilityButton: No se encontró el AbilityManager en la escena.");
            }
        }
    }
}