using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RelaxTurn", menuName = "LevelSO/TurnInfo/RelaxTurn")]
public class PathFindingTurn : TurnData
{
    public GameObject PathFindingPreBg;
    Transform startPoint;
    Transform endPoint;
    public override void OnCreate()
    {
        base.OnCreate();
        GameManager.Instance.Player.GetComponent<PlayerController>().playerState = PlayerState.PathFinding;
        startPoint = PathFindingPreBg.transform.Find("Start");
        endPoint = PathFindingPreBg.transform.Find("End");
        // ≤•∑≈œ‡”¶bgm

    }


}
