using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : BasePanel
{
    // �󶨰�ť


    private void Awake()
    {
        // ���Ұ�ťλ��

    }

    private void Start()
    {
        // ע�����¼�



    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1.0f;
    }

    // ��ť����¼�

}
