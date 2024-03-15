using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FightTurn", menuName = "LevelSO/TurnInfo/FightTurn")]
public class FightTurn : TurnData
{
    public GameObject enemySpwanPoints;
    public override void OnCreate()
    {
        base.OnCreate();
        GameManager.Instance.Player.GetComponent<PlayerController>().playerState = PlayerState.Fight;
        // ������Ӧbgm

    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        // ����ˢ�ֵ����ɹ���

    }

    public override void OnDestory()
    {
        
        base.OnDestory();
    }
}
