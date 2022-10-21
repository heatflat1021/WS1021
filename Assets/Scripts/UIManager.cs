using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject sliderObj;

    // Start()��Update()�̗����ɋL�q���Ȃ������Ƃ��Ă������Ȃ��B
    // �����ƃC���X�y�N�^�ł̋��������������Ȃ�
    // Start is called before the first frame update
    void Start()
    {
        // Find�͂ł��邾���g��Ȃ��B
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
