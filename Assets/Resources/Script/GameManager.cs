using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // 游戏进程记录
    public int curProgress;

    private void Start()
    {
        // 初始化变量
        curProgress = 0;

        // 初始界面和场景
               
    }


    private void Update()
    {
        
    }
}
