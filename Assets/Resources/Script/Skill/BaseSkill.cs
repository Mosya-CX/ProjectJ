using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseSkill : ScriptableObject
{
    // ��ر���
    public bool isUsed = false;
    public int spCost;// ���ܻ���
    public Image skillIcon;// ����ͼ��
    public PlayerController playData;// ���������
    public virtual bool OnTrigger()// �����жϼ����Ƿ񱻴���
    {
        if (SkillManager.Instance.curSp - spCost < 0)
        {
            return false;
        }
        SkillManager.Instance.consumSP(spCost);
        return true;
    }
    public virtual void OnCreate()// ���ܸտ�ʼ��������
    {
        isUsed = false;
        OnReset();
    }

    public virtual void OnUpdate()// ���ܴ���ʱ��������
    {

    }

    public virtual void OnDestory()// ���ܽ���ʱ����
    {
        OnReset();
    }

    public virtual void Effect()// ����Ч��
    {
        
    }

    public virtual void OnReset()// �������ü���ĳЩ����
    {

    }
}
