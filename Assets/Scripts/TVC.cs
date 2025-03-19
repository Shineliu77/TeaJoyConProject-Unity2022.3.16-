using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVC : MonoBehaviour
{
    public GameObject NextObj;

    void Start()
    {
        
    }

    void Update()
    {
        if (FindObjectOfType<JoyConConnect>().jc_ind != -1)
        {
            if (FindObjectOfType<JoyConConnect>().joycons == null || FindObjectOfType<JoyConConnect>().joycons.Count <= FindObjectOfType<JoyConConnect>().jc_ind)
                return;
            Joycon j = FindObjectOfType<JoyConConnect>().joycons[FindObjectOfType<JoyConConnect>().jc_ind];

            if (j.GetButtonDown(Joycon.Button.DPAD_DOWN)) // B 鍵（右手把）
            {
                Next();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) // B 鍵（右手把）
        {
            Next();
        }
    }
    void Next() {
        Debug.Log("B 按鍵被按下");
        gameObject.SetActive(false);
        NextObj.SetActive(true);
    }
}
