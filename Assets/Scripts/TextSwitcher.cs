using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextSwitcher : MonoBehaviour
{
    public Text uiText; // 指向 UI 的 Text 元件
    public float switchInterval = 1f; // 每段文字停留的秒數

    private string[] messages = {
        "產生QRCode中.",
        "產生QRCode中..",
        "產生QRCode中..."
    };

    private int currentIndex = 0;

    void Start()
    {
        StartCoroutine(SwitchText());
    }

    IEnumerator SwitchText()
    {
        while (true)
        {
            uiText.text = messages[currentIndex];
            currentIndex = (currentIndex + 1) % messages.Length;
            yield return new WaitForSeconds(switchInterval);
        }
    }
}
