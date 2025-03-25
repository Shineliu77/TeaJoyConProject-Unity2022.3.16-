using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // �ϥ� UI �i�ױ�

public class JoyConShakeProgress : MonoBehaviour
{

    public Slider progressBar; // �i�ױ�
    private int shakeCount = 0; // �p�Ʒn�̦���
    private float lastAccelY = 0f; // �W�@���[�t�ת� Y �b�ƭ�
    private float shakeThreshold =1f; // �n�̧P�w���H��
    private int totalShakes = 10; // �ݭn���n�̦���
    public GameObject Next_Obj;

    public GameObject Ribbon1, Ribbon2;
    public Image BG;
    public Sprite[] BGSprites;

    public AudioSource FinishedSound;
    bool isPlaySound;
    public float WaitTime;
    void Start()
    {
                // ��l�ƶi�ױ�
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
            Vector3 accel = j.GetAccel(); // ���o�[�t��

            // �����W�U�n��
            if (Mathf.Abs(accel.y - lastAccelY) > shakeThreshold)
            {
                shakeCount++;
                Ribbon1.SetActive(false);
                Ribbon2.SetActive(true);
                BG.sprite = BGSprites[shakeCount-1];
                Debug.Log($"�n�̦���: {shakeCount}");
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
            Debug.Log($"�n�̦���: {shakeCount}");
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
                Debug.Log("�n�̧����I");
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
