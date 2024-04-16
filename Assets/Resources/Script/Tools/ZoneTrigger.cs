using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public class ZoneTrigger : MonoBehaviour
{

    // 创建一个公开的自定义事件实例
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
        // 触发离开区域的自定义事件并传递碰撞器对象
        if (collider.tag == "Player")
            OnTriggerExitEvent?.Invoke(collider);
    }
}
