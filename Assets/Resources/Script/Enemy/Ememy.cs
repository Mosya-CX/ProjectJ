using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //敌人头顶的ui
    public TextMeshProUGUI enemyLabel;
    //原始的字母
    public string originalHealthLetters;
    //游戏过程中的字母
    public string currentHealthLetters;
    //用于回收去对象池
    public Pool owner;
    //字母数量
    public int letterAmount;
    public int enemyType;
    public bool dead=false;
    //伤害
    public int damage;
    public void Start()
    {
        InitializeHealthLetters(); // 获取初始生命字母序列
        enemyLabel.text = currentHealthLetters;
    }
    public void OnDisable()
    {
        
    }
    //用于外部初始化字母
    public void SetInitialHealthLetters(string healthLetters)
    {
        originalHealthLetters = healthLetters;
        currentHealthLetters = healthLetters;
        enemyLabel.text = currentHealthLetters;
    }
    //内部初始化字母
    virtual public void InitializeHealthLetters()
    {
        StringBuilder randomLettersBuilder = new StringBuilder();
        char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        for (int i = 0; i < letterAmount; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, alphabet.Length);
            randomLettersBuilder.Append(alphabet[randomIndex]);
        }
        Debug.Log(randomLettersBuilder.ToString());
        originalHealthLetters = randomLettersBuilder.ToString();
        currentHealthLetters = originalHealthLetters;
    }
    //留给外部检测敌人内部是否含有这个字母
    public bool HasLetter(char letter)
    {
        return currentHealthLetters.IndexOf(letter) >= 0;
    }
    //外部将要消除的字母导入，清除敌人字母
    public void OnHit(char letter)
    {
        currentHealthLetters = currentHealthLetters.Replace(letter.ToString(), "");
        enemyLabel.text = currentHealthLetters;
        //AudioManager 受击音效
        //VFXManager 受击特效
        // 播放受击动画

        if (string.IsNullOrEmpty(currentHealthLetters))
        {
            dead = true;
        }
    }
    //高亮字母，待办
    public void HighLightLetter(char letter)
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attack(collision);
    }
    //子类攻击函数
    virtual public void Attack(Collider2D collision)
    {

    }
    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.tag=="AttackArea")
    //    {
    //        ResetHealthLetters();
    //    }
    //}
    //出攻击范围后恢复字母
    public void ResetHealthLetters()
    {
        currentHealthLetters = originalHealthLetters;
        enemyLabel.text = currentHealthLetters;
    }

    public void OnDeath()
    {
        // 处理敌人死亡逻辑，例如：
        owner.Return(this.gameObject);
    }
}
