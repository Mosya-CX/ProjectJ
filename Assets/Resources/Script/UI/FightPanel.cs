using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightPanel : BasePanel
{
    // 绑定节点
    public Transform StatusBar;
    public Transform SettingBin;
    public Transform ComboArea;
    public Transform SkillBar;
    public Transform SP;
    public Transform BottomHpBar;
    public Transform ShieldBar;

    private void Awake()
    {
        // 查找节点位置
        StatusBar = transform.Find("Top/StatusBar");
        SettingBin = transform.Find("Top/Setting/Bin");
        ComboArea = transform.Find("Middle/ComboArea");
        SkillBar = transform.Find("Middle/SkillBar");
        SP = transform.Find("Bottom/SP");
        BottomHpBar = transform.Find("Bottom/HpBar");
        ShieldBar = transform.Find("Bottom/ShieldBar");
    }

    private void Start()
    {
        // 注册点击事件
        SettingBin.GetComponent<Button>().onClick.AddListener(OnSettingBin);


    }

    // 按钮点击事件
    public void OnSettingBin()
    {
        UIManager.Instance.OpenPanel(UIConst.SettingUI);
    }

}
