using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class StoryTellingPanel : BasePanel
{
    public Transform BlackBoardText;
    public TextMeshProUGUI DialogueDisplayText;
    public Transform Bg;
    public Transform PlayerAside;// Íæ¼ÒÅÔ°×
    public TextMeshProUGUI PlayerAsideText;
    public Transform DisplayArea;
    public Transform DisplayImg;

    private void Awake()
    {
        //UIManager.Instance.UIList.Add(this);

        Bg = transform.Find("Bg");
        BlackBoardText = transform.Find("Middle/DialogueText");
        DialogueDisplayText = BlackBoardText.GetComponent<TextMeshProUGUI>();
        PlayerAside = transform.Find("Bottom/PlayerAside");
        PlayerAsideText = PlayerAside.GetComponent<TextMeshProUGUI>();
        DisplayArea = transform.Find("DisplayArea");
        DisplayImg = DisplayArea.Find("Image");
        DisplayArea.gameObject.SetActive(false);
        DialogueDisplayText.text = "";
        PlayerAsideText.text = "";
    }

    
}
