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
    //����ͷ����image���
    public List<Image> letterImages;
    //ԭʼ����ĸ
    public string originalHealthLetters;
    //��Ϸ�����е���ĸ
    public string currentHealthLetters;
    //��̬��ĸ�ֵ�
    public static Dictionary<char, Sprite> normalLetterDict;
    //������ĸ�ֵ�
    public static Dictionary<char, Sprite> highLightLetterDict;
    //���ڻ���ȥ�����
    public Pool owner;
    //��ĸ����
    public int letterAmount;
    public int enemyType;
    //���˽׶�
    public int enemyMaxPhase;
    public int enemyCurrentPhase;
    public bool CanExeCute => enemyCurrentPhase == enemyMaxPhase && isHighLight;
    public bool dead=false;
    public bool isHighLight=false;
    //�˺�
    public int damage;
    public void Awake()
    {
        InitDict();
        letterImages = new List<Image>();
    }
    public void Start()
    {
        InitializeHealthLetters(); // ��ȡ��ʼ������ĸ����
        //����ͼƬ֮�������
        //InitializeLetterImages();
        enemyLabel.text = currentHealthLetters;
    }
    public void OnDisable()
    {
        isHighLight = false;
        dead = false;
    }
    //�����ֵ�
    private static Sprite LoadSpriteFromResources(string path)
    {
        return Resources.Load<Sprite>(path);
    }
    public void InitDict()
    {
        normalLetterDict = new Dictionary<char, Sprite>();
        highLightLetterDict = new Dictionary<char, Sprite>();
        //����·�������ֵ�
        for (char c = 'A'; c <= 'Z'; c++)
        {
            string normalPath = $"Textures/Normal/{c}.png";
            string highlightedPath = $"Textures/Highlighted/{c}.png";

            Sprite normalSprite = LoadSpriteFromResources(normalPath);
            Sprite highlightedSprite = LoadSpriteFromResources(highlightedPath);

            if (normalSprite != null && highlightedSprite != null)
            {
                normalLetterDict[c] = normalSprite;
                highLightLetterDict[c] = highlightedSprite;
            }
            else
            {
                Debug.LogError($"Failed to load sprites for letter {c}");
            }
        }
    }
    ////�����ⲿ��ʼ����ĸ
    //public void SetInitialHealthLetters(string healthLetters)
    //{
    //    originalHealthLetters = healthLetters;
    //    currentHealthLetters = healthLetters;
    //    enemyLabel.text = currentHealthLetters;
    //}
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
    //����originalletter��ʼ��ͼƬ
    public void InitializeLetterImages()
    {
        if (originalHealthLetters.Length != letterImages.Count)
        {
            Debug.LogError("The length of originalLetters does not match the number of letterImages.");
            return;
        }

        for (int i = 0; i < originalHealthLetters.Length; i++)
        {
            char currentLetter = originalHealthLetters[i];
            if (normalLetterDict.ContainsKey(currentLetter))
            {
                letterImages[i].sprite = normalLetterDict[currentLetter];
            }
            else
            {
                Debug.LogError($"No normal sprite found for letter: {currentLetter}");
            }
        }
    }
    //�����ⲿ�������ڲ��Ƿ��������ĸ
    public bool HasFirstLetter(char letter)
    {
        return currentHealthLetters.Length > 0 && currentHealthLetters[0] == letter;
    }
    //�ⲿ��Ҫ��������ĸ���룬���������ĸ
    public void OnHit(char letter)
    {
        currentHealthLetters = currentHealthLetters.Replace(letter.ToString(), "");
        enemyLabel.text = currentHealthLetters;
        //AudioManager �ܻ���Ч
        //VFXManager �ܻ���Ч
        // �����ܻ�����
        //����ͼƬ�˾������
        if (enemyCurrentPhase < enemyMaxPhase && isHighLight)
        {
            HighLightLetter(letter);
            ChangeToNextPhase();
        }
        else 
        {
            HighLightLetter(letter);
        }
    }
    //������ĸ
    public void HighLightLetter(char keyPressed)
    {
        if (HasFirstLetter(keyPressed))
        {
            // ������һλ��ĸ
            int index = originalHealthLetters.IndexOf(keyPressed);
            letterImages[index].sprite = highLightLetterDict[keyPressed];
            isHighLight = true;
        }
    }
    public void ChangeToNextPhase()
    {

        enemyCurrentPhase++;
        //��Ч
        //��Ч
        //���¸�original��ֵ
        InitializeHealthLetters();
        //����original���³�ʼ��ͼƬ
        InitializeLetterImages();
        isHighLight=false;
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
        isHighLight = false;
    }

    public void OnDeath()
    {
        // �������������߼������磺
        owner.Return(this.gameObject);
    }
}
