using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoginPanel : BasePanel
{
    public TextMeshProUGUI PromptText;
    public AudioClip BGM;

    public int index;
    public float duration;
    public Coroutine coroutine;
    private void Awake()
    {
        duration = 5;
        PromptText = transform.Find("Prompt").GetComponent<TextMeshProUGUI>();
        BGM = Resources.Load<AudioClip>("Audio/主界面背景音乐+非战斗场景背景音乐（循环）");
        PromptText.color = Color.white;
        index = 0;
        
    }

    private void Start()
    {
        // 注册点击事件

        // 播放bgm
        Sound LoginUIBGM = new Sound();
        LoginUIBGM.names = "LoginBgm";
        LoginUIBGM.Clip = BGM;
        soundManager.Instance.musicSound.Add(LoginUIBGM);
        soundManager.Instance.PlayMusic("LoginBgm");
        soundManager.Instance.musicSource.loop = true;

        coroutine = StartCoroutine(ChangeColor());
    }

    private void Update()
    {
        

        if (Input.anyKeyDown)
        {
            StopCoroutine(coroutine);
            soundManager.Instance.musicSource.loop = false;
            soundManager.Instance.stopMusic();
            LevelManager.Instance.LoadLevel(LevelPathConst.Level01Path);
            UIManager.Instance.ClosePanel(UIConst.LoginUI);
        }   
    }
    private void OnDisable()
    {
        Destroy(this.gameObject);
    }
    IEnumerator ChangeColor()
    {
        while (true)
        {
            switch (index)
            {
                case 0:
                    PromptText.DOColor(Color.red, duration); index++; break;
                case 1:
                    PromptText.DOColor(Color.green, duration); index++; break;
                case 2:
                    PromptText.DOColor(Color.blue, duration); index++; break;
                case 3:
                    PromptText.DOColor(Color.white, duration); index = 0; break;

            }
            yield return new WaitForSecondsRealtime(duration);
        }
        
    }
}
