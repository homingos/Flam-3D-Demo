using UnityEngine;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIToggle:UIElements{

    Sprite trueSprite;
    Sprite falseSprite;
    protected Image image;
    Button button;
    UnityAction<bool> onToggle;
    public bool on{get;protected set;}=false;
    public UIToggle(string name,MarginType marginType,Vector2 margin,Vector2 Size, Sprite trueSprite,Sprite falseSprite,UnityAction<bool> onToggle,bool init=false):
    base(name,marginType,margin,Size){
        this.trueSprite=trueSprite;
        this.falseSprite=falseSprite;
        on=init;
        this.onToggle =onToggle;
    }

     public override void addToPage()
    {
        holder =new GameObject();
        holder.AddComponent<CanvasRenderer>();
        button = holder.AddComponent<Button>();
        rect = holder.AddComponent<RectTransform>();

        // Debug.Log(rect);
        image = holder.AddComponent<Image>();
        image.color=Color.white;
        if(on)image.sprite=trueSprite;else image.sprite=falseSprite;
        button.onClick.AddListener(onclick);
        base.name =name;    
        holder.name=name;
        base.addToPage();
       }

    void toggleState(bool val){
        on=val;
        if(on)image.sprite=trueSprite;else image.sprite=falseSprite;
    }

    void onclick(){
        toggleState(!on);
        onToggle(on);
    }

    
}
