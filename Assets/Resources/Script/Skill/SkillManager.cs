using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public enum SkillType
{
    ���ȱ��,
    ��˫,
    ����Ϊ��,
    Ψ�첻��,
    ������ʯ,
    Բ�λ���,
}

public class SkillManager : SingletonWithMono<SkillManager>
{
    public List<BaseSkill>  effectiveSkillList = new List<BaseSkill>();// �洢������Ч�ļ���
    public const string skillsSOPath = "Data/Skill/";
    public int maxSp = 4;
    public int curSp;
    public List<BaseSkill> existedSkillList = new List<BaseSkill>(); // �洢�ѵõ��ļ�����Ϣ
    public Transform skillBar;// �󶨼�����

    protected override void Awake()
    {
        base.Awake();
    }
    
    
    
    void Start()
    {
        if (maxSp <= 0)
        {
            maxSp = 4;
        }
        curSp = maxSp;
    }
    // ��Ӽ���
    public void AddSkill(SkillType skillType)
    {
        BaseSkill skill;
        switch(skillType)
        {
            case SkillType.���ȱ��:
                skill = Resources.Load(skillsSOPath + "Skill01") as BaseSkill;
                break;
            case SkillType.��˫:
                skill = Resources.Load(skillsSOPath + "Skill02") as BaseSkill;
                break;
            case SkillType.����Ϊ��:
                skill = Resources.Load(skillsSOPath + "Skill03") as BaseSkill;
                break;
            case SkillType.Ψ�첻��:
                skill = Resources.Load(skillsSOPath + "Skill04") as BaseSkill;
                break;
            case SkillType.������ʯ:
                skill = Resources.Load(skillsSOPath + "Skill05") as BaseSkill;
                break;
            case SkillType.Բ�λ���:
                skill = Resources.Load(skillsSOPath + "Skill06") as BaseSkill;
                break;
            default:
                Debug.LogWarning("�Ҳ����ü���:"+skillType.ToString());
                return;
        }
        
        if (skill == null)
        {
            Debug.LogWarning("�ü�����Ϣ����ʧ��:"+skill.ToString());
            return;
        }
        
        if (existedSkillList.Contains(skill))
        {
            Debug.LogWarning("����ӹ��ü��ܣ�" + skill.name);
        }
        else
        {
            existedSkillList.Add(skill);// ��ӽ���ӵ�еļ��ܿ���

            // ���ڼ���������ʾ
        }

    }
    // ��������
    public void TriggerSkillEffect(BaseSkill skill)
    {
        if (skill == null)
        {
            Debug.LogWarning("δ�ҵ��ü���:" + skill.name);
            return;
        }
        skill.OnCreate();// ������һ��ʼ������ʱ����
        if (!effectiveSkillList.Contains(skill))
        {
            effectiveSkillList.Add(skill);
        }
    }
    // �ı�sp
    public void consumSP(int cost)
    {
        curSp -= cost;
        // ��UI���иı�

    }

    void Update()
    {
        if (skillBar == null)
        {
            Debug.LogWarning("δ�ҵ�������UI");
            if (UIManager.Instance != null)
            {
                skillBar = UIManager.Instance.FindPanel(UIConst.FightUI).GetComponent<FightPanel>().SkillBar;
            }
            return;
        }
        // �ж��Ƿ񴥷�����
        if (existedSkillList.Count > 0)
        {
            for (int i = 0; i < existedSkillList.Count;i++)
            {
                if(existedSkillList[i].OnTrigger())
                {
                    TriggerSkillEffect(existedSkillList[i]);
                }
            }
        }
        else
        {
            return;
        }
        //�ж��б����Ƿ��е��ߣ��еĻ��͵������е��ߵ�Update����
        if (effectiveSkillList.Count > 0)
        {
            List<int> markIndex = new List<int>();
            int count = effectiveSkillList.Count;
            for (int i = 0; i < count; i++)
            {
                effectiveSkillList[i].OnUpdate();
                if (effectiveSkillList[i].isUsed)//��ʹ����
                {
                    effectiveSkillList[i].OnDestory();//��������Ч��
                    
                    markIndex.Add(i);
                }
            }
            // ���ٵ���
            for (int i = 0;i < markIndex.Count; i++)
            {
                BaseSkill skill = effectiveSkillList[markIndex[i]];
                effectiveSkillList.Remove(skill);
                skill = null; 
            }
        }
    }
}


