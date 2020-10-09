using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> objectives = new List<GameObject>();
    private List<GameObject> _objectives = new List<GameObject>();

    void Start()
    {
        DontDestroyOnLoad(this);

        GameObject[] objectivesArray = GameObject.FindGameObjectsWithTag("Objecive");
        for(int i=0; i < objectivesArray.Length; i++)
        {
            objectives.Add(objectivesArray[i]);
        }
        _objectives = objectives;
    }

    public void CheckForCompletion()
    {
        if (_objectives.Count == 0)
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;

            LoadLevel(currentScene + 1);
        }
    }

    public void LoadLevel(int sceneIndex)
    {
        //Play Scene Transition

        SceneManager.LoadSceneAsync(sceneIndex);
    }
}
