using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> objectives = new List<GameObject>();
    private List<GameObject> _objectives = new List<GameObject>();

    public GameObject levelData;
    private float timer =30 ;
    [SerializeField, Tooltip("In-game display of timer")]
    private GameObject timerDisplay;
    [HideInInspector]
    public bool isPaused;
    public TimeBar timeBar;

    int currentScene;

    void Awake()
    {
        //DontDestroyOnLoad(this);
        
        GameObject[] objectivesArray = GameObject.FindGameObjectsWithTag("Objective");
        for(int i=0; i < objectivesArray.Length; i++)
        {
            objectives.Add(objectivesArray[i]);
        }
        _objectives = objectives;

        if(levelData!=null)
            levelData = GameObject.Find("Level Data");
        timer = levelData.GetComponent<LevelData>().levelTime;
        timeBar.SetMaxTime(timer);
        currentScene = SceneManager.GetActiveScene().buildIndex; //Grabs current scene's index
    }

    private void FixedUpdate()
    {
        if (!isPaused)
        {
            
            timer -= Time.deltaTime;
            timerDisplay.GetComponent<TextMeshProUGUI>().text = string.Format("{0:#.00}s", timer);

            if (timer <= 0)
                onLevelFail();

            timeBar.SetTime(timer);
        }
    }

    public void CheckForCompletion() //Checks if Level is complete
    {
        foreach(GameObject objective in objectives)
        {
            if (objective==null || objective.activeInHierarchy == true)
                return;
        }
        int nextLevel = levelData.GetComponent<LevelData>().nextLevel;
        if (nextLevel == -1)
            LoadLevel(currentScene + 1);
        else
        {
            LoadLevel(nextLevel);
        }
    }

    public void onLevelFail()
    {
        //Play Fail Animation

        LoadLevel(currentScene);
    }

    public void LoadLevel(int sceneIndex)
    {
        //Play Scene Transition
        print("Loading Level " + SceneManager.GetSceneByBuildIndex(sceneIndex).name);
        SceneManager.LoadSceneAsync(sceneIndex);
    }
}
