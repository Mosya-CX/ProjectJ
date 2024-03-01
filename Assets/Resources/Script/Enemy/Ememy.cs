using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //����ͷ����ui
    public TextMeshProUGUI enemyLabel;
    //ԭʼ����ĸ
    public string originalHealthLetters;
    //��Ϸ�����е���ĸ
    public string currentHealthLetters;
    //���ڻ���ȥ�����
    public Pool owner;
    //��ĸ����
    public int letterAmount;
    public int enemyType;
    public bool dead=false;
    //�˺�
    public int damage;
    public void Start()
    {
        InitializeHealthLetters(); // ��ȡ��ʼ������ĸ����
        enemyLabel.text = currentHealthLetters;
    }
    public void OnDisable()
    {
        
    }
    //�����ⲿ��ʼ����ĸ
    public void SetInitialHealthLetters(string healthLetters)
    {
        originalHealthLetters = healthLetters;
        currentHealthLetters = healthLetters;
        enemyLabel.text = currentHealthLetters;
    }
    //�ڲ���ʼ����ĸ
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
    //�����ⲿ�������ڲ��Ƿ��������ĸ
    public bool HasLetter(char letter)
    {
        return currentHealthLetters.IndexOf(letter) >= 0;
    }
    //�ⲿ��Ҫ��������ĸ���룬���������ĸ
    public void OnHit(char letter)
    {
        currentHealthLetters = currentHealthLetters.Replace(letter.ToString(), "");
        enemyLabel.text = currentHealthLetters;
        //AudioManager �ܻ���Ч
        //VFXManager �ܻ���Ч
        // �����ܻ�����

        if (string.IsNullOrEmpty(currentHealthLetters))
        {
            dead = true;
        }
    }
    //������ĸ������
    public void HighLightLetter(char letter)
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attack(collision);
    }
    //���๥������
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
    //��������Χ��ָ���ĸ
    public void ResetHealthLetters()
    {
        currentHealthLetters = originalHealthLetters;
        enemyLabel.text = currentHealthLetters;
    }

    public void OnDeath()
    {
        // ������������߼������磺
        owner.Return(this.gameObject);
    }
}
