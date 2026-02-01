using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "Abilities", menuName = "Game/Ability")]
    public class AbilityData : ScriptableObject
    {
        public string abilityName;
        public string abilityDescription;
        public Sprite abilityIcon;
        public GameObject abilityPrefab;
    }
}