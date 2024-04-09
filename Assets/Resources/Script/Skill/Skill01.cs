using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���ȱ��
//���;������ܵ�ͼ1ս��1�ڵ����й���
//����������������ɱ����50�κ�����sp 0��
//����Ч�����������״̬��ÿ�λ�ɱ���˵�combo������1��Ϊ2�� 

[CreateAssetMenu(fileName = "Skill01", menuName = "SkillSO/Skill01")]
public class Skill01 : BaseSkill
{
    [Header("�����޸�")]
    public int markCombo = 0;

    public override bool OnTrigger()
    {
        if( !base.OnTrigger())
        {
            markCombo = (int)ComboManager.Instance.comboNum;
            return false;
        }
        int curCombo = (int)ComboManager.Instance.comboNum;// ��¼��ǰ������
        int result = curCombo - markCombo;// ���㵱ǰ������������ʼ��������
        if (result < 0)// �ж��Ƿ����������
        {
            markCombo = curCombo;
            return false;
        }
        if (result >= 50)
        {
            markCombo = curCombo;
            return true;
        }
        return false;
    }
    public override void OnCreate()
    {
        base.OnCreate();
        // ����UI��������ϵ�һЩ����

        Effect();// ���ü���Ч��
    }

    public override void Effect()
    {
        base.Effect();
        // �ı�ÿ��combo����
        playData.comboAdd = 2;
    }

    public override void OnReset()
    {
        base.OnReset();
        markCombo = 0;
    }
}
