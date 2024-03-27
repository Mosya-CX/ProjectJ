using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // ��Ϸ���̼�¼
    public int curProgress;
    // ���������Ϣ
    public GameObject Player;
    // �洢����������Ϣ
    public float viewHeight;
    public float viewWidth;

    private void Start()
    {
        // ��ʼ������
        curProgress = 0;

        viewHeight = Camera.main.orthographicSize * 2;
        viewWidth = viewHeight * Camera.main.aspect;

        // ��ʼ����ͳ���
        //UIManager.Instance.OpenPanel(UIConst.LoginUI);

        // �������Ϣ
        //Player = GameObject.FindWithTag("Player");
        //if (Player == null)
        //{
        //    // �������Ԥ����
        //    Player = GameObject.Instantiate(Resources.Load("Prefab/Character/Player")) as GameObject;
        //}

        // ����
        

    }


    private void Update()
    {
        
    }
}
