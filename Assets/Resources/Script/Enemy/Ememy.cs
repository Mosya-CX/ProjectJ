using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy1 : MonoBehaviour
{
    //敌人头顶的ui
    public TextMeshProUGUI enemyLabel;
    //原始的字母
    public string originalHealthLetters;
    //游戏过程中的字母
    public string currentHealthLetters;
    public Pool owner;
    public int letterAmount;
    public bool dead=false;
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
    public bool HasLetter(char letter)
    {
        return currentHealthLetters.IndexOf(letter) >= 0;
    }
    //外部将要消除的字母导入，清除敌人字母
    public void OnHit(char letter)
    {
        currentHealthLetters = currentHealthLetters.Replace(letter.ToString(), "");
        enemyLabel.text = currentHealthLetters;

        if (string.IsNullOrEmpty(currentHealthLetters))
        {
            dead = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attack(collision);
    }
    virtual public void Attack(Collider2D collision)
    {

    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag=="AttackArea")
        {
            ResetHealthLetters();
        }
    }
    //出攻击范围后恢复字母
    void ResetHealthLetters()
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
