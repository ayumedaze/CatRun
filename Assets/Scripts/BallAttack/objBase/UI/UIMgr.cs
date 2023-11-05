using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public enum E_UI_layer
{
    bot,
    mid,
    top,
    system
}

public class UIMgr : MonoBehaviour
{
    public Dictionary<string,UIbase> panelDic = new Dictionary<string,UIbase>();
    public RectTransform Canvas;
    private Transform bot;
    private Transform mid;
    private Transform top;
    private Transform system;
    public UIMgr() 
    {
        GameObject obj = ResourcesMgr.Instance().Load<GameObject>("UI/Canvas");
        Canvas = obj.transform as RectTransform;
        GameObject.DontDestroyOnLoad(obj);
        bot = Canvas.Find("Bot");
        mid = Canvas.Find("Mid");
        top = Canvas.Find("Top");
        system = Canvas.Find("System");
        obj = ResourcesMgr.Instance().Load<GameObject>("UI/EventSystem");
        GameObject.DontDestroyOnLoad(obj);
    }
    public void ShowPanel<T>(string PanelName, E_UI_layer layer = E_UI_layer.mid,UnityAction<T> CallBack=null) where T : UIbase
    {
        if (panelDic.ContainsKey(PanelName))
        {
            panelDic[PanelName].ShowMe();
            CallBack(panelDic[PanelName] as T);
            return;
        }
        ResourcesMgr.Instance().LoadAsync<GameObject>("UI/" + PanelName, (obj) =>
        {
            Transform father = mid;
            switch (layer)
            {
                case E_UI_layer.bot:
                    father = bot;
                    break;
                case E_UI_layer.top:
                    father = top;
                    break;
                case E_UI_layer.system:
                    father = system;
                    break;
            }
            obj.transform.parent = father;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            (obj.transform as RectTransform).offsetMax = Vector2.zero;
            (obj.transform as RectTransform).offsetMin = Vector2.zero;
            T panel = obj.GetComponent<T>();
            if(CallBack != null)
                CallBack(panel);
            panel.ShowMe();
            panelDic.Add(PanelName, panel);
        });
    }
    public void HidePanel(string PanelName)
    {
        if (panelDic.ContainsKey(PanelName))
        {
            GameObject.Destroy(panelDic[PanelName].gameObject);
            panelDic.Remove(PanelName);
        }
    }
    public T GetPanel<T>(string PanelName)where T:UIbase
    {
        if (panelDic.ContainsKey(PanelName))
        {
            return panelDic[PanelName] as T;
        }
        return null;
    }
    public Transform GetLayerFather(E_UI_layer layer)
    {
        switch (layer)
        {
            case E_UI_layer.bot:
                return bot;
            case E_UI_layer.mid:
                return mid;
            case E_UI_layer.top:
                return top;
            case E_UI_layer.system:
                return system;
        }
        return null;
    }
    public static void AddCustomEventListener(UIBehaviour contral,EventTriggerType type,UnityAction<BaseEventData> CallBack)
    {
        EventTrigger trigger = contral.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = contral.gameObject.AddComponent<EventTrigger>();
        }
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener(CallBack);
        trigger.triggers.Add(entry);
    }
}
