using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIButton:UIElements{
        Button button;
        UnityAction onClick;
        Text text;
        Color? color ;
        Font? font;
        Image image;
        Sprite? sprite;
        int fontSize;
        string placeHolder;
        public UIButton(string name,MarginType margintype, Vector2 margin,Vector2 size,UnityAction onclick,string text="",Color? color = null,Font? font=null,int fontSize = 14,Sprite? sprite=null):base(name,margintype,margin,size)
        {
            this.font =font;
            this.fontSize=fontSize;
            this.sprite=sprite;
            this.color =color;
            placeHolder=text;
            this.onClick = onclick;
        }
    public override void addToPage()
    {
        holder =new GameObject();
        holder.AddComponent<CanvasRenderer>();
        button = holder.AddComponent<Button>();
        rect = holder.AddComponent<RectTransform>();

        if(placeHolder !="")
        {var go = new GameObject();
        go.transform.parent = button.transform;
        go.AddComponent<RectTransform>();
        go.gameObject.AddComponent<CanvasRenderer>();
        text=go.AddComponent<Text>();
        text.text=placeHolder;
        text.alignment=TextAnchor.MiddleCenter;
        text.alignByGeometry = true;
        text.color =color?? Color.black;
        text.font = font?? Font.CreateDynamicFontFromOSFont("Arial",fontSize);
        text.fontStyle = FontStyle.Bold;
        text.fontSize = fontSize;
        text.resizeTextForBestFit=true;
        text.rectTransform.sizeDelta =size;
       
        }
        image =holder.AddComponent<Image>();
        if(sprite!=null)
        image.sprite=sprite;   
        button.onClick.AddListener(onClick);
        base.name =name;    
        holder.name=name;
        base.addToPage();
    }

}