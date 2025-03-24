using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpriteAnimation : MonoBehaviour
{
    public Sprite[] frames; // 動畫幀圖陣列
    public float frameRate = 0.1f; // 每幀間隔時間
    public bool loop = true; // 是否循環播放

    private Image imageRenderer; // UI 圖像
    private SpriteRenderer spriteRenderer; // 2D 物件的 SpriteRenderer
    private int currentFrame = 0;

    public GameObject Next_Obj;

    void Start()
    {
        // 嘗試獲取 SpriteRenderer 或 Image 組件
        spriteRenderer = GetComponent<SpriteRenderer>();
        imageRenderer = GetComponent<Image>();

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

                if (currentFrame >= frames.Length)
                {
                    if (loop)
                        currentFrame = 0; // 重新播放
                    else
                    {
                        Next_Obj.SetActive(true);
                        yield break; // 停止協程
                    }
                }
            }
            yield return new WaitForSeconds(frameRate);
        }
    }
}
