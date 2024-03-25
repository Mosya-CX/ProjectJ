using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TurnData : ScriptableObject
{
    public GameObject TurnScene;// ����
    public Vector3 BornPos;// ��ʼλ��
    public PlayerController playerData;// ��¼�����Ϣ
    public Vector2 ScenePos;
    public virtual void OnCreate()
    {
        // �������λ��
        playerData.transform.position = BornPos;
        // ���س���
        if (!LevelManager.Instance.loadedScene.ContainsKey(TurnScene.name))
        {
            if (LevelManager.Instance.curScene != null)
            {
                LevelManager.Instance.curScene.SetActive(false);
            }
            GameObject scene = Instantiate(TurnScene, ScenePos, Quaternion.identity);// ���ɵ�������
            scene.name = TurnScene.name;
            LevelManager.Instance.curScene = scene;
        }
        else
        {
            GameObject scene = LevelManager.Instance.loadedScene[TurnScene.name];// ���ֵ��л�ȡ
            scene.SetActive(true);// ����
            LevelManager.Instance.curScene = scene;
        }


    }
    public virtual void OnUpdate()
    {

    }
    public virtual void OnDestory()
    {
        LevelManager.Instance.curTurn++;
        playerData.playerState = PlayerState.None;

        LevelManager.Instance.LoadTurn();// ������һ��Turn
    }

}
