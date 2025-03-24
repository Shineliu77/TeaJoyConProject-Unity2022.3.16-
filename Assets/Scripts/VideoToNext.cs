using UnityEngine;
using UnityEngine.Video;

public class VideoToNext : MonoBehaviour
{
    public GameObject NextObj;  // �U�@�B�n��ܪ�����
    public VideoPlayer videoPlayer;  // �v������

    // Start is called before the first frame update
    void Start()
    {
        // �T�O�v�����񵲧���Ĳ�o�ƥ�
        videoPlayer.loopPointReached += OnVideoFinished;
        NextObj.SetActive(false);  // �U�@�B�����l������
    }

    // �v�����񧹲���Ĳ�o�����
    void OnVideoFinished(VideoPlayer vp)
    {
        NextObj.SetActive(true);  // ��ܤU�@�Ӫ���
       // gameObject.SetActive(false);  // ���÷�e����
    }
}
