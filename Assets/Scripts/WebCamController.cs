using UnityEngine;
using UnityEngine.UI;

public class WebCamController : MonoBehaviour
{
    public RawImage rawImage; // 拖曳 UI 的 RawImage
    private WebCamTexture webCamTexture;
 
    private void Awake()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        // 檢查裝置是否有攝影機
        if (WebCamTexture.devices.Length > 0)
        {
            // 印出所有可用攝影機名稱
            for (int i = 0; i < devices.Length; i++)
            {
                Debug.Log("攝影機 " + i + ": " + devices[i].name);
            }

            // 指定你想用的攝影機（這裡選第1個外接鏡頭，可能是 index 1）
            string selectedCameraName = devices[1].name; // 改成你要的 index
            webCamTexture = new WebCamTexture(selectedCameraName);
            rawImage.texture = webCamTexture;
            webCamTexture.Play(); // 開始播放攝影機畫面
        }
        else
        {
            Debug.LogError("沒有偵測到攝影機！");
        }
    }

    public void StopCamera()
    {
        if (webCamTexture != null && webCamTexture.isPlaying)
        {
            webCamTexture.Stop(); // 停止攝影機
        }
    }
}
