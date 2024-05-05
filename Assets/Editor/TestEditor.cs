using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestEditor
{
    [MenuItem("����/��ȡ�����ز��Թؿ�")]
    public static void ReadAndLoadTest()
    {
        LevelManager.Instance.LoadLevel(LevelPathConst.Test);
    }
    [MenuItem("����/��ȡ�����ؾ������")]
    public static void ReadAndLoadStory()
    {
        LevelManager.Instance.LoadLevel(LevelPathConst.StoryTest);
    }
    [MenuItem("����/��ȡ�ؿ�1���ݲ�����")]
    public static void ReadAndLoadLevelData()
    {
        LevelManager.Instance.LoadLevel(LevelPathConst.Level01Path);
    }
    [MenuItem("����/�ṩ����")]
    public static void Enemy()
    {
        EnemyManager.Instance.CreateEnemy01(new Vector3(0,0, 0));
        EnemyManager.Instance.CreateEnemy02(new Vector3(0, 2, 2));
        EnemyManager.Instance.CreateEnemy03(new Vector3(0, 4, 4));

    }
    [MenuItem("����/��ս��UI")]
    public static void OpenFightPanel()
    {
        UIManager.Instance.OpenPanel(UIConst.FightUI);
    }
    [MenuItem("����/װ��ȫ������")]
    public static void EquipSkills()
    {
        SkillManager.Instance.AddSkill(SkillType.���ȱ��);
        SkillManager.Instance.AddSkill(SkillType.��˫);
        SkillManager.Instance.AddSkill(SkillType.����Ϊ��);
        SkillManager.Instance.AddSkill(SkillType.Ψ�첻��);
        SkillManager.Instance.AddSkill(SkillType.������ʯ);
        SkillManager.Instance.AddSkill(SkillType.Բ�λ���);
    }
    [MenuItem("����/����ȫ������")]
    public static void TriggerSkills()
    {
        List<BaseSkill> list = SkillManager.Instance.existedSkillList;
        while (list.Count > 0)
        {
            SkillManager.Instance.TriggerSkillEffect(list[0]);
        } 
    }
    [MenuItem("����/����ʮ��Combos��")]
    public static void AddTenCombo()
    {
        for (int i = 0; i < 10; i++)
        {
            ComboManager.Instance.AddComboNum(1);
        }
    }

}
