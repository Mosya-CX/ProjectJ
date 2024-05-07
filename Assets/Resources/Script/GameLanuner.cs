using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameLanuner : MonoBehaviour
{

    void Awake()
    {
        
        
        UIManager.Instance.OpenPanel(UIConst.LoginUI);

        UIManager.Instance.OpenPanel(UIConst.FightUI);
        UIManager.Instance.ClosePanel(UIConst.FightUI);

        GameManager.Instance.CreatePlayer();
    }
}
