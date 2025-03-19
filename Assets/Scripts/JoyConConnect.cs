using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoyConConnect : MonoBehaviour
{
    public List<Joycon> joycons;
    public int jc_ind = -1; // 目前使用的 Joy-Con 索引
    public Text Info;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        joycons = JoyconManager.Instance.j;

        if (joycons == null || joycons.Count == 0)
        {
            Debug.LogWarning("No Joy-Cons detected, destroying gameObject.");
            Info.text = "沒有Joy-Cons";
            return;
        }

        // 判斷目前是左手還是右手
        for (int i = 0; i < joycons.Count; i++)
        {
            if (!joycons[i].isLeft) // 右手把
            {
                jc_ind = i;
                Debug.Log("已偵測到右手 Joy-Con");
                Info.text = "已偵測到右手 Joy-Con";
                ChangeScene();

                break;
            }
            if (joycons[i].isLeft) // 左手把
            {
                jc_ind = i;
                Debug.Log("已偵測到左手 Joy-Con");
                Info.text = "已偵測到左手 Joy-Con";

                ChangeScene();
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter)) {
            ChangeScene();
        }
    }
    void ChangeScene()
    {
        Application.LoadLevel("Scene1");

    }
}
