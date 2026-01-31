using UnityEngine;

public enum GameState {Playing, LevelUp, GameOver}
public class GameManager : MonoBehaviour
{
    public static  GameManager Instance;
    public GameState currentState;

    private void Awake() => Instance = this;
    
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
                Debug.Log(currentState);
                break;
            
            case GameState.GameOver:
                //Aqui cuando el juego se pone game over
                Debug.Log(currentState);
                break;
        }
    }
}