using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameLanuner 
{

    void Awake()
    {
        
        // �ȼ�����Դ

        // �ٳ�ʼ��������
        GameManager.Instance = new GameManager();
        UIManager.Instance = new UIManager();
        soundManager.Instance = new soundManager();
        AttackMoment.Instance = new AttackMoment();
        LevelManager.Instance = new LevelManager();
        TimeManager.Instance = new TimeManager();
        // ��ʼ��Ѱ·����

        //EnemyManager.Instance = new EnemyManager();

        // Ȼ����ؿ�ʼ����
        //UIManager.Instance.OpenPanel(UIConst.LoginUI);

        // �첽����ս��UI������
    }
}
