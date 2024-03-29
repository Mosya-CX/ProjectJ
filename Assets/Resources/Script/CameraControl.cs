using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
//ʹ����һ���������л��������
//Ҳʵװ��Ҫ����ƶ���ĳ��position�ĺ���
public class CameraControl : MonoBehaviour
{
    public static CameraControl Instance;
    public Vector3 Position;//��¼Ҫ����ƶ����ĵ�
    private GameObject target;//��¼���Ҫ����Ķ���
    public GameObject Player;
    public float smooth;//�Ử��
    public GameObject curScene;

    // Start is called before the first frame update
    void Start()
    {
        target = Player;

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null&&curScene!=null)//����Target
        {
            if (transform.position != target.transform.position)
            {
               //��x��y�����������
                float x = Mathf.Clamp(target.transform.position.x, -curScene.GetComponent<SpriteRenderer>().sprite.bounds.size.x/2 + GameManager.Instance.viewWidth / 2,/*��ͼ���Եx����һ�������x*/
                    curScene.GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2 - GameManager.Instance.viewWidth / 2/*��ͼ�ұ�Եx��ȥһ�������x*/);
                float y = Mathf.Clamp(target.transform.position.y, -curScene.GetComponent<SpriteRenderer>().bounds.size.x / 2 + GameManager.Instance.viewHeight / 2,/*��ͼ�±�Եy����һ�������y*/
                      curScene.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2 - GameManager.Instance.viewHeight / 2/*��ͼ�ϱ�Եy��ȥһ�������y */);
                //�ٽ���lerp
                transform.position = Vector2.Lerp(transform.position, new Vector3(x, y, -10), smooth);
            }
        }
        if (target == null&&curScene!=null )//�ƶ���ĳ��ָ����
        {

            if (transform.position != Position)
            {
                //��x��y�����������
                float x = Mathf.Clamp(target.transform.position.x, -curScene.GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2 + GameManager.Instance.viewWidth / 2,/*��ͼ���Եx����һ�������x*/
                  curScene.GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2 - GameManager.Instance.viewWidth / 2/*��ͼ�ұ�Եx��ȥһ�������x*/);
                float y = Mathf.Clamp(target.transform.position.y, -curScene.GetComponent<SpriteRenderer>().bounds.size.x / 2 + GameManager.Instance.viewHeight / 2,/*��ͼ�±�Եy����һ�������y*/
                      curScene.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2 - GameManager.Instance.viewHeight / 2/*��ͼ�ϱ�Եy��ȥһ�������y */);
                //�ٽ���lerp
                transform.position = Vector2.Lerp(transform.position, new Vector3(x, y, -10), smooth);
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
    public void GetCursceneAndEnable(GameObject Curscene)//��ȡcurscene�������������������
    {
        print("1213e");
        curScene = Curscene;
    }
    public void DeleteCursceneAndStop()//�ر����������
    {
        curScene = null;
    }

}
