using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ش���
//�ظ���ɫ 1 ��Ѫ����������û������ɱ�� 10 ������ʱ�����˻ش�����ô��ɫ����ظ� 2
//��Ѫ���ش�+1��combo=10��+1��

public class Item02 : BaseItem
{
    public float healHp = 1;
    public GameObject playerInfo;
    public Animator playerAni;
    public PlayerController playerData;
    public override void OnCreate()
    {
        base.OnCreate();
        // ��ʼ����Ա����
        healHp = 1;
        // �����ҵ���Ϣ
        playerInfo = GameObject.FindWithTag("Player");
        playerAni = playerInfo.GetComponent<Animator>();
        playerData = playerInfo.GetComponent<PlayerController>();
        // ���Ż�Ѫ����

        // ��������

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        // ����Ѫ�������Ž���
        // ������ʵĽ���ʱ����Effect()
        // ���������ʱ��isUsed��Ϊtrue
    }

    public override void OnDestory()
    {
        base.OnDestory();
    }

    public override void Effect()
    {
        base.Effect();
        // ���Combo����
        // �����ڵ���10��healHp+1
        if (playerData.curHp + healHp >= playerData.maxHp)
        {
            playerData.curHp = playerData.maxHp;
        }
        else
        {
            playerData.curHp += healHp;
        }
    }
}
