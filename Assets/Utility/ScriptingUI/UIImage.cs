using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class UIImage:UIElements{
    Sprite? sprite;
    protected Image image;
   
    public UIImage(string name,MarginType margintype, Vector2 margin,Vector2 size,Sprite? sprite=null):base(name,margintype,margin,size)
        {
            this.sprite=sprite;

        }

    public override void addToPage()
    {
        holder =new GameObject();
        holder.AddComponent<CanvasRenderer>();
        rect = holder.AddComponent<RectTransform>();
        // Debug.Log(rect);
        image = holder.AddComponent<Image>();
        image.color=Color.white;
        if(sprite!=null)
        image.sprite=sprite;
        base.addToPage();

    }

}

public class UILoaderCircle:UIImage{
public UILoaderCircle(string name,MarginType margintype, Vector2 margin,Vector2 size,Sprite? sprite=null):base(name,margintype,margin,size,sprite)
        {
            

        }

    public override void addToPage()
    {
        base.addToPage();
        image.type= Image.Type.Filled;

    }

    public void UpdateLoader(float completeNormalized){
        //Todo
        //normalize 0-1 
        image.fillAmount =completeNormalized;

    }

}


