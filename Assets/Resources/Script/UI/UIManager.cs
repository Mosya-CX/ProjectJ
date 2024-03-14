using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Transform UI;// 绑定ui画布
    List<BasePanel> UIList;// ui界面存储

    private void Awake()
    {
        Instance = this;
        if (UI == null)
        {
            UI = GameObject.Find("UI").transform;
        }
    }

    private void Start()
    {
        
        UIList = new List<BasePanel>();
    }

    public BasePanel OpenPanel(string uiName) 
    {
        BasePanel panel = FindPanel(uiName);

        if (panel != null)
        {
            panel.Open(uiName);
        }
        else
        {
            GameObject obj = GameObject.Instantiate(Resources.Load("Prefab/UI/"+uiName), UI) as GameObject;
            
            panel = obj.GetComponent<BasePanel>();
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
        else
        {
            Debug.LogWarning("未打开" + uiName + "界面，无法关闭");
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

public class UIConst
{
    public const string LoginUI = "LoginPanel";
    public const string FightUI = "FightPanel";
    public const string CreditsUI = "CreditsPanel";
    public const string SettingUI = "SettingPanel";
}