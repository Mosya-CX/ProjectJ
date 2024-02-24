using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Transform UI;// 绑定ui画布
    List<BasePanel> UIList;// ui界面存储

    private void Start()
    {
        if (UI == null)
        {
            UI = GameObject.Find("UI").transform;
        }
        UIList = new List<BasePanel>();
    }

    public BasePanel OpenPanel<T>(string uiName) where T : BasePanel
    {
        BasePanel panel = FindPanel(uiName);

        if (panel != null)
        {
            panel.Open();
        }
        else
        {
            GameObject obj = GameObject.Instantiate(Resources.Load("Prefab/UI/"+uiName), UI) as GameObject;
            obj.name = uiName;
            panel = obj.GetComponent<T>();
            UIList.Add(panel);
        }

        return panel;
    }

    public void ClosePanel(string uiName)
    {
        BasePanel panel = FindPanel(uiName);
        
        if (panel != null)
        {
            panel.Close();   
        }
    }

    public BasePanel FindPanel(string uiName)
    {
        for (int i = 0; i < UIList.Count; i++)
        {
            if (UIList[i].name == uiName)
            {
                return UIList[i];
            }
        }
        return null;
    }

}
