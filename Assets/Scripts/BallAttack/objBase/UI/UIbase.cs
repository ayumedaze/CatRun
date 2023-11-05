using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIbase : MonoBehaviour
{
    private Dictionary<string, List<UIBehaviour>> controlDic = new Dictionary<string, List<UIBehaviour>>();
    protected virtual void Awake()
    {
        FindChildControl<Button>();
        FindChildControl<Image>();
        FindChildControl<Text>();
        FindChildControl<Toggle>();
        FindChildControl<Slider>();
        FindChildControl<ScrollRect>();
        FindChildControl<InputField>();
    }
    /// <summary>
    /// 用 switch 判断按钮名字 对应添加响应事件
    /// </summary>
    /// <param name="ButName">按钮名</param>
    protected void ButOnClick(string ButName)
    {

    }
    /// <summary>
    /// 用 switch 判断单选或多选名字 对应添加响应事件
    /// </summary>
    /// <param name="TogName">单选或多选名字</param>
    /// <param name="value">是否被选中</param>
    protected void TogOnValueChanged(string TogName,bool value)
    {

    }
    private void FindChildControl<T>() where T : UIBehaviour
    {
        T[] controls = GetComponentsInChildren<T>();
        for (int i = 0; i < controls.Length; i++)
        {
            string objName = controls[i].gameObject.name;
            if (controlDic.ContainsKey(objName))
            {
                controlDic[objName].Add(controls[i]);
            }
            else
            {
                controlDic.Add(objName, new List<UIBehaviour>() { controls[i] });
            }
            if (controls[i] is Button)
            {
                (controls[i] as Button).onClick.AddListener(() =>
                {
                    ButOnClick(objName);
                });
            }
            else if (controls[i] is Toggle)
            {
                (controls[i] as Toggle).onValueChanged.AddListener((value) =>
                {
                    TogOnValueChanged(objName, value);
                });
            }
        }
    }
    protected T GetControl<T>(string controlName) where T : UIBehaviour
    {
        if (controlDic.ContainsKey(controlName))
        {
            for (int i = 0; i < controlDic[controlName].Count; i++)
            {
                if (controlDic[controlName][i] is T)
                {
                    return controlDic[controlName][i] as T;
                }
            }
        }
        return null;
    }
    public virtual void ShowMe()
    {

    }
    public virtual void HideMe()
    {

    }
}
