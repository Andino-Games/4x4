using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    [Header("XP")]
    public int level = 1;
    [Tooltip("Cada cuantos niveles se permite elegir una habilidad")]
    public int levelsPerAbility = 1;

    public int currentXP = 0;
    public int requiredXP = 10;
    public TMPro.TextMeshProUGUI levelText;

    [Header("UI")]
    [SerializeField] private Image xpBar;

    public void AddXP(int amount)
    {
        currentXP += amount;

        if (currentXP >= requiredXP)
        {
            LevelUp();
            
            // Solo pausar y mostrar habilidades si es múltiplo de levelsPerAbility
            if (level % levelsPerAbility == 0)
            {
                GameManager.Instance.ChangeState(GameState.LevelUp);
            }
        }

        UpdateXPBar();
    }

    void LevelUp()
    {
        currentXP -= requiredXP; 
        level++;

        requiredXP = Mathf.RoundToInt(requiredXP * 1.5f); 

        Debug.Log("Nivel actual: " + level);
    }

    void UpdateXPBar()
    {
        xpBar.fillAmount = (float)currentXP / requiredXP;
        levelText.text = level.ToString();
    }
}
