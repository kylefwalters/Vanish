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
    [HideInInspector]
    public int itemsRemaining;

    private GameObject gameStateManager;
    public GameObject levelData;
    private float timer =30 ;
    [SerializeField, Tooltip("In-game display of timer")]
    private GameObject timerDisplay;
    [HideInInspector]
    public bool isPaused;
    public TimeBar timeBar;

    bool levelLoading = false;
    public GameObject winMenu;
    int currentScene;
    public AsyncOperation asyncOpertaion;
    static int totalAttempts;

    void Awake()
    {
        //DontDestroyOnLoad(this);
        
        GameObject[] objectivesArray = GameObject.FindGameObjectsWithTag("Objective");
        for(int i=0; i < objectivesArray.Length; i++)
        {
            objectives.Add(objectivesArray[i]);
        }
        _objectives = objectives;
        itemsRemaining = (objectives.Count-1);

        if (gameStateManager == null)
            gameStateManager = GameObject.Find("Game State Manager");
        if (levelData==null)
            levelData = GameObject.Find("Level Data");
        timer = levelData.GetComponent<LevelData>().levelTime;
        timeBar.SetMaxTime(timer);
        GameObject.Find("TotalItem").GetComponent<TextMeshProUGUI>().text = "\n" + "Remaining Items: " + itemsRemaining;
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
        
        //For when level is loading
        if(asyncOpertaion != null)
        {
            if (asyncOpertaion.progress >= 0.89)
            {
                winMenu.transform.GetChild(6).gameObject.SetActive(true);
            }
        }
    }

    public void CheckForCompletion() //Checks if Level is complete
    {
        GameObject.Find("TotalItem").GetComponent<TextMeshProUGUI>().text = "\n" + "Remaining Items: " + itemsRemaining;
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
        if (levelLoading)
            return;

        totalAttempts++;

        //Play Fail Animation

        SceneManager.LoadScene(currentScene);

        levelLoading = true;
    }

    public void LoadLevel(int sceneIndex)
    {
        if (levelLoading)
            return;

        totalAttempts++;
        isPaused = true;
        gameStateManager.SetActive(false);

        //Play Scene Transition

        print("Loading " + SceneManager.GetSceneByBuildIndex(sceneIndex).name);
        asyncOpertaion = SceneManager.LoadSceneAsync(sceneIndex);
        asyncOpertaion.allowSceneActivation = false;

        GameObject.Find("Player").GetComponent<topdown_Player>().enabled = false;
        winMenu.SetActive(true);
        winMenu.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = string.Format("{0:#.00}s", timer);
        winMenu.transform.GetChild(5).gameObject.GetComponent<TextMeshProUGUI>().text = totalAttempts.ToString();
        winMenu.transform.GetChild(6).gameObject.SetActive(false);
        
        levelLoading = true;
    }

    public void FinishLoad() //For use by Continue Button
    {
        totalAttempts = 0;
        asyncOpertaion.allowSceneActivation = true;
    }
}
