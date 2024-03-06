using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public List<Image> letterImages;
    //初始的字母
    public string originalHealthLetters;
    //当前状态的字母
    public string currentHealthLetters;
    //常态字母图片字典
    public static Dictionary<char, Sprite> normalLetterDict;
    //高亮字母图片字典
    public static Dictionary<char, Sprite> highLightLetterDict;
    //池子
    public Pool owner;
    //字母数量
    public int letterAmount;
    //敌人种类
    public int enemyType;
    //敌人阶段
    public int enemyMaxPhase;
    public int enemyCurrentPhase;
    //敌人是否可以斩杀
    public bool CanExeCute => enemyCurrentPhase == enemyMaxPhase && isHighLight;
    public bool dead=false;
    //当前是否有高亮的字母
    public bool isHighLight=false;
    //伤害
    public int damage;
    public void Awake()
    {
        InitDict();
    }
    public void OnEnable()
    {
        enemyCurrentPhase = 1;
    }
    public void Start()
    {
        InitializeHealthLetters(); // 内部初始化字母
        //根据字母初始化图片
        InitializeLetterImages();
    }
    public void OnDisable()
    {
        isHighLight = false;
        dead = false;
    }
    //初始化字典
    public void InitDict()
    {
        normalLetterDict = new Dictionary<char, Sprite>();
        highLightLetterDict = new Dictionary<char, Sprite>();
        for (char c = 'A'; c <= 'Z'; c++)
        {
            string normalPath = "Img/Character/Normal/" + c.ToString()  ;
            string highlightedPath = "Img/Character/Highlight/" + c.ToString() ;
            Sprite normalSprite = Resources.Load<Sprite>(normalPath);
            Sprite highlightedSprite = Resources.Load<Sprite>(highlightedPath);

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
    //外部初始化字母
    //public void SetInitialHealthLetters(string healthLetters)
    //{
    //    originalHealthLetters = healthLetters;
    //    currentHealthLetters = healthLetters;
    //    enemyLabel.text = currentHealthLetters;
    //}
    //内部初始化字母
    virtual public void InitializeHealthLetters()
    {
        StringBuilder randomLettersBuilder = new StringBuilder();
        char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        for (int i = 0; i < letterAmount; i++)
        {
            int randomIndex = Random.Range(0, alphabet.Length);
            randomLettersBuilder.Append(alphabet[randomIndex]);
        }
        originalHealthLetters = randomLettersBuilder.ToString();
        currentHealthLetters = originalHealthLetters;
    }
    //根据字母初始化图片
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
    //检测首字母是否是输入字母
    public bool HasFirstLetter(char letter)
    {
        return currentHealthLetters.Length > 0 && currentHealthLetters[0] == letter;
    }
    //受击
    public void OnHit(char letter)
    {
        if (!HasFirstLetter(letter))
        {
            return;
        }
        Debug.Log("enter");
        //enemyLabel.text = currentHealthLetters;
        //AudioManager 
        //VFXManager 
        //转换阶段
        if (enemyCurrentPhase < enemyMaxPhase && isHighLight)
        {
            Debug.Log("enter1");
            HighLightLetter(letter);
            ChangeToNextPhase();
        }
        else 
        {
            HighLightLetter(letter);
            Debug.Log("enter2");
        }
    }
    //点亮字母
    public void HighLightLetter(char keyPressed)
    {
        if (HasFirstLetter(keyPressed))
        {
            Debug.Log("enter3");
            int index = originalHealthLetters.IndexOf(keyPressed);
            letterImages[index].sprite = highLightLetterDict[keyPressed];
            isHighLight = true;
            currentHealthLetters = currentHealthLetters.Replace(keyPressed.ToString(), "");
        }
    }
    public void ChangeToNextPhase()
    {
        enemyCurrentPhase++;
        //特效
        //音效
        //重新初始化字母
        InitializeHealthLetters();
        //根据字母初始化图片
        InitializeLetterImages();
        isHighLight=false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attack(collision);
    }
    //
    virtual public void Attack(Collider2D collision)
    {

    }
    public void ResetImage()
    {
        currentHealthLetters = originalHealthLetters;
        isHighLight = false;
        InitializeLetterImages();
    }
    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.tag=="AttackArea")
    //    {
    //        ResetHealthLetters();
    //    }
    //}

    public void OnDeath()
    {
        // 斩杀时调用
        owner.Return(this.gameObject);
    }
}
