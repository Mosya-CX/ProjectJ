using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    // 绑定按钮
    public Button startBin;
    public Button quitBin;

    private void Awake()
    {
        // 查找按钮位置

    }

    private void Start()
    {
        // 注册点击事件

        // 播放bgm

    }

    // 按钮点击事件
    // 开始游戏
    public void onStartBin()
    {
        // 加载场景

        // 加载玩家

        // 加载其它UI界面

        // 关闭当前页面的bgm

        // 关闭当前页面
        Destroy(gameObject);

    }
    // 退出游戏
    public void onQuitBin()
    {
        Application.Quit();
    }

}
