using UnityEngine;
using UnityEngine.UI;

public class WebCamController : MonoBehaviour
{
    public RawImage rawImage; // �즲 UI �� RawImage
    private WebCamTexture webCamTexture;
    private void Awake()
    {
        
    }
    void OnEnable()
    {
     
        // �ˬd�˸m�O�_����v��
        if (WebCamTexture.devices.Length > 0)
        {
            webCamTexture = new WebCamTexture();
            rawImage.texture = webCamTexture;
            webCamTexture.Play(); // �}�l������v���e��
        }
        else
        {
            Debug.LogError("�S����������v���I");
        }
    }

    public void StopCamera()
    {
        if (webCamTexture != null && webCamTexture.isPlaying)
        {
            webCamTexture.Stop(); // ������v��
        }
    }
}
