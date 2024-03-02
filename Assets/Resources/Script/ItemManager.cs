using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
    public List<BaseItem>  ItemList= new List<BaseItem>();
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            Instance = this;
        }
    }
    // Start is called before the first frame update
    
    public void isGetItem()//每次怪死亡需要调用的函数
    {
        //判断是否掉落道具

        int index01 = Random.Range(1, 21);
        if (index01 == 1)//掉落道具
        {
            int idex02=Random.Range(1,11);
            if (idex02 == 1)//掉落无双
            {
                Item01 item01=new Item01();
                ItemList.Add(item01);
                item01.OnCreate();

            }
            else  if (idex02 == 2||idex02==3||idex02==4)//掉落磐石
            {
                Item03 item03 = new Item03();
                ItemList.Add(item03);
                item03.OnCreate();
            }
            else //掉落回春
            {
                Item02 item02 = new Item02();
                ItemList.Add(item02);
                item02.OnCreate();
            }

        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //判断列表里是否有道具，有的话就调用所有道具的Update函数
        if (ItemList.Count > 0)
        {
            for (int i = 0; i < ItemList.Count; i++)
            {
                ItemList[i].OnUpdate();
                if (ItemList[i].isUsed)//被使用了
                {
                    ItemList[i].OnDestory();//消除道具效果
                    ItemList.RemoveAt(i);//去除道具
                   
                }
            }
        }
    }
}
