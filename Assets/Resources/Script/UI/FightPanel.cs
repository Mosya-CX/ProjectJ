using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FightPanel : BasePanel
{
    // 绑定节点
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
    // 其它信息数据
    public PlayerController playerData;

    private void Awake()
    {
        // 查找节点位置
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
    }

    private void Start()
    {
        playerData = GameManager.Instance.Player.GetComponent<PlayerController>();

        // 初始化血条
        TopHpBar.maxValue = playerData.maxHp;
        TopHpBar.minValue = 0;
        TopHpBar.value = TopHpBar.maxValue;
        BottomHpBar.maxValue = playerData.maxHp;
        BottomHpBar.minValue = 0;
        BottomHpBar.value = BottomHpBar.maxValue;

        // 注册点击事件
        SettingBin.GetComponent<Button>().onClick.AddListener(OnSettingBin);
    }

    private void Update()
    {
        VaryHpBar();
    }

    // 按钮点击事件
    public void OnSettingBin()
    {
        UIManager.Instance.OpenPanel(UIConst.SettingUI);
    }

    // 血条渐变
    public void VaryHpBar()
    {
        float curHp = playerData.curHp;
        if (curHp != TopHpBar.value || curHp != BottomHpBar.value)
        {
            TopHpBar.value = Mathf.Lerp(TopHpBar.value, curHp, Time.deltaTime);
            BottomHpBar.value = Mathf.Lerp(BottomHpBar.value, curHp, Time.deltaTime);
        }
    }
}
