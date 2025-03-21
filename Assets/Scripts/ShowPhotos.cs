using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPhotos : MonoBehaviour
{
    public GameObject NextObj;
    public GameObject PhotoPage;
    public GameObject Tip;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnEnable()
    {
        FindObjectOfType<WebCamCapture>().enabled = false;
    }
        // Update is called once per frame
        void Update()
    {
        if (FindObjectOfType<JoyConConnect>().jc_ind != -1)
        {
            if (FindObjectOfType<JoyConConnect>().joycons == null || FindObjectOfType<JoyConConnect>().joycons.Count <= FindObjectOfType<JoyConConnect>().jc_ind)
                return;
            Joycon j = FindObjectOfType<JoyConConnect>().joycons[FindObjectOfType<JoyConConnect>().jc_ind];

            if (j.GetButtonDown(Joycon.Button.DPAD_DOWN)) // B 鍵（右手把）
            {
                Next();
            }
            if (j.GetButtonDown(Joycon.Button.DPAD_UP)) // B 鍵（右手把）
            {
                ReCapture();
            }
        }
        if (Input.GetKeyDown(KeyCode.A)) // B 鍵（右手把）
        {
            ReCapture();
        }
        if (Input.GetKeyDown(KeyCode.Space)) // B 鍵（右手把）
        {
            Next();
        }
    }
    void Next()
    {
        Debug.Log("B 按鍵被按下");
        Tip.SetActive(true);
        StartCoroutine(Wait());
    }
    void ReCapture() {
        gameObject.SetActive(false);
        FindObjectOfType<WebCamCapture>().enabled = true;

    }
    IEnumerator Wait() {
        yield return new WaitForSeconds(0.3f);
        FindObjectOfType<GoogleApiUse>().N_UploadPic();
        yield return new WaitForEndOfFrame();
        PhotoPage.GetComponent<WebCamController>().StopCamera();
        PhotoPage.SetActive(false);
        gameObject.SetActive(false);
        NextObj.SetActive(true);
    }
}
