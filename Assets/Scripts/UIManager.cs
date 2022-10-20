using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject sliderObj;

    // Start is called before the first frame update
    void Start()
    {
        sliderObj = GameObject.Find("Canvas/Panel/Slider");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddProgress(float incremental)
    {
        Slider slider = sliderObj.GetComponent<Slider>();
        float nextSliderValue = slider.value + incremental;

        if (nextSliderValue > 1)
        {
            nextSliderValue = 1;
        }
        else if (nextSliderValue < 0)
        {
            nextSliderValue = 0;
        }

        slider.value = nextSliderValue;
    }
}
