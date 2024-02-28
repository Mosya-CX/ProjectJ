using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy1 : MonoBehaviour
{
    public TextMeshProUGUI enemyLabel;
    public string originalHealthLetters;
    public string currentHealthLetters;
    public bool dead=false;
    void Start()
    {
        originalHealthLetters = GetInitialHealthLetters(); // 获取初始生命字母序列
        currentHealthLetters = originalHealthLetters;
        enemyLabel.text = currentHealthLetters;
    }

    public void SetInitialHealthLetters(string healthLetters)
    {
        originalHealthLetters = healthLetters;
        currentHealthLetters = healthLetters;
        enemyLabel.text = currentHealthLetters;
    }

    private string GetInitialHealthLetters()
    {
        // 示例返回ABC，实际项目中请替换为合适的获取方式
        return "BCX";
    }

    public bool HasLetter(char letter)
    {
        return currentHealthLetters.IndexOf(letter) >= 0;
    }

    public void AttackWithLetter(char letter)
    {
        currentHealthLetters = currentHealthLetters.Replace(letter.ToString(), "");
        enemyLabel.text = currentHealthLetters;

        if (string.IsNullOrEmpty(currentHealthLetters))
        {
            dead = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag=="AttackArea")
        {
            ResetHealthLetters();
        }
    }

    void ResetHealthLetters()
    {
        currentHealthLetters = originalHealthLetters;
        enemyLabel.text = currentHealthLetters;
    }

    public void OnDeath()
    {
        // 处理敌人死亡逻辑，例如：
        Destroy(gameObject);
    }
}
