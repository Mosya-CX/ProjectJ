using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ��ұ���
    public float maxHp;
    public float curHp;
    public float maxEndurance;
    public float curEndurance;
    // �洢�ɹ������˵�����
    public List<Enemy> attackableEnemies;

    private void Start()
    {
        // ��ʼ��
        curHp = maxHp;
        curEndurance = maxEndurance;
        attackableEnemies = new List<Enemy>();
    }

    private void Update()
    {
        // ����ж����


    }

    

    // ����ܻ��ж�
    public void OnHit()
    {
        
    }

}
