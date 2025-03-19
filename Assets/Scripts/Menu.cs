using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    public int SetTime;
    int Timer;
    public TextMeshProUGUI TimeText;
    public GameObject NextObj;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Time", 1, 1);
        Timer = SetTime;
        TimeText.text = Timer + "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Time() {
        Timer--;
        Timer = Mathf.Clamp(Timer, 0, Timer);
        TimeText.text = Timer + "";
        if (Timer == 0) {
            gameObject.SetActive(false);
            CancelInvoke();
            NextObj.SetActive(true);

        }
    }
}
