using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    // �󶨰�ť
    public Button startBin;
    public Button quitBin;

    private void Awake()
    {
        // ���Ұ�ťλ��

    }

    private void Start()
    {
        // ע�����¼�

        // ����bgm

    }

    // ��ť����¼�
    // ��ʼ��Ϸ
    public void onStartBin()
    {
        // ���س���

        // �������

        // ��������UI����

        // �رյ�ǰҳ���bgm

        // �رյ�ǰҳ��
        Destroy(gameObject);

    }
    // �˳���Ϸ
    public void onQuitBin()
    {
        Application.Quit();
    }

}
