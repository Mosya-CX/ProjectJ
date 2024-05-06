using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UIManager : SingletonWithMono<UIManager>
{

    public Transform UI;// 绑定ui画布
    public List<BasePanel> UIList;// ui界面存储

    protected override void Awake()
    {
        if (UI == null)
        {
            UI = GameObject.Find("UI").transform;
        }
        UIList = new List<BasePanel>();
    }

    private void Start()
    {
        
    }

    public BasePanel OpenPanel(string uiName) 
    {
        BasePanel panel = FindPanel(uiName);

        if (panel == null)
        {
            GameObject obj = GameObject.Instantiate(Resources.Load("Prefab/UI/" + uiName), UI) as GameObject;

            panel = obj.GetComponent<BasePanel>();
            UIList.Add(panel);
            
            if (panel == null)
            {
                Debug.LogWarning("打开UI失败:" + uiName);
                return panel;
            }
        }
        panel.Open(uiName);
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
    public bool isAvailable(string uiName)
    {
        BasePanel panel = FindPanel(uiName);
        if (panel == null)
        {
            return false;
        }
        if (panel.gameObject.activeSelf)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}

public class UIConst
{
    public const string LoginUI = "LoginPanel";
    public const string FightUI = "FightPanel";
    public const string CreditsUI = "CreditsPanel";// 制作名单
    public const string SettingUI = "SettingPanel";
    public const string StoryUI = "StoryTellingPanel";
    public const string DeadUI = "DeadPanel";
}