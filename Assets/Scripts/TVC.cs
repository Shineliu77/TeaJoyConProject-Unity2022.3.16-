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

            if (j.GetButtonDown(Joycon.Button.DPAD_DOWN)) // B ��]�k���^
            {
                Next();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) // B ��]�k���^
        {
            Next();
        }
    }
    void Next() {
        Debug.Log("B ����Q���U");
        gameObject.SetActive(false);
        NextObj.SetActive(true);
    }
}
