using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TurnData : ScriptableObject
{
    public GameObject TurnScene;// ����
    public Vector3 BornPos;// ��ҳ�ʼλ��
    public PlayerController playerData;// ��¼�����Ϣ
    public Transform parent;
    public bool isOver;// �ж��Ƿ����

    public virtual GameObject OnCreate()
    {
        // �������λ��
        playerData.transform.position = BornPos;

        GameObject scene;
        // ���س���
        if (!LevelManager.Instance.loadedScene.ContainsKey(TurnScene.name))
        {
            if (LevelManager.Instance.curScene != null)
            {
                LevelManager.Instance.curScene.SetActive(false);
            }
            scene = Instantiate(TurnScene, Vector3.zero, Quaternion.identity);// ���ɵ�������
            scene.transform.SetParent(parent, false);
            scene.name = TurnScene.name;
            //LevelManager.Instance.curScene = scene;
        }
        else
        {
            scene = LevelManager.Instance.loadedScene[TurnScene.name];// ���ֵ��л�ȡ
            scene.SetActive(true);// ����
            //LevelManager.Instance.curScene = scene;
            
        }
        isOver = false;
        return scene;
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
