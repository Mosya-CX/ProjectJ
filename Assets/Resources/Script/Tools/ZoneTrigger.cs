using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public class ZoneTrigger : MonoBehaviour
{

    // ����һ���������Զ����¼�ʵ��
    public UnityEvent<Collider2D> OnTriggerEnterEvent;
    public UnityEvent<Collider2D> OnTriggerExitEvent;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag=="Player")
        {
            Debug.Log(collider.gameObject.name);
            OnTriggerEnterEvent?.Invoke(collider);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        // �����뿪������Զ����¼���������ײ������
        if (collider.tag == "Player")
            OnTriggerExitEvent?.Invoke(collider);
    }
}
