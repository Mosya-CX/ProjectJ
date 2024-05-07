using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
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
    public float recoveryDuration = 15;
    public List<BaseSkill> existedSkillList = new List<BaseSkill>(); // 存储可用的技能的信息
    public Transform skillFields;// 绑定技能栏
    public Transform spBar;// 绑定sp槽
    public PlayerController playerData;// 绑定玩家信息
    public FightPanel fightPanel;

    public bool isPause;
    protected override void Awake()
    {
        base.Awake();
        playerData = GameManager.Instance.Player.GetComponent<PlayerController>();// 绑定玩家信息
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
    // 添加技能
    public void AddSkill(SkillType skillType)
    {
        if (fightPanel == null)
        {
            fightPanel = UIManager.Instance.OpenPanel(UIConst.FightUI) as FightPanel;
            UIManager.Instance.ClosePanel(UIConst.FightUI);
        }
        Debug.Log("正在添加技能:"+ skillType);
        BaseSkill skill;
        switch(skillType)
        {
            case SkillType.狂热标记:
                skill = Resources.Load(skillsSOPath + "Skill01") as BaseSkill;
                fightPanel.SkillFields.GetChild(0).gameObject.SetActive(true);
                break;
            case SkillType.无双:
                skill = Resources.Load(skillsSOPath + "Skill02") as BaseSkill;
                fightPanel.SkillFields.GetChild(1).gameObject.SetActive(true);
                break;
            case SkillType.此身为捷:
                skill = Resources.Load(skillsSOPath + "Skill03") as BaseSkill;
                fightPanel.SkillFields.GetChild(2).gameObject.SetActive(true);
                break;
            case SkillType.唯快不破:
                skill = Resources.Load(skillsSOPath + "Skill04") as BaseSkill;
                fightPanel.SkillFields.GetChild(3).gameObject.SetActive(true);
                break;
            case SkillType.安如磐石:
                skill = Resources.Load(skillsSOPath + "Skill05") as BaseSkill;
                fightPanel.SkillFields.GetChild(4).gameObject.SetActive(true);
                break;
            case SkillType.圆形环游:
                skill = Resources.Load(skillsSOPath + "Skill06") as BaseSkill;
                fightPanel.SkillFields.GetChild(5).gameObject.SetActive(true);
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
            if (playerData == null)
            {
                Debug.LogWarning("传入的玩家数据为空");
            }
            skill.playData = playerData;// 在技能里存储玩家对象信息


            Debug.Log("技能添加成功:"+skill.name);
            return;
        }

        Debug.Log("技能添加失败:"+skill.name);
    }
    // 触发技能
    public void TriggerSkillEffect(BaseSkill skill)
    {
        Debug.Log("正在触发技能");
        if (skill == null)
        {
            Debug.LogWarning("未找到该技能:" + skill.name);
            return;
        }
        skill.OnCreate();// 当技能一开始被触发时调用
        if (!effectiveSkillList.Contains(skill))
        {
            effectiveSkillList.Add(skill);
            Debug.Log("添加进生效组:" + skill.name);
        }
        existedSkillList.Remove(skill);// 从可用技能组中移除
        Debug.Log("触发完毕");
    }
    // 改变sp
    public void consumSP(int cost)
    {
        curSp -= cost;
        // 对UI进行改变

    }

    void Update()
    {
        // 判断是否处于战斗状态
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
        // 绑定技能栏
        if (skillFields == null)
        {
            Debug.LogWarning("未找到技能栏UI");
            if (UIManager.Instance != null)
            {
                Debug.Log("正在查找技能栏UI");
                skillFields = UIManager.Instance.FindPanel(UIConst.FightUI).GetComponent<FightPanel>().SkillFields;
            }
            if (skillFields == null)
            {
                Debug.Log("查找失败");
                return;
            }
        }

        if (isPause)
        {
            Debug.Log("SkillManager暂停中");
            return;
        }

        // 判断是否触发技能
        if (existedSkillList.Count > 0)
        {
            Debug.Log("正在检测技能是否被触发");
            for (int i = 0; i < existedSkillList.Count;i++)
            {
                Debug.Log("检测技能:" + existedSkillList[i].name);
                // 当技能条件被满足时返回 true
                if(existedSkillList[i].OnTrigger())
                {
                    // 并执行技能逻辑
                    TriggerSkillEffect(existedSkillList[i]);
                }
            }
            Debug.Log("检测完毕");
        }

        //判断列表里是否有道具，有的话就调用所有道具的Update函数
        if (effectiveSkillList.Count > 0)
        {
            Debug.Log("正在调用技能");
            List<int> markIndex = new List<int>();
            int count = effectiveSkillList.Count;
            for (int i = 0; i < count; i++)
            {
                Debug.Log("调用技能Update:" + effectiveSkillList[i].name);
                effectiveSkillList[i].OnUpdate();
                if (effectiveSkillList[i].isUsed)//被使用了
                {
                    Debug.Log("调用技能Destory:" + effectiveSkillList[i].name);
                    effectiveSkillList[i].OnDestory();//消除道具效果
                    
                    markIndex.Add(i);
                }
            }
            Debug.Log("调用完毕，正在销毁已用完的技能");
            // 销毁道具
            for (int i = 0;i < markIndex.Count; i++)
            {
                BaseSkill skill = effectiveSkillList[markIndex[i]];
                effectiveSkillList.Remove(skill);// 从正在生效的技能组里剔除
                existedSkillList.Add(skill);// 加回可用技能组
            }
            Debug.Log("销毁完毕");
        }
    }


}


