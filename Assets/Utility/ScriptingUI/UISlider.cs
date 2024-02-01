using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UISlider : UIElements
{
    Slider slider;
    UnityAction<float> onChanged;
    float defaultValue=0;

    public UISlider(string name, MarginType marginType, Vector2 margin, Vector2 size, UnityAction<float> onChanged,float defaultVal =0):base(name, marginType, margin,size)
    {
        this.onChanged =onChanged;
        defaultValue=defaultVal;
    }

    public override void addToPage()
    {
        holder = new GameObject();
        holder.AddComponent<CanvasRenderer>();
        holder.AddComponent<RectTransform>();
        holder = GameObject.Instantiate(Resources.Load<GameObject>("Slider"));
        slider = holder.GetComponent<Slider>();
        slider.value=defaultValue;
        slider.onValueChanged.AddListener(onChanged);
        rect = holder.GetComponent<RectTransform>();
        base.name =name;    
        holder.name=name;
        base.addToPage();
    }
    public void setDefaultValue(float value=0){
        slider.value = value;
    }

    
}
