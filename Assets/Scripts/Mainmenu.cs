using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Mainmenu : MonoBehaviour
{
    public GameObject level;
    public GameObject main;
    public GameObject credit;
    // Start is called before the first frame update
    void Start()
    {
        main.SetActive(true);
        credit.SetActive(false);
        level.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void gamestart()
    {
        SceneManager.LoadScene("Level1");
    }
    public void level1N()
    {
        SceneManager.LoadScene("Level1");
    }
    public void level2N()
    {
        SceneManager.LoadScene("Level2");
    }
    public void level3N()
    {
        SceneManager.LoadScene("Level3");
    }
    public void level4N()
    {
        SceneManager.LoadScene("Level4");
    }
    public void level5N()
    {
        SceneManager.LoadScene("Level5");
    }
    public void OnlevelClick()
    {
        
        
        main.SetActive(false);
        level.SetActive(true);
        
    }
    public void OncreditClick()
    {


        main.SetActive(false);
        credit.SetActive(true);

    }

    public void OnBackClickLV()
    {
        level.SetActive(false);
        main.SetActive(true);
    }
    public void OnBackClickCD()
    {
        credit.SetActive(false);
        main.SetActive(true);
    }
}
