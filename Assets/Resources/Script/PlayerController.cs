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

    private void Start()
    {
        // ��ʼ��
        curHp = maxHp;
        curEndurance = maxEndurance;
    }

    private void Update()
    {
        // ����ж����


    }

    // ��ҹ�����Χ��ⷽ��

    // ����ܻ��ж�
    public void OnHit()
    {
        
    }

}
