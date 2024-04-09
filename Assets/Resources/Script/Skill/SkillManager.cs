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
    public float recoveryDuration = 15;
    public List<BaseSkill> existedSkillList = new List<BaseSkill>(); // �洢���õļ��ܵ���Ϣ
    public Transform skillBar;// �󶨼�����
    public Transform spBar;// ��sp��
    public PlayerController playerData;// �������Ϣ

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

        playerData = GameManager.Instance.Player.GetComponent<PlayerController>();// �������Ϣ
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

            skill.playData = playerData;// �ڼ�����洢��Ҷ�����Ϣ

            // ���ڼ���������ʾUI


            Debug.Log("���ܳɹ���ӽ����ܿ�");
        }

        Debug.Log("���ܼ������");
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
        existedSkillList.Remove(skill);
    }
    // �ı�sp
    public void consumSP(int cost)
    {
        curSp -= cost;
        // ��UI���иı�

    }

    void Update()
    {
        // �ж��Ƿ���ս��״̬
        if (playerData.playerState != PlayerState.Fight)
        {
            if (effectiveSkillList.Count > 0)
            {
                for (int i = 0; i < effectiveSkillList.Count; i++)
                {
                    effectiveSkillList[i].OnReset();
                }
                effectiveSkillList.Clear();
            }
            return;
        }
        // �󶨼�����
        if (skillBar == null)
        {
            Debug.LogWarning("δ�ҵ�������UI");
            if (UIManager.Instance != null)
            {
                skillBar = UIManager.Instance.FindPanel(UIConst.FightUI).GetComponent<FightPanel>().SkillBar;
            }
            return;
        }

        // ���м��ܲ۵ĸı�
        VarySkillBar();

        // �ж��Ƿ񴥷�����
        if (existedSkillList.Count > 0)
        {
            for (int i = 0; i < existedSkillList.Count;i++)
            {
                // ����������������ʱ���� true
                if(existedSkillList[i].OnTrigger())
                {
                    // ��ִ�м����߼�
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
                effectiveSkillList.Remove(skill);// ��������Ч�ļ��������޳�
                existedSkillList.Add(skill);// �ӻؿ��ü�����
            }
        }
    }

    public void VarySkillBar()
    {
        if (curSp < maxSp)
        {
            recoveryDuration -= Time.deltaTime;
            if (recoveryDuration <= 0)
            {
                recoveryDuration = 15;
                curSp++;
            }
        }
        else
        {
            recoveryDuration = 15;
        }
        if (curSp != spBar.childCount)
        {
            // ����UI����ĵ���
        }
    }
}


