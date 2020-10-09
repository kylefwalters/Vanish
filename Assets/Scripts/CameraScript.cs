using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [Header("Camera Movement")]
    public Transform target;
    [Range(0.0f,1.0f)]
    public float trackSpeed = 0.5f;
    private Vector2 velocity;

    private float targetOrtho;
    private float velOrtho;
    [Header("Zoom")]
    public int maxScreenSize = 23;
    public int minScreenSize = 3;

    private void Awake()
    {
        targetOrtho = Camera.main.orthographicSize;

        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        float invTrackSpeed = 1.01f - (trackSpeed);
        Vector2 targetPos = Vector2.SmoothDamp(transform.position, target.position, ref velocity, invTrackSpeed);
        transform.position = new Vector3(targetPos.x, targetPos.y, -10);

        //Smooths Camera Zoom
        Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, targetOrtho, ref velOrtho, 0.1f);
    }

    private void Zoom()
    {
        //Zoom Out
        if (Input.GetKeyDown(KeyCode.R) || Input.mouseScrollDelta.y > 0)
        {
            if (Camera.main.orthographicSize <= 23)
                targetOrtho += 4;
        }

        //Zoom In
        if (Input.GetKeyDown(KeyCode.F) || Input.mouseScrollDelta.y < 0)
        {
            if (Camera.main.orthographicSize >= 3)
                targetOrtho -= 4;
        }
    }
}
