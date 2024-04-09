using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using TMPro;
//������ֻ��Ҫ��player�����ɹ�ʱ����AddComboNum�����Ϳ����ˣ�������

public class ComboManager: MonoBehaviour
{
    public static ComboManager Instance;
    private TextMeshProUGUI ComboText;
    public float DeleteComboTextTime;//combo���õ�ʱ��
    private float startDeleteComboTextTime;//��¼һ��ʼ��combo���õ�ʱ��
    private float Combonum;//comBo�Ķ���
    public float AddSizeCD;//combo��С���ӵ�Cd��ԽС����Խ��
    private float StartAddSizeCD;//��¼һ��ʼ��AddSizeCD
    
    public float comboNum// �����ⲿ�õ���ǰ������
    {
        get
        {
            return Combonum;
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        Instance = this;
    }
    void Start()
    {
        Instance.StartAddSizeCD = Instance.AddSizeCD;
        Instance.AddSizeCD = 0;
        Instance.ComboText = GetComponent<TextMeshProUGUI>();
        Instance.Combonum = 0;
        Instance.startDeleteComboTextTime = Instance.DeleteComboTextTime;
        Instance.DeleteComboTextTime = 0;
        Instance.ComboText.enableAutoSizing = false;
       //Instance.ComboText.rectTransform=UIManager.Instance.FindPanel.coombo.rectTransform;
    }

    // Update is called once per frame
    void Update()
    {
        
        //��Сtext��С���߼�
        Instance.AddSizeCD -= Time.unscaledDeltaTime;
        
        if (Instance.AddSizeCD <=0 && Instance.ComboText.fontSize>=140)
        {
            Instance.ComboText.fontSize -= 2;
            Instance.AddSizeCD = Instance.StartAddSizeCD;
        }
        Debug.Log(Combonum);
        Instance.ComboText.text = ((int)Instance.Combonum).ToString();
        //ʱ����˺��combo���߼�
        Instance.DeleteComboTextTime -= Time.unscaledDeltaTime;
        if (Instance.DeleteComboTextTime <= 0)
        {
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
        Instance.Combonum = 0;
    }
         
}
