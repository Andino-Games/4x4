using Abilities;
using UnityEngine;

public enum GameState {Playing, LevelUp, GameOver}
public class GameManager : MonoBehaviour
{
    public static  GameManager Instance;
    public GameState currentState;
    public AbilityManager abilityManager;

    private void Awake() => Instance = this;
    
    private void Start()
    {
        ChangeState(GameState.Playing);
    }
    
    public void ChangeState(GameState newState)
    {
        currentState = newState;
        
        switch (currentState)
        {
            case GameState.Playing:
                Time.timeScale = 1;
                Debug.Log(currentState);
                break;
            
            case GameState.LevelUp:
                Time.timeScale = 0;
                abilityManager.StartAnim();
                Debug.Log("animation played");
                abilityManager.ShowAbilities();
                Debug.Log(currentState);
                break;
            
            case GameState.GameOver:
                //Aqui cuando el juego se pone game over
                Debug.Log(currentState);
                break;
        }
    }
}