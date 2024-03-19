using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
//ʹ����һ���������л��������
//Ҳʵװ��Ҫ����ƶ���ĳ��position�ĺ���
public class CameraControl : MonoBehaviour
{
    public static CameraControl Instance;
    private Vector3 Position;//��¼Ҫ����ƶ����ĵ�
    private GameObject target;//��¼���Ҫ����Ķ���
    public GameObject Player;
    public float smooth;//�Ử��

    // Start is called before the first frame update
    void Start()
    {
        target = Player;
        

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)//����Target
        {
            if (transform.position != target.transform.position)
            {
                //��x��y�����������
                //float x=Mathf.Clamp(target.transform.position.x����ͼ���Եx����һ�������x����ͼ�ұ�Եx��ȥһ�������x);
                //float y=Mathf.Clamp(target.transform.position.y����ͼ�±�Եy����һ�������y����ͼ�ϱ�Եy��ȥһ�������y);
                //�ٽ���lerp
                //transform.position = Vector2.Lerp(transform.position, new Vector3(x,y,-10), smooth);
            }
        }
        if (target == null )//�ƶ���ĳ��ָ����
        {
            
            if(transform.position != Position)
            {
                //��x��y�����������
                //float x=Mathf.Clamp(Position.x����ͼ���Եx����һ�������x����ͼ�ұ�Եx��ȥһ�������x);
                //float y=Mathf.Clamp(Position.y����ͼ�±�Եy����һ�������y����ͼ�ϱ�Եy��ȥһ�������y);
                //�ٽ���lerp
                //transform.position = Vector2.Lerp(transform.position, new Vector3(x,y,-10), smooth);
            }
        }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }
    //�����������Ķ���
    public void ChangeTarget(GameObject Target)
    {
        if (Target != null)
        {
            Position = new Vector3(0, 0, -10);
            target = Target;
        }
    }

    //ʹ������浽ĳ��position�ĺ���
    public void ChangerPositionn(Vector2 changePosition)
    {
        target = null;
        Position = changePosition;

    }

}
