using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIBase {

}

public class Page :UIBase{
    public Canvas canvas;
    public RectTransform rect;
    string name;
    public static Page current;
    public Vector2 referenceResolution = new Vector2(800,600); 
    Dictionary<string,object> elements = new Dictionary<string, object>();


    public Page(string name){
        var go =new GameObject();
        go.name = name;
        this.name = name;
        canvas = go.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        var scalar =go.AddComponent<CanvasScaler>();
        scalar.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scalar.referenceResolution=referenceResolution;
        scalar.screenMatchMode=CanvasScaler.ScreenMatchMode.Expand;
        var graphics = go.AddComponent<GraphicRaycaster>();
        graphics.ignoreReversedGraphics=true;
        canvas.gameObject.SetActive(false);
        rect  = canvas.gameObject.GetComponent<RectTransform>();
        Debug.Log(name+rect.rect.width);

    }
    public Page(string name,List<UIElements> elements):this(name){
        foreach(var e in elements){
            addElement(e);
        }
    }
    public Page(string name,params UIElements[] elements):this(name){
        foreach(var e in elements){
            addElement(e);
        }
    }
    
    public  static void redirect(ref Page page){
        if(current !=null) deactivate(ref current);
        try {page.canvas.gameObject.SetActive(true);
        current=page;
        }catch(System.Exception e){
            Debug.Log("cant redirect "+ e.Message);
        }
    }

    public static void deactivate(ref Page page){
        try{page.canvas.gameObject.SetActive(false);}catch{}
    }
    //add elements to page
    public void addElement<T>(T element) where T:UIElements{
        // elements.Add(element.name,element);
        element.page =this;
        
        element.addToPage();
    }
    
}
public class UIElements:UIBase{
    public enum MarginType{
        Middle,
        Top,
        Bottom,
        Left,
        Right,
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

    public GameObject holder{get; protected set;}
   
    public Page page{get;set;}
    public string name;
    protected Vector2 margin;
    protected Vector2 position;
    protected Vector2 size;
    Vector2 anchorPos;
    MarginType marginType;
    //private shit
    protected RectTransform rect;

    public virtual void Init(){
        anchorPos  = new Vector2(0.5f,0.5f);
        // Vector2 pivotPos;
        switch(marginType){
            case MarginType.Middle:
                anchorPos = new Vector2(0.5f,0.5f);
                break;
            case MarginType.Top:
                anchorPos = new Vector2(0.5f,1f);
                break;
            case MarginType.Bottom:
                anchorPos = new Vector2(0.5f,0f);
                break;
            case MarginType.Left:
                anchorPos = new Vector2(0f,0.5f);
                break;
            case MarginType.Right:
                anchorPos = new Vector2(1f,0.5f);
                break;
            case MarginType.TopLeft:
                anchorPos = new Vector2(0f,1f);
                break;
            case MarginType.TopRight:
                anchorPos = new Vector2(1f,1f);
                break;
            case MarginType.BottomLeft:
                anchorPos = new Vector2(0f,0f);
                break;
            case MarginType.BottomRight:
                anchorPos = new Vector2(1f,0f);
                break;
        }

        
    }

     
    public UIElements(string name,MarginType type,Vector2 mar,Vector2 size){
        marginType = type;
        this.margin = mar;
        this.size=size;
        
        this.name =name;
        Init();
    }
    public void deactivate(){
        holder.SetActive(false);
    }
    public void activate(){
        holder.SetActive(true);
    }
    public virtual void addToPage(){
        holder.transform.SetParent(page.canvas.transform,false);
        rect.pivot =anchorPos;
        if(size == Vector2.zero)
       { rect.sizeDelta=page.rect.sizeDelta;}
        else if(size.x == 0)
        {
        rect.sizeDelta = new Vector2(page.referenceResolution.x,size.y*page.referenceResolution.x);
        }
        
        else
        rect.sizeDelta = size;
        rect.anchorMax=anchorPos;
        rect.anchorMin =anchorPos;
        holder.name =name;
        if(holder.GetComponent<CanvasRenderer>()==null)
        holder.AddComponent<CanvasRenderer>();
        rect.anchoredPosition = margin;
        
        
    }
    
    
}