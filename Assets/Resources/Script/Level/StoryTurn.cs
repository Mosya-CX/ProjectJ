using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoryTurn", menuName = "LevelSO/TurnInfo/StoryTurn")]
public class StoryTurn : TurnData
{
    public override void OnCreate()
    {
        base.OnCreate();
        GameManager.Instance.Player.GetComponent<PlayerController>().playerState = PlayerState.StoryReading;
        // ≤•∑≈œ‡”¶bgm
    }
}
