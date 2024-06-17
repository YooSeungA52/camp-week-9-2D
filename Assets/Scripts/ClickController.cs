using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    public TextMeshProUGUI CoinText;
    public int Coin = 0;
    public int ClickReward = 1; // Ŭ�� �� ����
    public float AutoClickTime = 3.0f; // �ڵ� Ŭ�� �ֱ�

    [SerializeField] private int count = 0;

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateCoinText();
    }

    void UpdateCoinText()
    {
        CoinText.text = "coin: " + Coin.ToString();
    }

    public void OnClick()
    {
        anim.SetTrigger("isClick");
        count++;
        Coin += ClickReward;
        UpdateCoinText();
    }

    public void StartAutoClick() // �ڵ� Ŭ��
    {
        StartCoroutine(AutoClickCoroutine());
    }

    IEnumerator AutoClickCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(AutoClickTime);
            anim.SetTrigger("isClick");
            count++;
            Coin += ClickReward;
            UpdateCoinText();
        }
    }

    public void IncreaseClickReward(int amount) // Ŭ�� ���� ����
    {
        ClickReward += amount;
    }

    public void DecreaseClickReward(float autoTime) // �ڵ� Ŭ�� �ð� ����
    {
        AutoClickTime -= autoTime;
    }
}
