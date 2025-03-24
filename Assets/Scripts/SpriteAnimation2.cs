using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpriteAnimation2 : MonoBehaviour
{
    public Sprite[] frames; // 動畫幀圖陣列
    public float frameRate = 0.1f; // 每幀間隔時間
    public bool loop = true; // 是否循環播放
    public int steps = 5; // 分成多少步驟播放
    public System.Action<int> onStepEnd; // 每個步驟結束後的回調函數
    public System.Action onAnimationEnd; // 動畫完全結束後的回調函數

    private Image imageRenderer; // UI 圖像
    private SpriteRenderer spriteRenderer; // 2D 物件的 SpriteRenderer
    private int currentFrame = 0;
    //public int currentStep = 0;
    private int framesPerStep;

    void Start()
    {
        // 嘗試獲取 SpriteRenderer 或 Image 組件
        spriteRenderer = GetComponent<SpriteRenderer>();
        imageRenderer = GetComponent<Image>();

        // 計算每個步驟應該播放多少幀
        framesPerStep = frames.Length / steps;

     
    }
    public void Play()
    {
        // 開始播放動畫
        StartCoroutine(PlayAnimation());
    }
    IEnumerator PlayAnimation()
    {
        while (true)
        {
            if (frames.Length > 0)
            {
                if (spriteRenderer != null)
                    spriteRenderer.sprite = frames[currentFrame];
                else if (imageRenderer != null)
                    imageRenderer.sprite = frames[currentFrame];

                currentFrame++;

                // 檢查是否達到步驟的最後一幀
                if (currentFrame % framesPerStep == 0 || currentFrame >= frames.Length)
                {
                    onStepEnd?.Invoke(FindObjectOfType<JoyconRotationTracker>().rotationCount); // 觸發步驟結束事件
                   // currentStep++;
                    StopCoroutine(PlayAnimation());
                }

                if (currentFrame >= frames.Length)
                {
                    if (loop)
                    {
                        currentFrame = 0; // 重新播放
                        //currentStep = 0;
                    }
                    else
                    {
                        onAnimationEnd?.Invoke(); // 動畫完全結束
                        yield break; // 停止協程
                    }
                }
            }
            yield return new WaitForSeconds(frameRate);
        }
    }
}
