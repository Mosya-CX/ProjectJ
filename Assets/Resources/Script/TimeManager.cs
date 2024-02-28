using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//！！！！无需进行任何改变，只需要调用TImeOut函数，并传入是否是iscombostop这个bool值就好了！！！！

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    public float StopLength;//暂停多久时间
    public float Time_Scale;//放缩尺度
    public float TimeStopCD;//时间停止的CD
    private float StartStopLength;//记录一开始的暂停多久时间
    private float StartTimeStopCD;//记录一开始的时间停止的CD

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
        Instance.StartStopLength = Instance.StopLength;
        Instance.StopLength = -0.1f;
        Instance.StartTimeStopCD= Instance.TimeStopCD;
        Instance.TimeStopCD = -0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        Instance.TimeStopCD -= Time.unscaledDeltaTime;
        //是否启动暂停的逻辑
        Instance.StopLength -= Time.unscaledDeltaTime;
        if (Instance.StopLength >= 0)//时间暂停
        {
            Time.timeScale = Instance.Time_Scale;
        }
        else
        {
            Time.timeScale = 1;//时间继续
        }
        //text
        print(TimeStopCD);
        
    }
    static public void TimeOut(bool isComboStopTIme)//时间暂停函数，只需传入是否是combo导致的暂停
    {
        if (isComboStopTIme)//是combo暂停
        {
            Instance.StopLength = Instance.StartStopLength;//暂停时间
            Instance.TimeStopCD = Instance.StartTimeStopCD ;//重置暂停CD
        }
        else if (!isComboStopTIme && Instance.TimeStopCD <= 0)//不是combo暂停
        {
            Instance.TimeStopCD = Instance.StartTimeStopCD;//重置暂停CD
            Instance.StopLength = Instance.StartStopLength;//暂停时间
        }

    }
}
