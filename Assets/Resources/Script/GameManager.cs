using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : SingletonWithMono<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
        viewHeight = Camera.main.orthographicSize * 2;
        viewWidth = viewHeight * Camera.main.aspect;
        CreatePlayer();
    }

    // ��Ϸ���̼�¼
    public int curProgress;
    // ���������Ϣ
    public GameObject Player;
    // �洢�������ʼ�����Ϣ
    public float viewHeight;
    public float viewWidth;
    
    
    private void Start()
    {
        // ��ʼ������
        curProgress = 0;

        

        

        // ��ʼ����ͳ���
        

        

        // ����


    }


    private void Update()
    {
        
    }

    public void CreatePlayer()
    {
        //�������Ϣ
        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player");
        }
        
        if (Player == null)
        {
            // �������Ԥ����
            Player = GameObject.Instantiate(Resources.Load("Prefab/Character/Player")) as GameObject;
            
        }
        Player.name = "Player";
        Player.SetActive(false);
    }
}
