using UnityEngine;
using TMPro; // 如果有 UI 文字顯示次數

public class JoyconRotationTracker : MonoBehaviour
{
    public GameObject cube;  // Unity 中的 Cube 物件
    public TextMeshProUGUI rotationText; // UI 顯示旋轉次數（可選）

    private JoyConConnect joyconConnect;
    private float totalYRotation = 0f; // Cube 累計 Y 軸旋轉角度
    public int rotationCount = 0;     // 記錄完整旋轉次數
    private bool hasCompleted = false; // 是否已完成 5 次旋轉
    private float prevZRotation = 0f;  // 上一幀的 Z 軸角度（Joy-Con）
    private float rotationSpeed = 5f;  // 平滑旋轉速度（可調整）
    public GameObject Next_Obj;
    public Animator AnimatorObj;
    void Start()
    {
        joyconConnect = FindObjectOfType<JoyConConnect>();
    }

    void Update()
    {
        if (joyconConnect.jc_ind != -1)
        {


            if (joyconConnect.joycons == null || joyconConnect.joycons.Count <= joyconConnect.jc_ind)
                return;

            Joycon j = joyconConnect.joycons[joyconConnect.jc_ind];

            // 取得 Joy-Con 旋轉數據（四元數）
            Quaternion orientation = j.GetVector();
            Vector3 euler = orientation.eulerAngles; // 轉換為歐拉角
            float zRotation = NormalizeAngle(euler.z); // 修正 Z 軸角度（Joy-Con 左右翻轉）

            // 計算 Z 軸的旋轉變化量，並轉換為影響 Cube Y 軸
            float deltaRotation = Mathf.DeltaAngle(prevZRotation, zRotation);
            totalYRotation += deltaRotation; // 影響 Cube 的 Y 軸旋轉

            // 限制 totalYRotation 在 180° 以內，避免過度旋轉
            if (totalYRotation >= 180f || totalYRotation <= -180f)
            {
                totalYRotation = 0f;
                rotationCount++; // 增加旋轉次數
                AnimatorObj.SetTrigger("Count");
                Debug.Log($"旋轉次數：{rotationCount}/5");

                // 更新 UI 顯示旋轉次數
                if (rotationText != null)
                    rotationText.text = $"旋轉次數: {rotationCount}/5";

                // 旋轉 5 次後變色
                if (rotationCount >= 5 && !hasCompleted)
                {
                    hasCompleted = true;
                    Debug.Log("🎉 Joy-Con 旋轉 5 次完成！");
                    cube.GetComponent<Renderer>().material.color = Color.green;
                    Finish();
                }
            }

            // **平滑轉動，避免 Y 軸亂跳**
            float smoothYRotation = Mathf.LerpAngle(cube.transform.localEulerAngles.y, totalYRotation, Time.deltaTime * rotationSpeed);

            // 讓 Cube 只沿 Y 軸旋轉
            if (cube != null)
            {
                cube.transform.localRotation = Quaternion.Euler(0, smoothYRotation, 0);
            }

            prevZRotation = zRotation; // 更新上一幀角度
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Finish();
        }
    }

    // **修正角度範圍到 -180° ~ 180°，避免錯誤跳變**
    float NormalizeAngle(float angle)
    {
        return (angle > 180f) ? angle - 360f : angle;
    }

    void Finish()
    {
        gameObject.SetActive(false);
        Next_Obj.SetActive(true);
    }
}
