using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class  UIText:UIElements{

    Text text;
     Color? color ;
    Font? font;
    string txt;
    int fontSize;
    bool bestfit=true;
    TextAnchor orientation=TextAnchor.MiddleCenter;

    public UIText(string name,MarginType type,Vector2 mar,Vector2 size,string txt,Font? font=null,Color? color=null,int fontSize=14,bool bestfit=true,TextAnchor ori=TextAnchor.MiddleCenter):base( name, type, mar, size){
             this.font =font;
            this.fontSize=fontSize;
            this.txt =txt;
            this.color= color;
            this.bestfit =bestfit;
            orientation=ori;
            
    }

    public void UpdateText(string txt){
            text.text=txt;
    }

    public override void addToPage(){
        holder =new GameObject();
        rect = holder.AddComponent<RectTransform>();
        holder.AddComponent<CanvasRenderer>();
        text = holder.AddComponent<Text>();
        text.text=txt;
        text.alignment=orientation;
        text.color =color?? Color.black;
        text.font = font?? Font.CreateDynamicFontFromOSFont("Arial",fontSize);
        text.fontSize = fontSize;
        text.resizeTextForBestFit=bestfit;
        text.rectTransform.sizeDelta =size;
        base.addToPage();
    }
}