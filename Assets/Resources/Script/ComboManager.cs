using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using TMPro;
//������ֻ��Ҫ��player�����ɹ�ʱ����AddComboNum�����Ϳ����ˣ�������

public class ComboManager: SingletonWithMono<ComboManager>
{
    public TextMeshProUGUI ComboText;
    public float DeleteComboTextTime;//combo���õ�ʱ��
    public float startDeleteComboTextTime;//��¼һ��ʼ��combo���õ�ʱ��
    public float Combonum;//comBo�Ķ���
    public float AddSizeCD;//combo��С���ӵ�Cd��ԽС����Խ��
    public float StartAddSizeCD;//��¼һ��ʼ��AddSizeCD
    
    public float comboNum// �����ⲿ�õ���ǰ������
    {
        get
        {
            return Combonum;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        StartAddSizeCD = AddSizeCD;
        AddSizeCD = 0;
        ComboText = UIManager.Instance.FindPanel(UIConst.FightUI).GetComponent<FightPanel>().ComboText;
        Combonum = 0;
        startDeleteComboTextTime = DeleteComboTextTime;
        DeleteComboTextTime = 0;
        ComboText.enableAutoSizing = false;
        //ComboText.rectTransform=FindPanel.coombo.rectTransform;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //��Сtext��С���߼�
        AddSizeCD -= Time.unscaledDeltaTime;
        
        if (AddSizeCD <=0 && ComboText.fontSize>=140)
        {
            ComboText.fontSize -= 2;
            AddSizeCD = StartAddSizeCD;
        }
        Debug.Log(Combonum);
        Instance.ComboText.text = ((int)Instance.Combonum).ToString();
        //ʱ����˺��combo���߼�
        Instance.DeleteComboTextTime -= Time.unscaledDeltaTime;
        if (Instance.DeleteComboTextTime <= 0)
        {
            Debug.Log("����ʱ�����");
            Instance.ComboText.enabled = false;
            Instance.Combonum = 0;
        }
        //�ﵽcombo����ʱ��ֹͣ
        if (Instance.Combonum == 20 || Instance.Combonum == 50 || Instance.Combonum % 100 == 0)
        {
            TimeManager.TimeOut(true);
        }
        //combo���˽���նɱ
    }
    public static void AddComboNum()
    {
        Instance.ComboText.enabled = true;
        Instance.ComboText.fontSize = 180;//����С���
        Instance.DeleteComboTextTime = Instance.startDeleteComboTextTime;//ˢ��text��ʧ��ʱ��
        Instance.Combonum += 1; 
    }
    public static void ReSetComboNum()
    {
        Debug.Log("�����combo����");
        Instance.Combonum = 0;
    }
         
}
