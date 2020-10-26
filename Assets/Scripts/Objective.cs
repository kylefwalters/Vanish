using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    private GameObject levelManager;

    private void Start()
    {
        levelManager = GameObject.Find("Level Manager");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(this);
            gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        levelManager.GetComponent<LevelManager>().CheckForCompletion();
    }
}
