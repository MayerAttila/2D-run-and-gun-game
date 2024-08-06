using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectingStats : MonoBehaviour
{
    public Slider slider;
    public SkillpointCounter skillpointCounterScript;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponentInChildren<Slider>();
    }

    public void IncreeseValue() 
    {
        if (slider.value < slider.maxValue && skillpointCounterScript.skillPoints > 0) 
        {
            slider.value++;
            skillpointCounterScript.skillPoints--;
            skillpointCounterScript.RefreshText();
        } 
        
    }

    public void DecreeseValue()
    {
        if (slider.value > slider.minValue && skillpointCounterScript.skillPoints < 10) 
        {
            slider.value--;
            skillpointCounterScript.skillPoints++;
            skillpointCounterScript.RefreshText();
        } 
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
