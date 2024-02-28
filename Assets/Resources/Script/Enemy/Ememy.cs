using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy1 : MonoBehaviour
{
    //����ͷ����ui
    public TextMeshProUGUI enemyLabel;
    //ԭʼ����ĸ
    public string originalHealthLetters;
    //��Ϸ�����е���ĸ
    public string currentHealthLetters;
    public bool dead=false;
    void Start()
    {
        originalHealthLetters = GetInitialHealthLetters(); // ��ȡ��ʼ������ĸ����
        currentHealthLetters = originalHealthLetters;
        enemyLabel.text = currentHealthLetters;
    }
    //�����ⲿ��ʼ����ĸ
    public void SetInitialHealthLetters(string healthLetters)
    {
        originalHealthLetters = healthLetters;
        currentHealthLetters = healthLetters;
        enemyLabel.text = currentHealthLetters;
    }

    virtual public string GetInitialHealthLetters()
    {
        // �����ڲ���ʼ����ĸ
        return "BCX";
    }
    //�����ⲿ�жϵ����Ƿ��е�ǰ����������ĸ
    public bool HasLetter(char letter)
    {
        return currentHealthLetters.IndexOf(letter) >= 0;
    }
    //�ⲿ��Ҫ��������ĸ���룬���������ĸ
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
    //��������Χ��ָ���ĸ
    void ResetHealthLetters()
    {
        currentHealthLetters = originalHealthLetters;
        enemyLabel.text = currentHealthLetters;
    }

    public void OnDeath()
    {
        // ������������߼������磺
        Destroy(gameObject);
    }
}
