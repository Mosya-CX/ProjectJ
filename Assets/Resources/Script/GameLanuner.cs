using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameLanuner 
{

    void Awake()
    {
        
        // 先加载资源

        // 再初始化管理类
        GameManager.Instance = new GameManager();
        UIManager.Instance = new UIManager();
        soundManager.Instance = new soundManager();
        AttackMoment.Instance = new AttackMoment();
        LevelManager.Instance = new LevelManager();
        TimeManager.Instance = new TimeManager();
        // 初始化寻路网格

        //EnemyManager.Instance = new EnemyManager();

        // 然后加载开始界面
        //UIManager.Instance.OpenPanel(UIConst.LoginUI);

        // 异步加载战斗UI并隐藏
    }
}
