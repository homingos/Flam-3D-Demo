
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIPanel:UIElements{
    Sprite? sprite;
    // GameObject panel;
    public UIPanel(string name,MarginType margintype, Vector2 margin,Vector2 size,Sprite? sprite=null):base(name,margintype,margin,size)
        {
            this.sprite=sprite;

        }

    public override void addToPage()
    {
        holder =new GameObject();
        holder.AddComponent<CanvasRenderer>();
        rect = holder.AddComponent<RectTransform>();
        // Debug.Log(rect);
        var button = holder.AddComponent<Image>();
        button.color=Color.black;
        if(sprite!=null)
        button.sprite=sprite;
        base.addToPage();

    }

}
