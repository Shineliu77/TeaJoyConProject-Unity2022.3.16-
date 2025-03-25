﻿using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class WebCamCapture : MonoBehaviour
{
    public RawImage webcamRawImage; // 原始攝影機畫面
    public RawImage capturedImage;  // 截圖顯示用
    public GameObject DisplayPhoto;
    public string path;
    public GameObject infoText;

    public Camera targetCamera;
    private void Update()
    {
        if (FindObjectOfType<JoyConConnect>().jc_ind != -1)
        {
            if (FindObjectOfType<JoyConConnect>().joycons == null || FindObjectOfType<JoyConConnect>().joycons.Count <= FindObjectOfType<JoyConConnect>().jc_ind)
                return;
            Joycon j = FindObjectOfType<JoyConConnect>().joycons[FindObjectOfType<JoyConConnect>().jc_ind];

            if (j.GetButtonDown(Joycon.Button.DPAD_DOWN)) // B 鍵（右手把）
            {
                infoText.SetActive(false);
                CaptureScreenshot();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            infoText.SetActive(false);
            CaptureScreenshot();
        }
    }
    public void CaptureScreenshot()
    {
        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        targetCamera.targetTexture = rt;

        // 渲染畫面到 RenderTexture
        targetCamera.Render();

        // 將 RenderTexture 複製到 Texture2D
        RenderTexture.active = rt;
        Texture2D snap = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        snap.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        snap.Apply();

        // 恢復相機與 RenderTexture 設定
        targetCamera.targetTexture = null;
        RenderTexture.active = null;
        rt.Release();

        // Flip texture if needed
        //snap = FlipTexture(snap); // 如果有上下顛倒

        // 儲存
        string PicFilePath = Application.streamingAssetsPath;
        string PicFileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        path = Path.Combine(PicFilePath, PicFileName);
        byte[] bytes = snap.EncodeToPNG();
        File.WriteAllBytes(path, bytes);

        Debug.Log("✅ 截圖完成！儲存於：" + path);

        // 顯示截圖到另一個 UI RawImage（如果有的話）
        capturedImage.texture = snap;
        DisplayPhoto.SetActive(true);

    }
    /*  public void CaptureScreenshot()
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
      }*/
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

        for (int y = 0; y < original.height; y++)
        {
            for (int x = 0; x < original.width; x++)
            {
                 flipped.SetPixel(x, original.height - y - 1, original.GetPixel(x, y)); // 上下翻轉

            }
        }
        flipped.Apply();
        return flipped;
    }
}
