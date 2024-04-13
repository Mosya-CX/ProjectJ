using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class TestPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       // ComboManager.Instance.DisableCombo();
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
           TimeManager.Instance.TimeOut(true);//combo导致的暂停\
            ComboManager.Instance.AddComboNum(1);//攻击成功
            soundManager.Instance.Playsfx("b");
            print("dkjasjfkdl");
        }
        else if (UnityEngine.Input.GetMouseButtonDown(1))
        {
            TimeManager.Instance.TimeOut(false); //不是combo导致的暂停
        }

    }
}
