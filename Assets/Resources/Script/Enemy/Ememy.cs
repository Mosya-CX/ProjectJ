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
    //无双图片
    public static Sprite matchLessNormalSprite;
    public static Sprite matchLessHighLightSprite;
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
    //是否处于无双状态
    public bool IsMathchlessMode => currentHealthLetters[0] == '~';
    public bool dead=false;
    //当前是否有高亮的字母
    public bool isHighLight=false;
    //伤害
    public int damage;
    //攻击频率
    public float attackFrequency = 1;
    private WaitForSeconds waitForAttackFrequency;
    virtual public void Awake()
    {
        InitDict();
    }
    public void OnEnable()
    {
        enemyCurrentPhase = 1;
    }
    public void Start()
    {
        //初始化无双图片
        string normalPath = "Img/Character/Normal/" + '~';
        string highlightedPath = "Img/Character/Highlight/" + '~';
        matchLessNormalSprite = Resources.Load<Sprite>(normalPath);
        matchLessHighLightSprite = Resources.Load<Sprite>(highlightedPath);
        InitializeHealthLetters(); // 内部初始化字母
        //根据字母初始化图片
        InitializeLetterImages();
        waitForAttackFrequency = new WaitForSeconds(attackFrequency);
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
    virtual public void InitializeLetterImages()
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
    virtual public void HighLightLetter(char keyPressed)
    {
        Debug.Log("enter3");
        int index = originalHealthLetters.IndexOf(keyPressed);
        letterImages[index].sprite = highLightLetterDict[keyPressed];
        isHighLight = true;
        currentHealthLetters = currentHealthLetters.Replace(keyPressed.ToString(), "");
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
        if(collision.tag=="Player")
        {
            StartCoroutine(Attack(collision));
        }
    }
    //
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StopCoroutine(Attack(other));
        }
    }
    public IEnumerator Attack(Collider2D collision)
    {
        //如果玩家处于无敌状态也返回
        var playerController = collision.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.OnHit(damage);
        }
        else
        {
            Debug.Log(collision.name);
            Debug.Log("No PlayerController component found on the collided object.");
        }
        yield return waitForAttackFrequency;
    }
    virtual public void ResetImage()
    {
        currentHealthLetters = originalHealthLetters;
        isHighLight = false;
        InitializeLetterImages();
    }
    virtual public void ChangeToMatchlessMode()
    {

    }
    virtual public void ChangeToNormalMode()
    {

    }

    public void OnDeath()
    {
        // 斩杀时调用
        if(EnemyManager.Instance.enemyList.Contains(this))
        {
            EnemyManager.Instance.RemoveFromList(this);
        }
        else
        {
            Debug.Log("remove error");
        }
        owner.Return(this.gameObject);
    }
}
