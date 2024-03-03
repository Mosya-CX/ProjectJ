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
    //敌人头顶的image组件
    public List<Image> letterImages;
    //原始的字母
    public string originalHealthLetters;
    //游戏过程中的字母
    public string currentHealthLetters;
    //常态字母字典
    public static Dictionary<char, Sprite> normalLetterDict;
    //高亮字母字典
    public static Dictionary<char, Sprite> highLightLetterDict;
    //用于回收去对象池
    public Pool owner;
    //字母数量
    public int letterAmount;
    public int enemyType;
    //敌人阶段
    public int enemyMaxPhase;
    public int enemyCurrentPhase;
    public bool CanExeute => enemyCurrentPhase == enemyMaxPhase && isHighLight;
    public bool dead=false;
    public bool isHighLight=false;
    //伤害
    public int damage;
    public void Awake()
    {
        InitDict();
        letterImages = new List<Image>();
    }
    public void Start()
    {
        InitializeHealthLetters(); // 获取初始生命字母序列
        //有了图片之后用这个
        //InitializeLetterImages();
        enemyLabel.text = currentHealthLetters;
    }
    public void OnDisable()
    {
        isHighLight = false;
        dead = false;
    }
    //加载字典
    private static Sprite LoadSpriteFromResources(string path)
    {
        return Resources.Load<Sprite>(path);
    }
    public void InitDict()
    {
        normalLetterDict = new Dictionary<char, Sprite>();
        highLightLetterDict = new Dictionary<char, Sprite>();
        //根据路径加载字典
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
    ////用于外部初始化字母
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
            int randomIndex = UnityEngine.Random.Range(0, alphabet.Length);
            randomLettersBuilder.Append(alphabet[randomIndex]);
        }
        Debug.Log(randomLettersBuilder.ToString());
        originalHealthLetters = randomLettersBuilder.ToString();
        currentHealthLetters = originalHealthLetters;
    }
    //根据originalletter初始化图片
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
    //留给外部检测敌人内部是否含有这个字母
    public bool HasFirstLetter(char letter)
    {
        return currentHealthLetters.Length > 0 && currentHealthLetters[0] == letter;
    }
    //外部将要消除的字母导入，清除敌人字母
    public void OnHit(char letter)
    {
        currentHealthLetters = currentHealthLetters.Replace(letter.ToString(), "");
        enemyLabel.text = currentHealthLetters;
        //AudioManager 受击音效
        //VFXManager 受击特效
        // 播放受击动画
        //等有图片了就用这个
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
    //高亮字母
    public void HighLightLetter(char keyPressed)
    {
        if (HasFirstLetter(keyPressed))
        {
            // 点亮第一位字母
            int index = originalHealthLetters.IndexOf(keyPressed);
            letterImages[index].sprite = highLightLetterDict[keyPressed];
            isHighLight = true;
        }
    }
    public void ChangeToNextPhase()
    {

        enemyCurrentPhase++;
        //特效
        //音效
        //重新给original赋值
        InitializeHealthLetters();
        //根据original重新初始化图片
        InitializeLetterImages();
        isHighLight=false;
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
        isHighLight = false;
    }

    public void OnDeath()
    {
        // 处理敌人死亡逻辑，例如：
        owner.Return(this.gameObject);
    }
}
