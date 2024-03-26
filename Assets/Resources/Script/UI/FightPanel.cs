using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightPanel : BasePanel
{
    // �󶨽ڵ�
    public Transform StatusBar;
    public Transform SettingBin;
    public Transform ComboArea;
    public Transform SkillBar;
    public Transform SP;
    public Transform BottomHpBar;
    public Transform ShieldBar;

    private void Awake()
    {
        // ���ҽڵ�λ��
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
        // ע�����¼�
        SettingBin.GetComponent<Button>().onClick.AddListener(OnSettingBin);


    }

    // ��ť����¼�
    public void OnSettingBin()
    {
        UIManager.Instance.OpenPanel(UIConst.SettingUI);
    }

}
