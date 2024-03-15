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
        // 播放相应bgm

    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        // 根据刷怪点生成怪物

    }

    public override void OnDestory()
    {
        
        base.OnDestory();
    }
}
