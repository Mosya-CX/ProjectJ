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
           TimeManager.Instance.TimeOut(true);//combo���µ���ͣ\
            ComboManager.Instance.AddComboNum(1);//�����ɹ�
            soundManager.Instance.Playsfx("b");
            print("dkjasjfkdl");
        }
        else if (UnityEngine.Input.GetMouseButtonDown(1))
        {
            TimeManager.Instance.TimeOut(false); //����combo���µ���ͣ
        }

    }
}
