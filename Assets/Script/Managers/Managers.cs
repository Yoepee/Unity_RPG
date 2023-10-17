using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // 유일성이 보장
    // public static Managers GetInstance() { Init(); return s_instance; }
    public static Managers Instance { get { Init(); return s_instance; } }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    static void Init()
    {
        //초기화
        if (s_instance == null)
        {
            // Instance = this;
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            // Managers mg = go.GetComponent<Managers>();
            s_instance = go.GetComponent<Managers>();
        }
    }
}
