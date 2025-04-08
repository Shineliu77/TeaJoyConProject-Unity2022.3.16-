using UnityEngine;
using UnityEngine.UI;

public class WebCamController : MonoBehaviour
{
    public RawImage rawImage; // �즲 UI �� RawImage
    private WebCamTexture webCamTexture;
 
    private void Awake()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        // �ˬd�˸m�O�_����v��
        if (WebCamTexture.devices.Length > 0)
        {
            // �L�X�Ҧ��i����v���W��
            for (int i = 0; i < devices.Length; i++)
            {
                Debug.Log("��v�� " + i + ": " + devices[i].name);
            }

            // ���w�A�Q�Ϊ���v���]�o�̿��1�ӥ~�����Y�A�i��O index 1�^
            string selectedCameraName = devices[1].name; // �令�A�n�� index
            webCamTexture = new WebCamTexture(selectedCameraName);
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
