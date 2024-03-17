using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FightTurn", menuName = "LevelSO/TurnInfo/FightTurn")]
public class FightTurn : TurnData
{
    public GameObject enemySpwanPoints;// ��¼ս������������ˢ�ֵ�ĸ��ڵ�
    public int totalEnemyNum;// ��������
    public float createDuration;// ˢ�ּ��ʱ��
    public int OnceCreateNum;// һ�����ɶ��ٵ���
    float Timer;
    int spareEnemyNum;
    public override void OnCreate()
    {
        base.OnCreate();

        spareEnemyNum = totalEnemyNum;

        GameManager.Instance.Player.GetComponent<PlayerController>().playerState = PlayerState.Fight;
        // ������Ӧbgm

    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        // ���ж��Ƿ������һ�����ɵ���
        if (spareEnemyNum <= OnceCreateNum )
        {
            // ���ɵ���


        }

    }

    public override void OnDestory()
    {
        
        base.OnDestory();
    }
}
