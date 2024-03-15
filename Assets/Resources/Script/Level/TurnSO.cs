using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TurnData : ScriptableObject
{
    public GameObject TurnScene;// ս������
    public Vector3 BornPos;// ��ʼλ��
    public PlayerController playerData;// ��¼�����Ϣ
    public Vector2 ScenePos;
    public virtual void OnCreate()
    {
        // �������λ��
        playerData.transform.position = BornPos;
        // ���س���
        if (LevelManager.Instance.curScene != TurnScene)
        {
            Destroy(LevelManager.Instance.curScene);
            Instantiate(TurnScene, ScenePos, Quaternion.identity);
        }
        else
        {
            LevelManager.Instance.curScene.SetActive(true);
        }
    }
    public virtual void OnUpdate()
    {

    }
    public virtual void OnDestory()
    {
        LevelManager.Instance.curTurn++;
        playerData.playerState = PlayerState.None;
    }

}
