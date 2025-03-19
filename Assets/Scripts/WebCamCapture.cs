using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class WebCamCapture : MonoBehaviour
{
    public RawImage webcamRawImage; // ��l��v���e��
    public RawImage capturedImage;  // �I����ܥ�
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
            Debug.LogError("�S����v���e���i�I�ϡI");
            return;
        }

        // ���o��v���e�����ഫ�� Texture2D
        Texture2D snap = new Texture2D(webcamRawImage.texture.width, webcamRawImage.texture.height);
        snap.SetPixels(((WebCamTexture)webcamRawImage.texture).GetPixels());
        snap.Apply();
        snap = FlipTexture(snap);

        // �x�s�Ϥ��쥻��
        byte[] bytes = snap.EncodeToPNG();
        string PicFilePath = Path.Combine(Application.streamingAssetsPath);
        string PicFileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
         path = Path.Combine(PicFilePath, PicFileName);

        File.WriteAllBytes(path, bytes);
        Debug.Log("�Ϥ��x�s����");
         Debug.Log("�I���x�s�ܡG" + path);

        // ��ܺI�Ϩ�t�@�� UI RawImage�]�p�G�����ܡ^
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
               // flipped.SetPixel(x, original.height - y - 1, original.GetPixel(x, y)); // �W�U½��
                flipped.SetPixel(original.width - x - 1, y, original.GetPixel(x, y)); // ���k½��

            }
        }

        flipped.Apply();
        return flipped;
    }
}
