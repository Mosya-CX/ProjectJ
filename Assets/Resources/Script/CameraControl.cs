using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
//使用了一个函数来切换跟随对象，
//也实装了要相机移动到某个position的函数
public class CameraControl : MonoBehaviour
{
    public static CameraControl Instance;
    public Vector3 Position;//记录要相机移动到的点
    private GameObject target;//记录相机要跟随的对象
    public GameObject Player;
    public float smooth;//柔滑度
    public GameObject curScene;

    // Start is called before the first frame update
    void Start()
    {
        target = Player;

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null&&curScene!=null)//跟随Target
        {
            if (transform.position != target.transform.position)
            {
               //对x和y坐标进行限制
                float x = Mathf.Clamp(target.transform.position.x, -curScene.GetComponent<SpriteRenderer>().sprite.bounds.size.x/2 + GameManager.Instance.viewWidth / 2,/*地图左边缘x加上一半摄像机x*/
                    curScene.GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2 - GameManager.Instance.viewWidth / 2/*地图右边缘x减去一半摄像机x*/);
                float y = Mathf.Clamp(target.transform.position.y, -curScene.GetComponent<SpriteRenderer>().bounds.size.x / 2 + GameManager.Instance.viewHeight / 2,/*地图下边缘y加上一半摄像机y*/
                      curScene.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2 - GameManager.Instance.viewHeight / 2/*地图上边缘y加去一半摄像机y */);
                //再进行lerp
                transform.position = Vector2.Lerp(transform.position, new Vector3(x, y, -10), smooth);
            }
        }
        if (target == null&&curScene!=null )//移动到某个指定点
        {

            if (transform.position != Position)
            {
                //对x和y坐标进行限制
                float x = Mathf.Clamp(target.transform.position.x, -curScene.GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2 + GameManager.Instance.viewWidth / 2,/*地图左边缘x加上一半摄像机x*/
                  curScene.GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2 - GameManager.Instance.viewWidth / 2/*地图右边缘x减去一半摄像机x*/);
                float y = Mathf.Clamp(target.transform.position.y, -curScene.GetComponent<SpriteRenderer>().bounds.size.x / 2 + GameManager.Instance.viewHeight / 2,/*地图下边缘y加上一半摄像机y*/
                      curScene.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2 - GameManager.Instance.viewHeight / 2/*地图上边缘y加去一半摄像机y */);
                //再进行lerp
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
    //更换相机跟随的对象
    public void ChangeTarget(GameObject Target)
    {
        if (Target != null)
        {
            Position = new Vector3(0, 0, -10);
            target = Target;
        }
    }

    //使相机跟随到某个position的函数
    public void ChangerPositionn(Vector2 changePosition)
    {
        target = null;
        Position = changePosition;

    }
    public void GetCursceneAndEnable(GameObject Curscene)//获取curscene，并且启动摄像机跟随
    {
        print("1213e");
        curScene = Curscene;
    }
    public void DeleteCursceneAndStop()//关闭摄像机跟随
    {
        curScene = null;
    }

}
