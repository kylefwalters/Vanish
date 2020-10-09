using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> objectives = new List<GameObject>();
    private List<GameObject> _objectives = new List<GameObject>();

    private GameObject levelData;
    private float timer;

    int currentScene;

    void Start()
    {
        DontDestroyOnLoad(this);

        GameObject[] objectivesArray = GameObject.FindGameObjectsWithTag("Objecive");
        for(int i=0; i < objectivesArray.Length; i++)
        {
            objectives.Add(objectivesArray[i]);
        }
        _objectives = objectives;

        levelData = GameObject.Find("Level Data");
        timer = levelData.GetComponent<LevelData>().levelTime;

        currentScene = SceneManager.GetActiveScene().buildIndex; //Grabs current scene's index
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
            onLevelFail();
    }

    public void CheckForCompletion() //Checks if Level is complete
    {
        if (_objectives.Count == 0)
        {
            LoadLevel(currentScene + 1);
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

        SceneManager.LoadSceneAsync(sceneIndex);
    }
}
