using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class WebCamCapture : MonoBehaviour
{
    public RawImage webcamRawImage; // 原始攝影機畫面
    public RawImage capturedImage;  // 截圖顯示用
    public GameObject DisplayPhoto;
    public string path;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            CaptureScreenshot();
        }
    }
    public void CaptureScreenshot()
    {
        if (webcamRawImage.texture == null)
        {
            Debug.LogError("沒有攝影機畫面可截圖！");
            return;
        }

        // 取得攝影機畫面並轉換為 Texture2D
        Texture2D snap = new Texture2D(webcamRawImage.texture.width, webcamRawImage.texture.height);
        snap.SetPixels(((WebCamTexture)webcamRawImage.texture).GetPixels());
        snap.Apply();
        snap = FlipTexture(snap);

        // 儲存圖片到本機
        byte[] bytes = snap.EncodeToPNG();
        string PicFilePath = Path.Combine(Application.streamingAssetsPath);
        string PicFileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
         path = Path.Combine(PicFilePath, PicFileName);

        File.WriteAllBytes(path, bytes);
        Debug.Log("圖片儲存完成");
         Debug.Log("截圖儲存至：" + path);

        // 顯示截圖到另一個 UI RawImage（如果有的話）
         capturedImage.texture = snap;
        DisplayPhoto.SetActive(true);
    }
    private Texture2D FlipTexture(Texture2D original)
    {
        Texture2D flipped = new Texture2D(original.width, original.height);
        Color[] pixels = original.GetPixels();

        for (int y = 0; y < original.height; y++)
        {
            for (int x = 0; x < original.width; x++)
            {
               // flipped.SetPixel(x, original.height - y - 1, original.GetPixel(x, y)); // 上下翻轉
                flipped.SetPixel(original.width - x - 1, y, original.GetPixel(x, y)); // 左右翻轉

            }
        }

        flipped.Apply();
        return flipped;
    }
}
