using UnityEngine;
using UnityEngine.UI;

public class WebCamController : MonoBehaviour
{
    public RawImage rawImage; // 拖曳 UI 的 RawImage
    private WebCamTexture webCamTexture;
    private void Awake()
    {
        
    }
    void OnEnable()
    {
     
        // 檢查裝置是否有攝影機
        if (WebCamTexture.devices.Length > 0)
        {
            webCamTexture = new WebCamTexture();
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
