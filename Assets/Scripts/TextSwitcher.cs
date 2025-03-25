using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextSwitcher : MonoBehaviour
{
    public Text uiText; // ���V UI �� Text ����
    public float switchInterval = 1f; // �C�q��r���d�����

    private string[] messages = {
        "����QRCode��.",
        "����QRCode��..",
        "����QRCode��..."
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
