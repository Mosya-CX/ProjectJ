using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UIManager : SingletonWithMono<UIManager>
{

    public Transform UI;// ��ui����
    public List<BasePanel> UIList;// ui����洢

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
                Debug.LogWarning("��UIʧ��:" + uiName);
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
            Debug.LogWarning("δ��" + uiName + "���棬�޷��ر�");
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
    public const string CreditsUI = "CreditsPanel";// ��������
    public const string SettingUI = "SettingPanel";
    public const string StoryUI = "StoryTellingPanel";
    public const string DeadUI = "DeadPanel";
}