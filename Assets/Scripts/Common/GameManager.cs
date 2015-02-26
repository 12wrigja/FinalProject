using UnityEngine;
using System.Collections;

//The GameManager abstract class is used to define a central point of control for the game scene currently running.
//It is marked as abstract so that it has to be defined for each scene.
//Used in this game to make custom game managers for single player and multiplayer.
public abstract class GameManager : MonoBehaviour {
	
    public abstract void StartGame();
	public abstract void PauseGame();
	public abstract void ResumeGame();
	public abstract void EndGame();
	public abstract void RestartGame();

	public static GameManager Instance;

	void Start(){
		Instance = this;
	}

	protected enum GAME_STATE {
		LOADING,
		STARTED,
		PAUSED,
		ENDED
	}
	
	protected GAME_STATE currentGameState = GAME_STATE.LOADING;

	public static bool IsGameRunning(){
		return Instance.currentGameState == GAME_STATE.STARTED;
	}
}
