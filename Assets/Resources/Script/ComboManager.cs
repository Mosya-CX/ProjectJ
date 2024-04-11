using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using TMPro;
//！！！只需要在player攻击成功时调用AddComboNum函数就可以了！！！！

public class ComboManager: SingletonWithMono<ComboManager>
{
    private TextMeshProUGUI ComboText;
    public float DeleteComboTextTime;//combo重置的时间
    private float startDeleteComboTextTime;//记录一开始的combo重置的时间
    public float Combonum;//comBo的段数
    public float AddSizeCD;//combo大小增加的Cd，越小增大越快
    private float StartAddSizeCD;//记录一开始的AddSizeCD
    
    public float comboNum// 用于外部得到当前连击数
    {
        get
        {
            return Combonum;
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        StartAddSizeCD = AddSizeCD;
        AddSizeCD = 0;
        ComboText = UIManager.Instance.FindPanel(UIConst.FightUI).GetComponent<FightPanel>().ComboText;
        Combonum = 0;
        startDeleteComboTextTime = DeleteComboTextTime;
        DeleteComboTextTime = 0;
        ComboText.enableAutoSizing = false;
       //ComboText.rectTransform=FindPanel.coombo.rectTransform;
    }

    // Update is called once per frame
    void Update()
    {
        
        //减小text大小的逻辑
        AddSizeCD -= Time.unscaledDeltaTime;
        
        if (AddSizeCD <=0 && ComboText.fontSize>=140)
        {
            ComboText.fontSize -= 2;
            AddSizeCD = StartAddSizeCD;
        }
        Debug.Log(Combonum);
        Instance.ComboText.text = ((int)Instance.Combonum).ToString();
        //时间过了后断combo的逻辑
        Instance.DeleteComboTextTime -= Time.unscaledDeltaTime;
        if (Instance.DeleteComboTextTime <= 0)
        {
            Instance.ComboText.enabled = false;
            Instance.Combonum = 0;
        }
        //达到combo数后时间停止
        if (Instance.Combonum == 20 || Instance.Combonum == 50 || Instance.Combonum % 100 == 0)
        {
            TimeManager.TimeOut(true);
        }
        //combo到了进行斩杀
    }
    public static void AddComboNum()
    {
        Instance.ComboText.enabled = true;
        Instance.ComboText.fontSize = 180;//将大小变大
        Instance.DeleteComboTextTime = Instance.startDeleteComboTextTime;//刷新text消失的时间
        Instance.Combonum += 1; 
    }
    public static void ReSetComboNum()
    {
        Instance.Combonum = 0;
    }
         
}
