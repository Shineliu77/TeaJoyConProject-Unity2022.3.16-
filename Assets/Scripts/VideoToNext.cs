using UnityEngine;
using UnityEngine.Video;

public class VideoToNext : MonoBehaviour
{
    public GameObject NextObj;  // 下一步要顯示的物件
    public VideoPlayer videoPlayer;  // 影片播放器

    // Start is called before the first frame update
    void Start()
    {
        // 確保影片播放結束後觸發事件
        videoPlayer.loopPointReached += OnVideoFinished;
        NextObj.SetActive(false);  // 下一步物件初始為隱藏
    }

    // 影片播放完畢後觸發此函數
    void OnVideoFinished(VideoPlayer vp)
    {
        NextObj.SetActive(true);  // 顯示下一個物件
       // gameObject.SetActive(false);  // 隱藏當前物件
    }
}
