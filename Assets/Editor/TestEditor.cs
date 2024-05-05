using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestEditor
{
    [MenuItem("测试/读取并加载测试关卡")]
    public static void ReadAndLoadTest()
    {
        LevelManager.Instance.LoadLevel(LevelPathConst.Test);
    }
    [MenuItem("测试/读取并加载剧情测试")]
    public static void ReadAndLoadStory()
    {
        LevelManager.Instance.LoadLevel(LevelPathConst.StoryTest);
    }
    [MenuItem("测试/读取关卡1数据并加载")]
    public static void ReadAndLoadLevelData()
    {
        LevelManager.Instance.LoadLevel(LevelPathConst.Level01Path);
    }
    [MenuItem("测试/提供敌人")]
    public static void Enemy()
    {
        EnemyManager.Instance.CreateEnemy01(new Vector3(0,0, 0));
        EnemyManager.Instance.CreateEnemy02(new Vector3(0, 2, 2));
        EnemyManager.Instance.CreateEnemy03(new Vector3(0, 4, 4));

    }
    [MenuItem("测试/打开战斗UI")]
    public static void OpenFightPanel()
    {
        UIManager.Instance.OpenPanel(UIConst.FightUI);
    }
    [MenuItem("测试/装载全部技能")]
    public static void EquipSkills()
    {
        SkillManager.Instance.AddSkill(SkillType.狂热标记);
        SkillManager.Instance.AddSkill(SkillType.无双);
        SkillManager.Instance.AddSkill(SkillType.此身为捷);
        SkillManager.Instance.AddSkill(SkillType.唯快不破);
        SkillManager.Instance.AddSkill(SkillType.安如磐石);
        SkillManager.Instance.AddSkill(SkillType.圆形环游);
    }
    [MenuItem("测试/触发全部技能")]
    public static void TriggerSkills()
    {
        List<BaseSkill> list = SkillManager.Instance.existedSkillList;
        while (list.Count > 0)
        {
            SkillManager.Instance.TriggerSkillEffect(list[0]);
        } 
    }
    [MenuItem("测试/增加十次Combos数")]
    public static void AddTenCombo()
    {
        for (int i = 0; i < 10; i++)
        {
            ComboManager.Instance.AddComboNum(1);
        }
    }

}
