using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;
using System;
using TMPro;

public class QRCodeGenerator : MonoBehaviour
{
    public RawImage qrCodeImage; // 要顯示 QR Code 的 RawImage
    public int qrWidth = 256; // QR Code 大小
    public int qrHeight = 256;

    public int SetTime;
    int Timer;
    public TextMeshProUGUI TimeText;

    void Start()
    {
        InvokeRepeating("Time", 1, 1);
        Timer = SetTime;
        TimeText.text = Timer + "";
    }

    private void OnEnable()
    {
       GenerateQRCode(FindObjectOfType<GoogleApiUse>().GoogleTextureUrl);

    }
    public void GenerateQRCode(string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            Debug.LogError("URL 不能為空！");
            return;
        }

        try
        {
            Texture2D qrTexture = GenerateQRTexture(url, qrWidth, qrHeight);
            qrCodeImage.texture = qrTexture;
        }
        catch (Exception e)
        {
            Debug.LogError("QR Code 產生失敗：" + e.Message);
        }
    }

    private Texture2D GenerateQRTexture(string text, int width, int height)
    {
        BarcodeWriter writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Width = width,
                Height = height,
                Margin = 1
            }
        };

        Color32[] pixels = writer.Write(text);
        Texture2D texture = new Texture2D(width, height);

        // 設定透明背景
        for (int i = 0; i < pixels.Length; i++)
        {
            if (pixels[i].r == 0 && pixels[i].g == 0 && pixels[i].b == 0) // 黑色部分
            {
                pixels[i] = new Color32(82, 117, 27, 255);  // 使用指定的前景色
            }
            else
            {
                pixels[i] = new Color32(0, 0, 0, 0); // 設為透明
            }
        }

        texture.SetPixels32(pixels);
        texture.Apply();
        return texture;
    }
    void Time()
    {
        Timer--;
        Timer = Mathf.Clamp(Timer, 0, Timer);
        TimeText.text = Timer + "";
        if (Timer == 0)
        {
            Application.LoadLevel(Application.loadedLevel);

        }
    }
}
