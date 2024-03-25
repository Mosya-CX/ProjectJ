using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossTurn", menuName = "LevelSO/TurnInfo/BossTurn")]
public class BossTurn : TurnData
{
    public GameObject BossPrefab;
    public List<EnemyOrder> enemyOrder;// ���ֵ�˳��
    int curOrder;// ��¼��ǰ����
    public GameObject enemySpwanPoints;// ��¼ս������������ˢ�ֵ�ĸ��ڵ�
    public override void OnCreate()
    {
        base.OnCreate();
        curOrder = 0;
        GameManager.Instance.Player.GetComponent<PlayerController>().playerState = PlayerState.Fight;
        // ������Ӧbgm

        // ����boss

    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        // ���ɵ���


    }

}

[Serializable]
public class EnemyOrder
{
    public int enemy1Num;
    public int enemy2Num;
    public int enemy3Num;
}