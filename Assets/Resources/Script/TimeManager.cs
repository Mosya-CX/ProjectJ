using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//！！！！无需进行任何改变，只需要调用TImeOut函数，并传入是否是iscombostop这个bool值就好了！！！！

public class TimeManager: MonoBehaviour
{
    public static TimeManager Instance;
    public float StopLength=2.5f;//暂停多久时间
    //public float Time_Scale;//放缩尺度
    public float TimeStopCD=7.5f;//闪避时间停止的CD
    private float StartStopLength;//记录一开始的暂停多久时间
    private float StartTimeStopCD;//记录一开始的闪避时间停止的CD

    //判断是否开启的bool（默认开启）
    private bool isEnableTime=true;

    public bool isTimeOut=false;//给外部判断是否时间停止

    // Start is called before the first frame update
    void Awake()
    {
        //给Instance赋值
        if (Instance != null)
            Destroy(this);
        Instance = this;
    }
    void Start()
    {
        if (Instance.isEnableTime)
        {
            Instance.StartStopLength = Instance.StopLength;
            Instance.StopLength = -0.1f;
            Instance.StartTimeStopCD = Instance.TimeStopCD;
            Instance.TimeStopCD = -0.1f;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Instance.isEnableTime)
        {
            Instance.TimeStopCD -= Time.unscaledDeltaTime;
            //是否启动暂停的逻辑
            Instance.StopLength -= Time.unscaledDeltaTime;

            //！！！所有的时间暂停后的表现都写在下面，比如计时器的停止计时，小怪移动停止，！！！！

            if (Instance.StopLength >= 0)//时间暂停
            {
                //Time.timeScale = Instance.Time_Scale;
                Instance.isTimeOut = true;

            }
            else//时间继续
            {
                //Time.timeScale = 1;
                Instance.isTimeOut = false;
            }
            //text
        }
       
        
        
    }
     public void TimeOut(bool isComboStopTIme)//时间暂停函数，只需传入是否是combo导致的暂停
    {
        if (Instance.isEnableTime)
        {
            if (isComboStopTIme)//是combo暂停
            {
                Instance.StopLength = Instance.StartStopLength;//暂停多久时间
                //Instance.TimeStopCD = Instance.StartTimeStopCD;//重置暂停CD
            }
            else if (!isComboStopTIme && Instance.TimeStopCD <= 0)//闪避暂停
            {
                Instance.TimeStopCD = Instance.StartTimeStopCD;//重置闪避暂停CD
                Instance.StopLength = Instance.StartStopLength;//暂停时间
            }
        }
    }
    public void SkillTimeOut(float SkillStopLenth)//给外部技能时间停止调用
    {
        if (Instance.isEnableTime)
        {
            Instance.StopLength = SkillStopLenth;
        }
    }
    //开启TimeMa的函数
    public void EnableTimeMa()
    {
        Instance.isEnableTime = true;
    }
    //关闭TimeMa的函数
    public void DisableTimeMa()
    {
        Instance.isEnableTime = false;
    }
}
