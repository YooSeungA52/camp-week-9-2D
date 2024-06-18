using System.Collections;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    public TextMeshProUGUI CoinText;
    public int Coin = 0;
    public int ClickReward = 1; // 클릭 당 보상
    public float AutoClickTime = 3.0f; // 자동 클릭 주기
    public int count = 0;

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
        CoinText.text = Coin.ToString();
    }

    public void OnClick()
    {
        anim.SetTrigger("isClick");
        AudioManager.Instance.PlayClickSound();
        count++;
        Coin += ClickReward;
        UpdateCoinText();
    }

    public void StartAutoClick() // 자동 클릭
    {
        StartCoroutine(AutoClickCoroutine());
    }

    IEnumerator AutoClickCoroutine()
    {
        while (true)
        {
            Debug.Log("AutoClick 일했다!");
            yield return new WaitForSeconds(AutoClickTime);
            anim.SetTrigger("isClick");
            AudioManager.Instance.PlayClickSound();
            count++;
            Coin += ClickReward;
            UpdateCoinText();
        }
    }

    public void IncreaseClickReward(int amount) // 클릭 보상 증가
    {
        ClickReward += amount;
    }

    public void DecreaseClickReward(float autoTime) // 자동 클릭 시간 감소
    {
        AutoClickTime -= autoTime;
    }
}
