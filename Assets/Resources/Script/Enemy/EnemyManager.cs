using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    // �洢�����ڵ��˵�λ
    public List<Enemy> enemyList;
    private void Awake()
    {
        Instance = this;
        enemyList = new List<Enemy>();
    }


    // ���ɵ���
    public void CreateEnemy()
    {
        // �����������

        // ���ɵ���������

    }

}
