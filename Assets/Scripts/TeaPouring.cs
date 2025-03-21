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
        // 記錄初始旋轉
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

            // 取得加速度計數據來判斷重力方向
            Vector3 gravity = j.GetAccel().normalized;

            // **1. 計算 X 軸前後傾斜角度 (茶壺嘴朝下)**
            //float tiltX = Mathf.Atan2(gravity.x, gravity.y) * Mathf.Rad2Deg;

            // **2. 計算 Z 軸左右傾角度 (往左或右倒茶)**
            float tiltZ = Mathf.Atan2(gravity.z, gravity.y) * Mathf.Rad2Deg;

            // **3. 限制最大角度，防止旋轉異常**
            //tiltX = Mathf.Clamp(tiltX, -90f, 90f);
            tiltZ = Mathf.Clamp(tiltZ, -45f, 45f); // 限制左右傾斜角度

            // **4. 設定最終旋轉**
            Quaternion targetRotation = Quaternion.Euler(0, 0, tiltZ);

            // **5. 平滑過渡，讓倒茶更流暢**
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
        

            if (Timer> SetTime&&transform.eulerAngles.z> Angle) // B 鍵（右手把）
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
