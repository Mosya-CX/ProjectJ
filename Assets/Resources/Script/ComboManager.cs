using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
//！！！只需要在player攻击成功时调用AddComboNum函数就可以了！！！！

public class ComboManager: MonoBehaviour
{
    public static ComboManager Instance;
    private Text ComboText;
    public float DeleteComboTextTime;//combo重置的时间
    private float startDeleteComboTextTime;//记录一开始的combo重置的时间
    private float Combonum;//comBo的段数
    public float AddSizeCD;//combo大小增加的Cd，越小增大越快
    private float StartAddSizeCD;//记录一开始的AddSizeCD
    
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
        //减小text大小的逻辑
        AddSizeCD -= Time.unscaledDeltaTime;
        if (AddSizeCD<=0 && ComboText.fontSize>=140)
        {
            ComboText.fontSize -= 2;
            AddSizeCD = StartAddSizeCD;
        }
        ComboText.text = ((int)Combonum).ToString();
        //时间过了后断combo的逻辑
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
        Instance.ComboText.fontSize = 250;//将大小变大
        Instance.DeleteComboTextTime = Instance.startDeleteComboTextTime;//刷新text消失的时间
        Instance.Combonum += 1; 
    }
}
