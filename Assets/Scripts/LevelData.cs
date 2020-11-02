using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    [Tooltip("The Time Limit for the Level")]
    public float levelTime;
    [Tooltip("The build index for the next level; if null will automatically select (current scene + 1)")]
    public int nextLevel=-1;
}
