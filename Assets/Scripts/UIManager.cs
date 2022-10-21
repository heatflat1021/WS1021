using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject sliderObj;

    // Start()とUpdate()の両方に記述がなかったとしても消さない。
    // 消すとインスペクタでの挙動がおかしくなる
    // Start is called before the first frame update
    void Start()
    {
        // Findはできるだけ使わない。
        // sliderObj = GameObject.Find("Canvas/Panel/Slider");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddProgress(float incremental)
    {
        Slider slider = sliderObj.GetComponent<Slider>();

        float nextSliderValue = Clip(slider.value + incremental, 0, 1);

        slider.value = nextSliderValue;
    }

    private float Clip(float value, float min, float max)
    {
        float clippedValue = value;
        if (clippedValue > max)
        {
            clippedValue = max;
        }
        else if (clippedValue < min)
        {
            clippedValue = min;
        }
        return clippedValue;
    }
}
