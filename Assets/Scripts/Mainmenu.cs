using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Mainmenu : MonoBehaviour
{
    public GameObject levelE;
    public GameObject levelH;
    public GameObject main;
    public GameObject credit;
    public GameObject tutorial;
    public GameObject diff;
    // Start is called before the first frame update
    void Start()
    {
        main.SetActive(true);
        credit.SetActive(false);
        levelE.SetActive(false); 
        levelH.SetActive(false);
        diff.SetActive(false);
        tutorial.SetActive(false);
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
    public void level1H()
    {
        SceneManager.LoadScene("Level1H");
    }
    public void level2N()
    {
        SceneManager.LoadScene("Level2");
    }
    public void level2H()
    {
        SceneManager.LoadScene("Level2H");
    }
    public void level3N()
    {
        SceneManager.LoadScene("Level3");
    }
    public void level3H()
    {
        SceneManager.LoadScene("Level3H");
    }
    public void level4N()
    {
        SceneManager.LoadScene("Level4");
    }
    public void level4H()
    {
        SceneManager.LoadScene("Level4H");
    }
    public void level5N()
    {
        SceneManager.LoadScene("Level5");
    }
    public void level5H()
    {
        SceneManager.LoadScene("Level5H");
    }
    public void OnEasyClick()
    {


        diff.SetActive(false);
        levelE.SetActive(true);

    }
    public void OnHardClick()
    {


        diff.SetActive(false);
        levelH.SetActive(true);

    }


    public void OnlevelClick()
    {
        
        
        main.SetActive(false);
        diff.SetActive(true);
        
    }
    public void OnBackClickDF()
    {
        diff.SetActive(false);
        main.SetActive(true);
    }
    public void OncreditClick()
    {


        main.SetActive(false);
        credit.SetActive(true);

    }

    public void OntutorClick()
    {


        main.SetActive(false);
        tutorial.SetActive(true);

    }

    public void OnBackClickLVE()
    {
        levelE.SetActive(false);
        diff.SetActive(true);
    }
    public void OnBackClickLVH()
    {
        levelH.SetActive(false);
        diff.SetActive(true);
    }
    public void OnBackClickCD()
    {
        credit.SetActive(false);
        main.SetActive(true);
    }
    public void OnBackClickTT()
    {
        tutorial.SetActive(false);
        main.SetActive(true);
    }
}
