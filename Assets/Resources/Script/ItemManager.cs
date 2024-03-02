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
    
    public void isGetItem()//ÿ�ι�������Ҫ���õĺ���
    {
        //�ж��Ƿ�������

        int index01 = Random.Range(1, 21);
        if (index01 == 1)//�������
        {
            int idex02=Random.Range(1,11);
            if (idex02 == 1)//������˫
            {
                Item01 item01=new Item01();
                ItemList.Add(item01);
                item01.OnCreate();

            }
            else  if (idex02 == 2||idex02==3||idex02==4)//������ʯ
            {
                Item03 item03 = new Item03();
                ItemList.Add(item03);
                item03.OnCreate();
            }
            else //����ش�
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
        //�ж��б����Ƿ��е��ߣ��еĻ��͵������е��ߵ�Update����
        if (ItemList.Count > 0)
        {
            for (int i = 0; i < ItemList.Count; i++)
            {
                ItemList[i].OnUpdate();
                if (ItemList[i].isUsed)//��ʹ����
                {
                    ItemList[i].OnDestory();//��������Ч��
                    ItemList.RemoveAt(i);//ȥ������
                   
                }
            }
        }
    }
}
