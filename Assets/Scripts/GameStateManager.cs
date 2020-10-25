using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameObject playerr;
    private enum states
    {
        play,
        pause,
        cutscene
    }
    private states currentState = states.play;
    private states priorState;

    [Tooltip("The menu that appears when the game is paused")]
    public GameObject pauseMenu;
    private GameObject levelManager;
    private GameObject[] enemies;

    void Start()
    {
        levelManager = GameObject.Find("Level Manager");
    }

    void Update()
    {
        #region Pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState != states.pause)
            {
                //print("Paused");
                Pause();
            }
            else
            {
                //print("Resuming");
                UnPause();
            }
        }
        #endregion
    }

    void Pause()
    {
        priorState = currentState;
        currentState = states.pause;

       

        levelManager.GetComponent<LevelManager>().isPaused = true;



        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }

        enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach(GameObject enemy in enemies)
        {
            enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<topdown_Player>().enabled = false;
        

    }

    public void UnPause()
    {
        currentState = priorState;

        levelManager.GetComponent<LevelManager>().isPaused = false;

        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            //enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<topdown_Player>().enabled = true;
    }

    public void ExitGame() //Accessed by the exit button on the pause menu
    {
        print("Exiting");
        Application.Quit();
    }
}
