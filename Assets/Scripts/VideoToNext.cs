using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Audio;
public class VideoToNext : MonoBehaviour
{
    public GameObject NextObj;  // �U�@�B�n��ܪ�����
    public VideoPlayer videoPlayer;  // �v������
    public AudioSource BGMAudio;
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
        BGMAudio.Play();
       // gameObject.SetActive(false);  // ���÷�e����
    }
}
