using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    // �򿪽���
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    // �رս���
    public virtual void Close()
    {
        gameObject.SetActive(false);
    }


}
