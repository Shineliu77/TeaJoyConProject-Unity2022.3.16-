using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TVC : MonoBehaviour
{
    public GameObject NextObj;
    public VideoPlayer videoPlayer;  // 影片播放器
    bool isStop, SecondIsStop;
    public float VideoStopSec;
    public float VideoStopRate;
    void Start()
    {
    }

    void Update()
    {
        if (videoPlayer.time > VideoStopSec && !isStop) {
            videoPlayer.Pause();
            isStop= true;
        }
        if (videoPlayer.time / videoPlayer.length > VideoStopRate&&!SecondIsStop)
        {
            videoPlayer.Pause();
            SecondIsStop = true;
        }
            if (FindObjectOfType<JoyConConnect>().jc_ind != -1)
        {
            if (FindObjectOfType<JoyConConnect>().joycons == null || FindObjectOfType<JoyConConnect>().joycons.Count <= FindObjectOfType<JoyConConnect>().jc_ind)
                return;
            Joycon j = FindObjectOfType<JoyConConnect>().joycons[FindObjectOfType<JoyConConnect>().jc_ind];

            if (j.GetButtonDown(Joycon.Button.DPAD_DOWN)) // B 鍵（右手把）
            {
                if (isStop&&!SecondIsStop) {
                    videoPlayer.Play();
                }
                if (isStop && SecondIsStop)
                {
                  Next();
                }
                
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) // B 鍵（右手把）
        {
            Next();
        }
    }
  
    void Next() {
        Debug.Log("B 按鍵被按下");
        gameObject.SetActive(false);
        NextObj.SetActive(true);
    }
}
