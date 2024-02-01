using UnityEngine;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine.UI;
using UnityEngine.Events;


public class  UITextBg:UIElements{

    Text text;
     Color? color ;
    Font? font;
    string txt;
    int fontSize;
    Sprite sprite;
    Image image;
    RectTransform imagerect;
    bool isBestFit = true;

    public UITextBg(string name,MarginType margintype, Vector2 margin,Vector2 size,string text="",Color? color = null,Font? font=null,int fontSize = 14,Sprite? sprite=null, bool isBestFit = true):base(name,margintype,margin,size)
        {
            this.font =font;
            this.fontSize=fontSize;
            this.sprite=sprite;
            this.color =color;
            this.txt=text;
        this.isBestFit = isBestFit;
            
        }

    public void UpdateText(string txt){
            text.text=txt;
    }

    public override void addToPage(){
        holder =new GameObject();
        var go = new GameObject();
        imagerect = go.AddComponent<RectTransform>();
        
        go.AddComponent<CanvasRenderer>();
        text = go.AddComponent<Text>();
        
        text.text=txt;
        text.alignByGeometry = true;
        text.alignment=TextAnchor.MiddleCenter;
        text.color =color?? Color.black;
        text.font = font?? Font.CreateDynamicFontFromOSFont("Arial",fontSize);
        text.fontSize = fontSize;
        text.resizeTextForBestFit=isBestFit;
        text.rectTransform.sizeDelta =size;
         if(sprite!=null)
        {
            
            go.transform.parent = holder.transform;
            rect=holder.AddComponent<RectTransform>();
            holder.AddComponent<CanvasRenderer>();
            image =holder.AddComponent<Image>();
            image.sprite=sprite;
            imagerect.sizeDelta = size;
            
            
        }
        base.addToPage();
        
    }
}

public class UIToast : UITextBg
{
    float TimeDelay=2f;
    public UIToast(string name, MarginType margintype, Vector2 margin, Vector2 size, string text = "",float timeTostay=5f, Color? color = null, Font? font = null, int fontSize = 30, Sprite? sprite = null,bool isBestfit=false)
        : base(name, margintype, margin, size, text, color, font, fontSize, sprite,isBestFit:isBestfit)
    {
        TimeDelay = timeTostay;
    }
    public override void addToPage()
    {
        base.addToPage();
        
        holder.SetActive(false);
    }

    public async  void showToast()
    {
        if(holder.activeSelf) return;
        holder.SetActive(true);
        await Task.Delay((int)TimeDelay * 1000);
        holder.SetActive(false);

    }
}