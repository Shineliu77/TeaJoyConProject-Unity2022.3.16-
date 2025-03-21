using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ㄏノ UI 秈兵

public class JoyConShakeProgress : MonoBehaviour
{

    public Slider progressBar; // 秈兵
    private int shakeCount = 0; // 璸计穘Ω计
    private float lastAccelY = 0f; // Ω硉 Y 禸计
    private float shakeThreshold = 0.5f; // 穘﹚霩
    private int totalShakes = 10; // 惠璶穘Ω计
    public GameObject Next_Obj;

    void Start()
    {
                // ﹍て秈兵
        if (progressBar != null)
            progressBar.value = 0;
    }

    void Update()
    {
        if (FindObjectOfType<JoyConConnect>().jc_ind != -1)
        {
            if (FindObjectOfType<JoyConConnect>().joycons == null || FindObjectOfType<JoyConConnect>().joycons.Count <= FindObjectOfType<JoyConConnect>().jc_ind)
                return;

            Joycon j = FindObjectOfType<JoyConConnect>().joycons[FindObjectOfType<JoyConConnect>().jc_ind];
            Vector3 accel = j.GetAccel(); // 眔硉

            // 盎代穘
            if (Mathf.Abs(accel.y - lastAccelY) > shakeThreshold)
            {
                shakeCount++;
                Debug.Log($"穘Ω计: {shakeCount}");
                UpdateProgressBar();
            }

            lastAccelY = accel.y;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shakeCount++;
            Debug.Log($"穘Ω计: {shakeCount}");
            UpdateProgressBar();
        }
    }

    void UpdateProgressBar()
    {
        if (progressBar != null)
        {
            progressBar.value = (float)shakeCount / totalShakes;
            if (shakeCount >= totalShakes)
            {
                Debug.Log("穘ЧΘ");
                gameObject.SetActive(false);
                Next_Obj.SetActive(true);
            }
        }
    }
}
