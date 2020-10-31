using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Objective : MonoBehaviour
{
    [Tooltip("Items that have to be collected before this item can be collected")]
    public GameObject[] prerequisiteItems;
    public GameObject levelManager;

    private void Start()
    {
        Color color = GetComponent<SpriteRenderer>().color;
        if (prerequisiteItems.Length > 0)
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
            foreach(GameObject item in prerequisiteItems)
            {
                if (item.activeInHierarchy == true)
                {
                    return;
                }
            }
            Destroy(this);
            gameObject.SetActive(false);
        }
    }

    public void CheckPrereqs()
    {
        foreach (GameObject item in prerequisiteItems)
        {
            if (item.activeInHierarchy == true)
            {
                return;
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
        if(levelManager!=null)
            levelManager.GetComponent<LevelManager>().CheckForCompletion();
    }
}
