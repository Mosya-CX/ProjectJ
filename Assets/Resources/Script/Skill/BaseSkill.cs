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
        if (playData == null)
        {
            Debug.LogWarning("�������Ϊ��:" + this.name);
        }
        if (SkillManager.Instance.curSp - spCost < 0)
        {
            return false;
        }
        Debug.Log("��������:" + this.name);
        SkillManager.Instance.consumSP(spCost);
        return true;
    }
    public virtual void OnCreate()// ���ܸտ�ʼ��������
    {
        Debug.Log("���ܿ�ʼ����:"+this.name);
        isUsed = false;
        OnReset();

        
    }

    public virtual void OnUpdate()// ���ܴ���ʱ��������
    {
        Debug.Log("�������ڵ���:" + this.name);
    }

    public virtual void OnDestory()// ���ܽ���ʱ����
    {
        Debug.Log("���ܽ�������:" + this.name);
        OnReset();
    }

    public virtual void Effect()// ����Ч��
    {
        Debug.Log("����Ч������:" + this.name);
    }

    public virtual void OnReset()// �������ü���ĳЩ����
    {
        Debug.Log("�������õ���:" + this.name);
    }
}
