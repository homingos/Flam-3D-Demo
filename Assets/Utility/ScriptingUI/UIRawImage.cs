using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIRawImage : UIElements{
    RawImage image;
    Texture texture ;

    // RectTransform imagerect;
    public UIRawImage(string name, MarginType type,Vector2 mar,Vector2 size, Texture texture):base (name, type, mar, size)
    {
        this.texture = texture;
    }

    public override void addToPage()
    {
        holder = new GameObject();
        holder.AddComponent<CanvasRenderer>();
        rect = holder.AddComponent<RectTransform>();
        image = holder.AddComponent<RawImage>();
        image.color=Color.white;
        if(texture!=null)
        image.texture=texture;
        base.addToPage();
    }

    
    
    void updateTexture(Texture tex){
        texture=tex;
        image.texture=tex;
    }
}