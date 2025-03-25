using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ㄏノ UI 秈兵

public class JoyConShakeProgress : MonoBehaviour
{

    public Slider progressBar; // 秈兵
    private int shakeCount = 0; // 璸计穘Ω计
    private float lastAccelY = 0f; // Ω硉 Y 禸计
    private float shakeThreshold =1f; // 穘﹚霩
    private int totalShakes = 10; // 惠璶穘Ω计
    public GameObject Next_Obj;

    public GameObject Ribbon1, Ribbon2;
    public Image BG;
    public Sprite[] BGSprites;

    public AudioSource FinishedSound;
    bool isPlaySound;
    public float WaitTime;
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
                Ribbon1.SetActive(false);
                Ribbon2.SetActive(true);
                BG.sprite = BGSprites[shakeCount-1];
                Debug.Log($"穘Ω计: {shakeCount}");
                UpdateProgressBar();
            }

            lastAccelY = accel.y;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shakeCount++;
            Ribbon1.SetActive(false);
            Ribbon2.SetActive(true);
            BG.sprite = BGSprites[shakeCount-1];
            Debug.Log($"穘Ω计: {shakeCount}");
            UpdateProgressBar();
        }
    }

    void UpdateProgressBar()
    {
        if (progressBar != null)
        {
            progressBar.value = (float)shakeCount / totalShakes;
            if (shakeCount >= totalShakes&&!isPlaySound)
            {
                Debug.Log("穘ЧΘ");
                FinishedSound.Play();
                isPlaySound=true;
                StartCoroutine(Finish());

            }
        }
    }

    IEnumerator Finish()
    {
        yield return new WaitForSeconds(WaitTime);
        gameObject.SetActive(false);
        Next_Obj.SetActive(true);
    }
}
