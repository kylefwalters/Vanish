using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
public class Objective : MonoBehaviour
{
    [Tooltip("Items that have to be collected before this item can be collected")]
    public GameObject[] prerequisiteItems;
    public GameObject levelManager;

    private void Start()
    {
        Color color = GetComponent<SpriteRenderer>().color;
        if (prerequisiteItems!=null)
        {
            GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0.4f);
            if (transform.childCount > 0)
            {
                SpriteRenderer[] childSprites = GetComponentsInChildren<SpriteRenderer>(true);
                for(int i=0; i < transform.childCount; i++)
                {
                    childSprites[i].color = new Color(color.r, color.g, color.b, 0.4f);
                }
            }
        }
        InvokeRepeating("CheckPrereqs", 0.0f, 0.05f);

        levelManager = GameObject.Find("Level Manager");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (prerequisiteItems != null)
            {
                foreach (GameObject item in prerequisiteItems)
                {
                    if (item != null)
                    {
                        if (item.activeInHierarchy == true)
                        {
                            return;
                        }
                    }
                }
            }
            Destroy(this);
            gameObject.SetActive(false);
        }
    }

    public void CheckPrereqs()
    {
        if (prerequisiteItems != null)
        {
            foreach (GameObject item in prerequisiteItems)
            {
                if (item.activeInHierarchy == true)
                {
                    return;
                }
            }
        }
        Color color = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 1.0f);
        if (transform.childCount > 0)
        {
            SpriteRenderer[] childSprites = GetComponentsInChildren<SpriteRenderer>(true);
            for (int i = 0; i < transform.childCount; i++)
            {
                childSprites[i].color = new Color(color.r, color.g, color.b, 1.0f);
            }
        }

        CancelInvoke("CheckPrereqs");
    }

    private void OnDestroy()
    {
        if (levelManager != null)
        {
            if (gameObject.name.Length >= 5)
            {
                if (gameObject.name.ToLower().Substring(0, 5) != "arrow")
                    levelManager.GetComponent<LevelManager>().itemsRemaining--;
            }
            else
                levelManager.GetComponent<LevelManager>().itemsRemaining--;
            levelManager.GetComponent<LevelManager>().CheckForCompletion();
        }
    }
}
