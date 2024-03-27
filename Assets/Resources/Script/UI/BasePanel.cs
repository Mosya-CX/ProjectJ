using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    // �򿪽���
    public virtual void Open(string name)
    {
        this.name = name;
        gameObject.SetActive(true);
    }

    // �رս���
    public virtual void Close()
    {
        gameObject.SetActive(false);
    }


}
