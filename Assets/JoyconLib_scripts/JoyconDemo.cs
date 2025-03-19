using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyconDemo : MonoBehaviour
{

    private List<Joycon> joycons;

    // Values made available via Unity
    public float[] stick;
    public Vector3 gyro;
    public Vector3 accel;
    public int jc_ind = 0;
    public Quaternion orientation;

    void Start()
    {
        gyro = new Vector3(0, 0, 0);
        accel = new Vector3(0, 0, 0);
        joycons = JoyconManager.Instance?.j;

        if (joycons == null || joycons.Count < jc_ind + 1)
        {
            Debug.LogWarning("Joycon not found, destroying gameObject.");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // 確保只處理一個 Joy-Con
        if (joycons == null || joycons.Count <= jc_ind)
            return;

        Joycon j = joycons[jc_ind];

        // 按鈕控制
        if (j.GetButtonDown(Joycon.Button.SHOULDER_2))
        {
            Debug.Log("Shoulder button 2 pressed");
            Debug.Log(string.Format("Stick x: {0:N} Stick y: {1:N}", j.GetStick()[0], j.GetStick()[1]));
            j.Recenter();
        }
        if (j.GetButtonUp(Joycon.Button.SHOULDER_2))
        {
            Debug.Log("Shoulder button 2 released");
        }
        if (j.GetButton(Joycon.Button.SHOULDER_2))
        {
            Debug.Log("Shoulder button 2 held");
        }

        // 震動控制
        if (j.GetButtonDown(Joycon.Button.DPAD_DOWN))
        {
            Debug.Log("Rumble");
            j.SetRumble(160, 320, 0.6f, 200);
        }

        // 更新數據
        stick = j.GetStick();
        gyro = j.GetGyro();
        accel = j.GetAccel();
        orientation = j.GetVector();

        // 控制物體顏色
        Renderer rend = gameObject.GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.color = j.GetButton(Joycon.Button.DPAD_UP) ? Color.red : Color.blue;
        }

        // 每300幀重置一次，防止漂移
        if (Time.frameCount % 300 == 0)  // 每300幀重置一次
        {
            j.Recenter();
        }

        // 控制物體的旋轉（只影響指定的 Joy-Con）
        // 僅使用 yaw 來控制物體的旋轉，忽略其他軸的漂移
        float yaw = orientation.eulerAngles.y; // 取得 Yaw 角度
        gameObject.transform.rotation = Quaternion.Euler(0, yaw, 0); // 只調整 Yaw 旋轉
    }
}
