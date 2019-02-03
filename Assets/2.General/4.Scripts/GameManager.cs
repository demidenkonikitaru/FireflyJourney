using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager> {

    public GameObject player;
    public Transform playerTrs;

    public Transform gameCamera;
    

    public Vector2 playerStartPoint;
    public Vector3 cameraStartPoint;



	void Start () {
		
	}
	

	void Update () {
		
	}

    public void StartGame()
    {
        ResetPositions();
        PlayerController.Instance.UnfreezePlayer();
        //player.SetActive(true);
    }

    public void FinishGame()
    {
        ResetPositions();
        PlayerController.Instance.FreezePlayer();
        LevelGenerator.Instance.DestroyAndResetLevel();
        //player.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void ResetPositions()
    {
        playerTrs.position = playerStartPoint;
        gameCamera.position = cameraStartPoint;
    }
}
