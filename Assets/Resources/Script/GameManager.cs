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

    private void Start()
    {
        // ��ʼ������
        curProgress = 0;

        // ��ʼ����ͳ���

        // ����
        char test = (char)65;
        print(test);
    }


    private void Update()
    {
        
    }
}
