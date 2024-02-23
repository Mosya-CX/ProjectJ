using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    // 打开界面
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    // 关闭界面
    public virtual void Close()
    {
        gameObject.SetActive(false);
    }


}
