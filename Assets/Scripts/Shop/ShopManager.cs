using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class ShopManager : MonoBehaviour
{
    public ShopItem clickUpgradeItem; // 기본 클릭 아이템
    public ShopItem autoClickUpgradeItem; // 자동 클릭 아이템

    public TextMeshProUGUI ClickPriceText;  // 기본 클릭 업그레이드 비용
    public TextMeshProUGUI AutoClickPriceText; // 자동 클릭 업그레이드 비용
    public TextMeshProUGUI CurrentClickCoinTxt;  // 현재 기본 클릭 코인량
    public TextMeshProUGUI CurrentAutoTimeTxt; // 현재 자동 클릭 시간

    public Button BuyButton; // 구매 버튼
    public Button ClickUpgradeButton; // Click 업그레이드 버튼
    public Button AutoClickUpgradeButton; // AutoClick 업그레이드 버튼

    public ClickController clickController;

    void Start()
    {
        clickUpgradeItem = new ShopItem()
        {
            ItemName = "Click",
            IsBuy = true,
            UpgradeLevel = 0,
            Price = 30,
            UpgradePrice = 30
        };

        autoClickUpgradeItem = new ShopItem()
        {
            ItemName = "AutoClick",
            IsBuy = false,
            UpgradeLevel = 0,
            Price = 100,
            UpgradePrice = 100
        };

        BuyButton.onClick.AddListener(OnBuyButtonClick);
        ClickUpgradeButton.onClick.AddListener(OnClickUpgradeButton);
        AutoClickUpgradeButton.onClick.AddListener(OnAutoClickUpgradeButtonClick);

        
    }

    void Update()
    {
        UpdateClickPriceText();
        UpdateAutoClickPriceText();
        PrintCurrentClickCoinTxt();
        PrintCurrentAutoTimeTxt();
    }

    void OnBuyButtonClick() // 자동 클릭 구매
    {
        if (!autoClickUpgradeItem.IsBuy && clickController.Coin >= autoClickUpgradeItem.Price)
        {
            clickController.Coin -= autoClickUpgradeItem.Price;
            autoClickUpgradeItem.IsBuy = true;
            clickController.StartAutoClick(); // 자동 클릭 시작

            BuyButton.gameObject.SetActive(false); // 버튼 비활성화
        }
        else
        {
            print("coin 부족");
        }
    }

    void OnClickUpgradeButton() // 클릭 보상 업글
    {
        if (clickUpgradeItem.IsBuy && clickController.Coin >= clickUpgradeItem.UpgradePrice)
        {
            clickController.Coin -= clickUpgradeItem.UpgradePrice;
            clickUpgradeItem.UpgradeLevel++;
            clickController.IncreaseClickReward(1); // 클릭 보상 증가
            clickUpgradeItem.UpgradePrice += clickUpgradeItem.UpgradePrice / 2; // 업그레이드 비용 증가
        }
        else
        {
            print("coin 부족");
        }
    }

    void OnAutoClickUpgradeButtonClick() // 자동 클릭 업글
    {
        if (autoClickUpgradeItem.IsBuy && clickController.Coin >= autoClickUpgradeItem.UpgradePrice)
        {
            clickController.Coin -= autoClickUpgradeItem.UpgradePrice;
            autoClickUpgradeItem.UpgradeLevel++;
            clickController.DecreaseClickReward(0.05f); // 자동 클릭 시간 감소
            autoClickUpgradeItem.UpgradePrice += autoClickUpgradeItem.UpgradePrice / 2; // 업그레이드 비용 증가
        }
        else
        {
            print("coin 부족");
        }
    }

    void UpdateClickPriceText()
    {
        ClickPriceText.text = "비용 : " + clickUpgradeItem.UpgradePrice.ToString();
    }

    void UpdateAutoClickPriceText()
    {
        AutoClickPriceText.text = "비용 : " + autoClickUpgradeItem.UpgradePrice.ToString();
    }

    void PrintCurrentClickCoinTxt()
    {
        CurrentClickCoinTxt.text = $"현재 : {clickController.ClickReward} <color=blue>(+1)</color>";
    }

    void PrintCurrentAutoTimeTxt()
    {
        CurrentAutoTimeTxt.text = $"현재 : {clickController.AutoClickTime} <color=blue>(-0.05)</color>";
    }
}