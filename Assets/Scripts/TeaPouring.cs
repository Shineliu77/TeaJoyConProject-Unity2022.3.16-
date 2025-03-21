using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TeaPouring : MonoBehaviour
{
   
    private Quaternion initialRotation;
    public GameObject Next_Obj;

    public int SetTime;
    int Timer;
    public float Angle;
    void Start()
    {
        InvokeRepeating("Timers", 1, 1);
        // �O����l����
        initialRotation = transform.rotation;
    }
    void Timers()
    {
        Timer++;
    }
        void Update()
    {
        if (FindObjectOfType<JoyConConnect>().joycons.Count > 0)
        {
            Joycon j = FindObjectOfType<JoyConConnect>().joycons[FindObjectOfType<JoyConConnect>().jc_ind];

            // ���o�[�t�׭p�ƾڨӧP�_���O��V
            Vector3 gravity = j.GetAccel().normalized;

            // **1. �p�� X �b�e��ɱר��� (�����L�¤U)**
            //float tiltX = Mathf.Atan2(gravity.x, gravity.y) * Mathf.Rad2Deg;

            // **2. �p�� Z �b���k�ɨ��� (�����Υk�˯�)**
            float tiltZ = Mathf.Atan2(gravity.z, gravity.y) * Mathf.Rad2Deg;

            // **3. ����̤j���סA������ಧ�`**
            //tiltX = Mathf.Clamp(tiltX, -90f, 90f);
            tiltZ = Mathf.Clamp(tiltZ, -45f, 45f); // ����k�ɱר���

            // **4. �]�w�̲ױ���**
            Quaternion targetRotation = Quaternion.Euler(0, 0, tiltZ);

            // **5. ���ƹL��A���˯���y�Z**
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
        

            if (Timer> SetTime&&transform.eulerAngles.z> Angle) // B ��]�k���^
            {
                Finish();
            CancelInvoke();

        }


        if (Input.GetKeyDown(KeyCode.Space)) {
            Finish();
        }
    }
    void Finish()
    {
        gameObject.SetActive(false);
        Next_Obj.SetActive(true);
    }
}
