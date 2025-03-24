using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpriteAnimation2 : MonoBehaviour
{
    public Sprite[] frames; // �ʵe�V�ϰ}�C
    public float frameRate = 0.1f; // �C�V���j�ɶ�
    public bool loop = true; // �O�_�`������
    public int steps = 5; // �����h�֨B�J����
    public System.Action<int> onStepEnd; // �C�ӨB�J�����᪺�^�ը��
    public System.Action onAnimationEnd; // �ʵe���������᪺�^�ը��

    private Image imageRenderer; // UI �Ϲ�
    private SpriteRenderer spriteRenderer; // 2D ���� SpriteRenderer
    private int currentFrame = 0;
    //public int currentStep = 0;
    private int framesPerStep;

    void Start()
    {
        // ������� SpriteRenderer �� Image �ե�
        spriteRenderer = GetComponent<SpriteRenderer>();
        imageRenderer = GetComponent<Image>();

        // �p��C�ӨB�J���Ӽ���h�ִV
        framesPerStep = frames.Length / steps;

     
    }
    public void Play()
    {
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

                // �ˬd�O�_�F��B�J���̫�@�V
                if (currentFrame % framesPerStep == 0 || currentFrame >= frames.Length)
                {
                    onStepEnd?.Invoke(FindObjectOfType<JoyconRotationTracker>().rotationCount); // Ĳ�o�B�J�����ƥ�
                   // currentStep++;
                    StopCoroutine(PlayAnimation());
                }

                if (currentFrame >= frames.Length)
                {
                    if (loop)
                    {
                        currentFrame = 0; // ���s����
                        //currentStep = 0;
                    }
                    else
                    {
                        onAnimationEnd?.Invoke(); // �ʵe��������
                        yield break; // �����{
                    }
                }
            }
            yield return new WaitForSeconds(frameRate);
        }
    }
}
