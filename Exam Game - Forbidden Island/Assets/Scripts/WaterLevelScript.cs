using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterLevelScript : MonoBehaviour
{

   

    [SerializeField]
    private int currentWaterLevel = 0;

    public Slider waterLevelSlider;

    private void Start()
    {
        
        UpdateSlider();

        waterLevelSlider.onValueChanged.AddListener(OnWaterLevelSliderValueChanged);
    }

    

    public void SetWaterLevel(float level)
    {
 
       
        UpdateSlider();
    }

    public void IncreaseWaterLevel(int amount)
    {
        currentWaterLevel += amount;
        UpdateSlider();
    }

    private void UpdateSlider()
    {
        waterLevelSlider.value = currentWaterLevel;
        
    }

    private void OnWaterLevelSliderValueChanged(float value)
    {
        currentWaterLevel = Mathf.RoundToInt(value);
        
    }


}
