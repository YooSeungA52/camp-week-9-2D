using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAutoClickManager : MonoBehaviour
{
    //private ShopItem shopItem;

    [Header("ClickerBtn")]
    public ClickController clickController;

    public void StartBotAutoClick(ShopItem botItem, Animator anim) // 봇 자동 클릭
    {
        Debug.Log(botItem.name + "봇 자동 클릭 시작하세요");
        StartCoroutine(BotClickCoroutine(botItem, anim));
    }

    public IEnumerator BotClickCoroutine(ShopItem botItem, Animator anim)
    {
        while (true)
        {
            yield return new WaitForSeconds(botItem.AutoClickTime);
            Debug.Log(botItem.name + "일했다!");
            anim.SetTrigger("isClick");
            AudioManager.Instance.PlayClickSound();
            clickController.count++;
            clickController.Coin += botItem.ClickReward;
            UpdateCoinText();
        }
    }

    void UpdateCoinText()
    {
        clickController.CoinText.text = clickController.Coin.ToString();
    }
}
