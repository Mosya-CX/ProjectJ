using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class TestPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
           TimeManager.TimeOut(true);//combo导致的暂停\
            ComboManager.AddComboNum();//攻击成功
            print("dkjasjfkdl");
        }
        else if (UnityEngine.Input.GetMouseButtonDown(1))
        {
            TimeManager.TimeOut(false); //不是combo导致的暂停
        }
    }
}
