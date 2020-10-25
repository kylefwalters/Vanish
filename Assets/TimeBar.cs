using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    //public float time;

    public void SetMaxTime(float time)
    {
        slider.maxValue = time;
        slider.value = time;
    }
    public void SetTime(float time)
    {
        slider.value = time;
    }
}
