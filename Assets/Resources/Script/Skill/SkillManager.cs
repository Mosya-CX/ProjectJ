using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
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
    public Transform skillFields;// �󶨼�����
    public Transform spBar;// ��sp��
    public PlayerController playerData;// �������Ϣ
    public FightPanel fightPanel;

    public bool isPause;
    protected override void Awake()
    {
        base.Awake();
        playerData = GameManager.Instance.Player.GetComponent<PlayerController>();// �������Ϣ
    }
    
    
    
    void Start()
    {
        if (maxSp <= 0)
        {
            maxSp = 4;
        }
        curSp = maxSp;

        isPause = false;
    }
    // ��Ӽ���
    public void AddSkill(SkillType skillType)
    {
        if (fightPanel == null)
        {
            fightPanel = UIManager.Instance.OpenPanel(UIConst.FightUI) as FightPanel;
            UIManager.Instance.ClosePanel(UIConst.FightUI);
        }
        Debug.Log("������Ӽ���:"+ skillType);
        BaseSkill skill;
        switch(skillType)
        {
            case SkillType.���ȱ��:
                skill = Resources.Load(skillsSOPath + "Skill01") as BaseSkill;
                fightPanel.SkillFields.GetChild(0).gameObject.SetActive(true);
                break;
            case SkillType.��˫:
                skill = Resources.Load(skillsSOPath + "Skill02") as BaseSkill;
                fightPanel.SkillFields.GetChild(1).gameObject.SetActive(true);
                break;
            case SkillType.����Ϊ��:
                skill = Resources.Load(skillsSOPath + "Skill03") as BaseSkill;
                fightPanel.SkillFields.GetChild(2).gameObject.SetActive(true);
                break;
            case SkillType.Ψ�첻��:
                skill = Resources.Load(skillsSOPath + "Skill04") as BaseSkill;
                fightPanel.SkillFields.GetChild(3).gameObject.SetActive(true);
                break;
            case SkillType.������ʯ:
                skill = Resources.Load(skillsSOPath + "Skill05") as BaseSkill;
                fightPanel.SkillFields.GetChild(4).gameObject.SetActive(true);
                break;
            case SkillType.Բ�λ���:
                skill = Resources.Load(skillsSOPath + "Skill06") as BaseSkill;
                fightPanel.SkillFields.GetChild(5).gameObject.SetActive(true);
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
            if (playerData == null)
            {
                Debug.LogWarning("������������Ϊ��");
            }
            skill.playData = playerData;// �ڼ�����洢��Ҷ�����Ϣ


            Debug.Log("������ӳɹ�:"+skill.name);
            return;
        }

        Debug.Log("�������ʧ��:"+skill.name);
    }
    // ��������
    public void TriggerSkillEffect(BaseSkill skill)
    {
        Debug.Log("���ڴ�������");
        if (skill == null)
        {
            Debug.LogWarning("δ�ҵ��ü���:" + skill.name);
            return;
        }
        skill.OnCreate();// ������һ��ʼ������ʱ����
        if (!effectiveSkillList.Contains(skill))
        {
            effectiveSkillList.Add(skill);
            Debug.Log("��ӽ���Ч��:" + skill.name);
        }
        existedSkillList.Remove(skill);// �ӿ��ü��������Ƴ�
        Debug.Log("�������");
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
        if (skillFields == null)
        {
            Debug.LogWarning("δ�ҵ�������UI");
            if (UIManager.Instance != null)
            {
                Debug.Log("���ڲ��Ҽ�����UI");
                skillFields = UIManager.Instance.FindPanel(UIConst.FightUI).GetComponent<FightPanel>().SkillFields;
            }
            if (skillFields == null)
            {
                Debug.Log("����ʧ��");
                return;
            }
        }

        if (isPause)
        {
            Debug.Log("SkillManager��ͣ��");
            return;
        }

        // �ж��Ƿ񴥷�����
        if (existedSkillList.Count > 0)
        {
            Debug.Log("���ڼ�⼼���Ƿ񱻴���");
            for (int i = 0; i < existedSkillList.Count;i++)
            {
                Debug.Log("��⼼��:" + existedSkillList[i].name);
                // ����������������ʱ���� true
                if(existedSkillList[i].OnTrigger())
                {
                    // ��ִ�м����߼�
                    TriggerSkillEffect(existedSkillList[i]);
                }
            }
            Debug.Log("������");
        }

        //�ж��б����Ƿ��е��ߣ��еĻ��͵������е��ߵ�Update����
        if (effectiveSkillList.Count > 0)
        {
            Debug.Log("���ڵ��ü���");
            List<int> markIndex = new List<int>();
            int count = effectiveSkillList.Count;
            for (int i = 0; i < count; i++)
            {
                Debug.Log("���ü���Update:" + effectiveSkillList[i].name);
                effectiveSkillList[i].OnUpdate();
                if (effectiveSkillList[i].isUsed)//��ʹ����
                {
                    Debug.Log("���ü���Destory:" + effectiveSkillList[i].name);
                    effectiveSkillList[i].OnDestory();//��������Ч��
                    
                    markIndex.Add(i);
                }
            }
            Debug.Log("������ϣ���������������ļ���");
            // ���ٵ���
            for (int i = 0;i < markIndex.Count; i++)
            {
                BaseSkill skill = effectiveSkillList[markIndex[i]];
                effectiveSkillList.Remove(skill);// ��������Ч�ļ��������޳�
                existedSkillList.Add(skill);// �ӻؿ��ü�����
            }
            Debug.Log("�������");
        }
    }


}


