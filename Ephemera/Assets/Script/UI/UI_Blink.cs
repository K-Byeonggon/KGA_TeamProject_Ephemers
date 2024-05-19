using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Blink : MonoBehaviour
{
    [SerializeField]
    private Image uiElement; // ������ UI ��� (Text, Image ��)
    public float blinkInterval = 0.5f; // ������ ���� (��)
    private float timer;
    private bool isVisible = true;

    void Start()
    {
        if (uiElement == null)
        {
            uiElement = GetComponent<Image>();
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= blinkInterval)
        {
            isVisible = !isVisible;
            uiElement.color = UI_Color.CautionColor;
            timer = 0f;
        }
    }
}
