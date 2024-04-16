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
    public bool CanSkip=>skipArea&&!attackArea;
    public bool skipArea;
    public bool attackArea;
    public bool dead=false;
    //当前是否有高亮的字母
    public bool isHighLight=false;
    //伤害
    public int damage;
    //攻击频率
    public float attackFrequency = 1;
    private WaitForSeconds waitForAttackFrequency;
    public Coroutine attackCoroutine;
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
            string normalPath = "Img/Character/Normal/blue_" + c.ToString()  ;
            string highlightedPath = "Img/Character/Highlight/red_" + c.ToString() ;
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
                Debug.Log(gameObject.name);
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

    public void AttackEnter2D(Collider2D collider)
    {
        Debug.Log(1);
        if (collider.tag == "Player")
        {
            Debug.Log("enter");
            if (attackCoroutine == null)
            {
                Debug.Log("attack");
                attackCoroutine = StartCoroutine(Attack(collider));
                attackArea = true;
            }
        }
    }
    //
    public void AttackExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (attackCoroutine != null)
            {
                Debug.Log("exit attack");
                attackArea= false;
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }
    }
    public void SkipAreaEnter()
    {
        skipArea = true;
    }
    public void SkipAreaExit()
    {
        skipArea= false;
    }
    public IEnumerator Attack(Collider2D collision)
    {
        //如果玩家处于无敌状态也返回
        Debug.Log("enterAttack");
        var playerController = collision.gameObject.GetComponent<PlayerController>();
        if (playerController != null)
        {
            while(true)
            {
                playerController.OnHit(damage);
                Debug.Log(collision.name);
                yield return waitForAttackFrequency;
            }
        }
        else
        {
            Debug.Log(collision.name);
            Debug.Log("No PlayerController component found on the collided object.");
        }
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

    virtual public void OnDeath()
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
