using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoyConConnect : MonoBehaviour
{
    public List<Joycon> joycons;
    public int jc_ind = -1; // �ثe�ϥΪ� Joy-Con ����
    public Text Info;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        joycons = JoyconManager.Instance.j;

        if (joycons == null || joycons.Count == 0)
        {
            Debug.LogWarning("No Joy-Cons detected, destroying gameObject.");
            Info.text = "�S��Joy-Cons";
            return;
        }

        // �P�_�ثe�O�����٬O�k��
        for (int i = 0; i < joycons.Count; i++)
        {
            if (!joycons[i].isLeft) // �k���
            {
                jc_ind = i;
                Debug.Log("�w������k�� Joy-Con");
                Info.text = "�w������k�� Joy-Con";
                ChangeScene();

                break;
            }
            if (joycons[i].isLeft) // �����
            {
                jc_ind = i;
                Debug.Log("�w�����쥪�� Joy-Con");
                Info.text = "�w�����쥪�� Joy-Con";

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
