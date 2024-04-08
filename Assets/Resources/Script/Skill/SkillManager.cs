using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public enum SkillType
{
    狂热标记,
    无双,
    此身为捷,
    唯快不破,
    安如磐石,
    圆形环游,
}

public class SkillManager : SingletonWithMono<SkillManager>
{
    public List<BaseSkill>  effectiveSkillList = new List<BaseSkill>();// 存储正在生效的技能
    public const string skillsSOPath = "Data/Skill/";
    public int maxSp = 4;
    public int curSp;
    public List<BaseSkill> existedSkillList = new List<BaseSkill>(); // 存储已得到的技能信息
    public Transform skillBar;// 绑定技能栏

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
    // 添加技能
    public void AddSkill(SkillType skillType)
    {
        BaseSkill skill;
        switch(skillType)
        {
            case SkillType.狂热标记:
                skill = Resources.Load(skillsSOPath + "Skill01") as BaseSkill;
                break;
            case SkillType.无双:
                skill = Resources.Load(skillsSOPath + "Skill02") as BaseSkill;
                break;
            case SkillType.此身为捷:
                skill = Resources.Load(skillsSOPath + "Skill03") as BaseSkill;
                break;
            case SkillType.唯快不破:
                skill = Resources.Load(skillsSOPath + "Skill04") as BaseSkill;
                break;
            case SkillType.安如磐石:
                skill = Resources.Load(skillsSOPath + "Skill05") as BaseSkill;
                break;
            case SkillType.圆形环游:
                skill = Resources.Load(skillsSOPath + "Skill06") as BaseSkill;
                break;
            default:
                Debug.LogWarning("找不到该技能:"+skillType.ToString());
                return;
        }
        
        if (skill == null)
        {
            Debug.LogWarning("该技能信息加载失败:"+skill.ToString());
            return;
        }
        
        if (existedSkillList.Contains(skill))
        {
            Debug.LogWarning("已添加过该技能：" + skill.name);
        }
        else
        {
            existedSkillList.Add(skill);// 添加进已拥有的技能库里

            // 并在技能栏上显示
        }

    }
    // 触发技能
    public void TriggerSkillEffect(BaseSkill skill)
    {
        if (skill == null)
        {
            Debug.LogWarning("未找到该技能:" + skill.name);
            return;
        }
        skill.OnCreate();// 当技能一开始被触发时调用
        if (!effectiveSkillList.Contains(skill))
        {
            effectiveSkillList.Add(skill);
        }
    }
    // 改变sp
    public void consumSP(int cost)
    {
        curSp -= cost;
        // 对UI进行改变

    }

    void Update()
    {
        if (skillBar == null)
        {
            Debug.LogWarning("未找到技能栏UI");
            if (UIManager.Instance != null)
            {
                skillBar = UIManager.Instance.FindPanel(UIConst.FightUI).GetComponent<FightPanel>().SkillBar;
            }
            return;
        }
        // 判断是否触发技能
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
        //判断列表里是否有道具，有的话就调用所有道具的Update函数
        if (effectiveSkillList.Count > 0)
        {
            List<int> markIndex = new List<int>();
            int count = effectiveSkillList.Count;
            for (int i = 0; i < count; i++)
            {
                effectiveSkillList[i].OnUpdate();
                if (effectiveSkillList[i].isUsed)//被使用了
                {
                    effectiveSkillList[i].OnDestory();//消除道具效果
                    
                    markIndex.Add(i);
                }
            }
            // 销毁道具
            for (int i = 0;i < markIndex.Count; i++)
            {
                BaseSkill skill = effectiveSkillList[markIndex[i]];
                effectiveSkillList.Remove(skill);
                skill = null; 
            }
        }
    }
}


