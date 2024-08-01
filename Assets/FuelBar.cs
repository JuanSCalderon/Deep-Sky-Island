using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{
    private Slider slider;
    
    private void Start()
    {
        InitializeSlider();
        SetFuel(0);
    }

    private void InitializeSlider()
    {
        if (slider == null)
        {
            slider = GetComponent<Slider>();
        }
    }

    public void SetMaxFuel(float fuelMax)
    {
        InitializeSlider();
        slider.maxValue = fuelMax;
    }

    public void SetFuel(float fuel)
    {
        InitializeSlider();
        slider.value = fuel;
    }

    public void AddFuel(float amount)
    {
        InitializeSlider();
        slider.value = Mathf.Min(slider.value + amount, slider.maxValue);
    }
    
}
