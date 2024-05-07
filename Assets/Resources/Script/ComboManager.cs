using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using TMPro;
//������ֻ��Ҫ��player�����ɹ�ʱ����AddComboNum�����Ϳ����ˣ�������


public class ComboManager: SingletonWithMono<ComboManager>
{
    private TextMeshProUGUI ComboText;
    public float DeleteComboTextTime = 4 ;//combo���õ�ʱ��
    private float startDeleteComboTextTime;//��¼һ��ʼ��combo���õ�ʱ��
    private int Combonum;//comBo�Ķ���
    public float AddSizeCD=0.03f;//combo��С���ӵ�Cd��ԽС����Խ��
    private float StartAddSizeCD;//��¼һ��ʼ��AddSizeCD

    private bool IsComboEnable=true;//�ж�ComboManager�Ƿ�������Ĭ��������

    
    public int comboNum// �����ⲿ�õ���ǰ������
    {
        get
        {
            return Combonum;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        if (Instance.IsComboEnable)
        {
            Instance.StartAddSizeCD = Instance.AddSizeCD;
            Instance.AddSizeCD = 0;
            Instance.ComboText = UIManager.Instance.FindPanel(UIConst.FightUI).GetComponent<FightPanel>().ComboText;
            Instance.Combonum = 0;
            // if (Instance.DeleteComboTextTime == 0)
            //{
            //    Instance.DeleteComboTextTime = 4;
            //}
            Instance.startDeleteComboTextTime = Instance.DeleteComboTextTime;
            Instance.DeleteComboTextTime = 0;
            Instance.ComboText.enableAutoSizing = false;
            //ComboText.rectTransform=FindPanel.coombo.rectTransform;
        }
        ComboText.text = "";
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Instance.DeleteComboTextTime);
        if (Instance.IsComboEnable)
        {
            //��Сtext��С���߼�
            Instance.AddSizeCD -= Time.unscaledDeltaTime;
            Debug.Log(Instance.ComboText);

            if (Instance.AddSizeCD <= 0 && Instance.ComboText.fontSize >= 140)
            {
                Instance.ComboText.fontSize -= 2;
                Instance.AddSizeCD = Instance.StartAddSizeCD;
            }
            Debug.Log(Combonum);
            Instance.ComboText.text = Instance.Combonum.ToString();
            //ʱ����˺��combo���߼�
            if (Instance.DeleteComboTextTime >= 0 && !TimeManager.Instance.isTimeOut)
            {
                Instance.DeleteComboTextTime -= Time.unscaledDeltaTime;
            }

            if (Instance.DeleteComboTextTime <= 0)
            {
                Instance.ComboText.enabled = false;
                Instance.Combonum = 0;
            }
            //�ﵽcombo����ʱ��ֹͣ
            if (Instance.Combonum == 20 || Instance.Combonum == 50 || Instance.Combonum % 100 == 0)
            {
                TimeManager.Instance.TimeOut(true);
            }
            //combo���˽���նɱ
        }
    }
    public  void AddComboNum(int AddComboNum)
    {
        if (Instance.IsComboEnable)
        {
            Instance.ComboText.enabled = true;
            Instance.ComboText.fontSize = 180;//����С���
            Instance.DeleteComboTextTime = Instance.startDeleteComboTextTime;//ˢ��text��ʧ��ʱ��
            Instance.Combonum += AddComboNum;
        }
    }
    public  void ReSetComboNum()
    {
        if (Instance.IsComboEnable)
        {
            Instance.Combonum = 0;
        }
        
    }
    //����comboManager�ĺ���
    public void EnableCombo()
    {
        Instance.IsComboEnable = true;
    }
    //�رյĺ���
    public void DisableCombo()
    {
        Instance.IsComboEnable = false;
    }
}
