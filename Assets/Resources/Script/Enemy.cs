using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // ���˱���
    public List<char> key;// �洢��ǰ�������ϵ���ĸ
    public int enemyType;// �жϵ�������

    private void Awake()
    {
        key = new List<char>();
    }

    private void Start()
    {
        // ��ʼ��

        // ���ݵ����������ɵ���������ĸ

    }

    // ������ɵ���������ĸ
    public void CreateKey()
    {
        key.Add((char)Random.Range(65, 91));
    }

    // �ܻ��ж�
    public void OnHit()
    {
        
    }

}
