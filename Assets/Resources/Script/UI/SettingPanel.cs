using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : BasePanel
{
    // 绑定按钮


    private void Awake()
    {
        // 查找按钮位置

    }

    private void Start()
    {
        // 注册点击事件



    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1.0f;
    }

    // 按钮点击事件

}
