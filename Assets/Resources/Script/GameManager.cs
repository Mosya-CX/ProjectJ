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

    private void Start()
    {
        // ��ʼ������
        curProgress = 0;

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
        GameObject testobj = GameObject.Find("Level01FightBg");
        SpriteRenderer spr = testobj.GetComponent<SpriteRenderer>();
        Sprite bgsp = spr.sprite;
        Debug.Log(bgsp.bounds.size.x);
        Debug.Log(bgsp.bounds.size.y);

    }


    private void Update()
    {
        
    }
}
