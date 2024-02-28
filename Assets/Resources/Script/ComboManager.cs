using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
//������ֻ��Ҫ��player�����ɹ�ʱ����AddComboNum�����Ϳ����ˣ�������

public class ComboManager: MonoBehaviour
{
    public static ComboManager Instance;
    private Text ComboText;
    public float DeleteComboTextTime;//combo���õ�ʱ��
    private float startDeleteComboTextTime;//��¼һ��ʼ��combo���õ�ʱ��
    private float Combonum;//comBo�Ķ���
    public float AddSizeCD;//combo��С���ӵ�Cd��ԽС����Խ��
    private float StartAddSizeCD;//��¼һ��ʼ��AddSizeCD
    
    // Start is called before the first frame update
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
        StartAddSizeCD = AddSizeCD;
        AddSizeCD = 0;
        ComboText = GetComponent<Text>();
        Combonum = 0;
        startDeleteComboTextTime = DeleteComboTextTime;
        DeleteComboTextTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //��Сtext��С���߼�
        AddSizeCD -= Time.unscaledDeltaTime;
        if (AddSizeCD<=0 && ComboText.fontSize>=140)
        {
            ComboText.fontSize -= 2;
            AddSizeCD = StartAddSizeCD;
        }
        ComboText.text = ((int)Combonum).ToString();
        //ʱ����˺��combo���߼�
        DeleteComboTextTime -= Time.unscaledDeltaTime;
        if (DeleteComboTextTime <= 0)
        {
            ComboText.enabled = false;
            Combonum = 0;
        }
    }
    public static void AddComboNum()
    {
        Instance.ComboText.enabled = true;
        Instance.ComboText.fontSize = 250;//����С���
        Instance.DeleteComboTextTime = Instance.startDeleteComboTextTime;//ˢ��text��ʧ��ʱ��
        Instance.Combonum += 1; 
    }
}
