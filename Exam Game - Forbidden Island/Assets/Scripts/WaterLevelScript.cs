using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterLevelScript : MonoBehaviour
{

    public static UI uiInstance;

    [SerializeField]
    private int currentWaterLevel = 0;

    public Slider waterLevelSlider;


    public delegate void WaterLevelChangedHandler(int waterLevel);
    public event WaterLevelChangedHandler OnWaterLevelChanged;


    private void Start()
    {
        UpdateSlider();

        waterLevelSlider.onValueChanged.AddListener(OnWaterLevelSliderValueChanged);
    }

    public int GetWaterLevel()
    {
        return currentWaterLevel;
    }



    public void SetWaterLevel(float level)
    {
        currentWaterLevel = Mathf.RoundToInt(level);
        UpdateSlider();
        NotifyWaterLevelChanged();



    }

    public void IncreaseWaterLevel(int amount)
    {
        currentWaterLevel += amount;
        UpdateSlider();
        NotifyWaterLevelChanged();

    }

    private void UpdateSlider()
    {
        waterLevelSlider.value = currentWaterLevel;
        UI.uiInstance.WaterDisplay(currentWaterLevel);

    }

    private void OnWaterLevelSliderValueChanged(float value)
    {
        currentWaterLevel = Mathf.RoundToInt(value);
        NotifyWaterLevelChanged();

    }

    public void NotifyWaterLevelChanged()
    {
        OnWaterLevelChanged?.Invoke(currentWaterLevel);
    }


}
