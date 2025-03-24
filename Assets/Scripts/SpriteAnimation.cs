using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpriteAnimation : MonoBehaviour
{
    public Sprite[] frames; // �ʵe�V�ϰ}�C
    public float frameRate = 0.1f; // �C�V���j�ɶ�
    public bool loop = true; // �O�_�`������

    private Image imageRenderer; // UI �Ϲ�
    private SpriteRenderer spriteRenderer; // 2D ���� SpriteRenderer
    private int currentFrame = 0;

    public GameObject Next_Obj;

    void Start()
    {
        // ������� SpriteRenderer �� Image �ե�
        spriteRenderer = GetComponent<SpriteRenderer>();
        imageRenderer = GetComponent<Image>();

        // �}�l����ʵe
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
                        currentFrame = 0; // ���s����
                    else
                    {
                        Next_Obj.SetActive(true);
                        yield break; // �����{
                    }
                }
            }
            yield return new WaitForSeconds(frameRate);
        }
    }
}
