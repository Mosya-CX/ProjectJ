using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FightPanel : BasePanel
{
    // �󶨽ڵ�
    public Transform StatusBar;
    public Transform SettingBin;
    public Transform ComboArea;
    public Transform SkillBar;
    public Transform SpBar;
    public Transform ShieldBar;
    public Transform SkillFields;
    public Slider BottomHpBar;
    public Slider TopHpBar;
    public TextMeshProUGUI ComboText;
    public Transform BottomSpBar;
    public Transform BottomShieldBar;
    // ������Ϣ����
    public PlayerController playerData;
    public int ShieldCount;
    public int SpCount;

    private void Awake()
    {
        // ���ҽڵ�λ��
        StatusBar = transform.Find("Top/StatusBar");
        SettingBin = transform.Find("Top/Setting/Bin");
        ComboArea = transform.Find("Middle/ComboArea");
        SkillBar = transform.Find("Middle/SkillBar");
        SpBar = transform.Find("Bottom/SpBar");
        BottomHpBar = transform.Find("Bottom/HpBar").GetComponent<Slider>();
        TopHpBar = StatusBar.Find("HpBar").GetComponent <Slider>();
        ShieldBar = transform.Find("Bottom/ShieldBar");
        SkillFields = SkillBar.Find("Fields");
        ComboText = ComboArea.Find("ComboNum").GetComponent<TextMeshProUGUI>();
        BottomSpBar = transform.Find("Bottom/SpBar");
        BottomShieldBar = transform.Find("Bottom/ShieldBar");
    }

    private void Start()
    {
        playerData = GameManager.Instance.Player.GetComponent<PlayerController>();

        // ��ʼ��Ѫ��
        TopHpBar.maxValue = playerData.maxHp;
        TopHpBar.minValue = 0;
        TopHpBar.value = TopHpBar.maxValue;
        BottomHpBar.maxValue = playerData.maxHp;
        BottomHpBar.minValue = 0;
        BottomHpBar.value = BottomHpBar.maxValue;

        ShieldCount = playerData.shield;
        SpCount = SkillManager.Instance.curSp;

        // ע�����¼�
        SettingBin.GetComponent<Button>().onClick.AddListener(OnSettingBin);
    }

    private void Update()
    {
        VaryHpBar();
    }

    // ��ť����¼�
    public void OnSettingBin()
    {
        UIManager.Instance.OpenPanel(UIConst.SettingUI);
    }

    // Ѫ������
    public void VaryHpBar()
    {
        float curHp = playerData.curHp;
        if (curHp != TopHpBar.value || curHp != BottomHpBar.value)
        {
            TopHpBar.value = Mathf.Lerp(TopHpBar.value, curHp, Time.deltaTime);
            BottomHpBar.value = Mathf.Lerp(BottomHpBar.value, curHp, Time.deltaTime);
        }
    }
    // �ı令��UI
    public void ShieldUIChange()
    {
        if (ShieldCount != playerData.shield)
        {
            ShieldCount = playerData.shield;
            for (int i = 0; i < 3; i++)
            {
                Transform children = BottomShieldBar.GetChild(i);
                if (ShieldCount > i)
                {
                    children.gameObject.SetActive(true);
                }
                else
                {
                    children.gameObject.SetActive(false) ;
                }
            }
        }

    }
    // �ı�Sp��UI
    public void SpUIChange()
    {
        if (SpCount != SkillManager.Instance.curSp)
        {
            SpCount = SkillManager.Instance.curSp;
            for (int i = 0;i < SkillManager.Instance.maxSp;i++)
            {
                Transform children = SpBar.GetChild(i);
                if (ShieldCount > i)
                {
                    children.gameObject.SetActive(true);
                }
                else
                {
                    children.gameObject.SetActive(false);
                }
                children = BottomShieldBar.GetChild(i);
                if (ShieldCount > i)
                {
                    children.gameObject.SetActive(true);
                }
                else
                {
                    children.gameObject.SetActive(false);
                }
            }

        }
    }
}
