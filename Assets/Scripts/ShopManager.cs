﻿using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class ShopManager : MonoBehaviour
{
    [Header("ClickerBtn")]
    public ClickController clickController;

    [Header("ItemData")]
    public ShopItem ClickItem; // 기본 클릭 아이템
    public ShopItem AutoClickItem; // 자동 클릭 아이템
    public ShopItem BearJellyItem; // BearJelly 아이템
    public ShopItem CatJellyItem; // BearJelly 아이템
    public ShopItem[] shopItems;

    [Header("BotObject")]
    public GameObject BotBearJelly;
    public GameObject BotCatJelly;

    [Header("PriceText")]
    public TextMeshProUGUI ClickPriceText;  // 기본 클릭 업그레이드 비용
    public TextMeshProUGUI AutoClickPriceText; // 자동 클릭 업그레이드 비용
    public TextMeshProUGUI BearJellyPriceText; // BearJelly 업그레이드 비용
    public TextMeshProUGUI CatJellyPriceText; // CatJelly 업그레이드 비용

    [Header("CurrentText")]
    public TextMeshProUGUI CurrentClickCoinTxt;  // 현재 기본 클릭 코인량
    public TextMeshProUGUI CurrentAutoTimeTxt; // 현재 자동 클릭 시간
    public TextMeshProUGUI CurrentBearJellyAutoTimeTxt; // BearJelly 자동 클릭 시간
    public TextMeshProUGUI CurrentCatJellyAutoTimeTxt; // CatJelly 자동 클릭 시간

    [Header("BuyButton")]
    public Button BuyAutoClickButton; // AutoClick 구매 버튼
    public Button BuyBearJellyButton; // BearJelly 구매 버튼
    public Button BuyCatJellyButton; // CatJelly 구매 버튼

    [Header("UpgradeButton")]
    public Button ClickUpgradeButton; // Click 업그레이드 버튼
    public Button AutoClickUpgradeButton; // AutoClick 업그레이드 버튼
    public Button BearJellyUpgradeButton; // BearJelly 업그레이드 버튼
    public Button CatJellyUpgradeButton; // CatJelly 업그레이드 버튼

    [Header("UI")]
    public GameObject GiveMeMoreCoinsUI; // 코인 부족 UI

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        // 게임 시작 시 모든 아이템의 초기값 저장
        foreach (var item in shopItems)
        {
            item.SaveInitialValues();
        }

        ClickItem.UpgradeAction = () =>
        {
            clickController.IncreaseClickReward(1); // 클릭 보상 증가
        };

        AutoClickItem.UpgradeAction = () =>
        {
            clickController.DecreaseClickReward(0.05f); // 자동 클릭 시간 감소
        };

        BearJellyItem.UpgradeAction = () =>
        {
            BearJellyItem.AutoClickTime -= 0.05f; // BearJelly 자동 클릭 시간 감소
        };

        CatJellyItem.UpgradeAction = () =>
        {
            CatJellyItem.AutoClickTime -= 0.05f; // CatJelly 자동 클릭 시간 감소
        };

        BuyAutoClickButton.onClick.AddListener(OnBuyAutoClickButton);
        BuyBearJellyButton.onClick.AddListener(OnBuyBearJellyButton);
        BuyCatJellyButton.onClick.AddListener(OnBuyCatJellyButton);

        ClickUpgradeButton.onClick.AddListener(() => OnUpgradeButton(ClickItem));
        AutoClickUpgradeButton.onClick.AddListener(() => OnUpgradeButton(AutoClickItem));
        BearJellyUpgradeButton.onClick.AddListener(() => OnUpgradeButton(BearJellyItem));
        CatJellyUpgradeButton.onClick.AddListener(() => OnUpgradeButton(CatJellyItem));

        GiveMeMoreCoinsUI.SetActive(false);
    }

    void Update()
    {
        UpdatePriceText(ClickPriceText, ClickItem);
        UpdatePriceText(AutoClickPriceText, AutoClickItem);

        if (BearJellyUpgradeButton.gameObject.activeSelf)
        {
            UpdatePriceText(BearJellyPriceText, BearJellyItem);
        }
        else
        {
            BearJellyPriceText.text = "비용 : " + BearJellyItem.Price.ToString();
        }

        if (CatJellyUpgradeButton.gameObject.activeSelf)
        {
            UpdatePriceText(CatJellyPriceText, CatJellyItem);
        }
        else
        {
            CatJellyPriceText.text = "비용 : " + CatJellyItem.Price.ToString();
        }

        PrintCurrentClickCoinTxt();

        if (!BuyAutoClickButton.gameObject.activeSelf)
        {
            PrintCurrentAutoTimeTxt();
        }
        else
        {
            CurrentAutoTimeTxt.text = "";
        }

        if (!BuyBearJellyButton.gameObject.activeSelf)
        {
            PrintCurrentBearJellyAutoTimeTxt();
        }
        else
        {
            CurrentBearJellyAutoTimeTxt.text = "";
        }

        if (!BuyCatJellyButton.gameObject.activeSelf)
        {
            PrintCurrentCatJellyAutoTimeTxt();
        }
        else
        {
            CurrentCatJellyAutoTimeTxt.text = "";
        }

        UpdateCoinText();
    }

    void OnBuyAutoClickButton() // 자동 클릭 구매
    {
        if (!AutoClickItem.IsBuy && clickController.Coin >= AutoClickItem.Price)
        {
            AudioManager.Instance.PlayBuySound();
            clickController.Coin -= AutoClickItem.Price;
            AutoClickItem.IsBuy = true;
            clickController.StartAutoClick(); // 자동 클릭 시작

            BuyAutoClickButton.gameObject.SetActive(false); // 구매 버튼 비활성화
            AutoClickUpgradeButton.gameObject.SetActive(true); // 업글 버튼 활성화
        }
        else
        {
            PrintGiveMeMoreCoinsUI();
        }
    }

    void OnBuyBearJellyButton() // BearJelly 구매
    {
        if (!BearJellyItem.IsBuy && clickController.Coin >= BearJellyItem.Price)
        {
            AudioManager.Instance.PlayBuySound();
            BotBearJelly.SetActive(true);
            clickController.Coin -= BearJellyItem.Price;
            BearJellyItem.IsBuy = true;
            StartAutoClick(BearJellyItem.AutoClickTime,BearJellyItem.ClickReward); // 자동 클릭 시작

            BuyBearJellyButton.gameObject.SetActive(false); // 구매 버튼 비활성화
            BearJellyUpgradeButton.gameObject.SetActive(true); // 업글 버튼 활성화
        }
        else
        {
            PrintGiveMeMoreCoinsUI();
        }
    }

    void OnBuyCatJellyButton() // CatJelly 구매
    {
        if (!CatJellyItem.IsBuy && clickController.Coin >= CatJellyItem.Price)
        {
            AudioManager.Instance.PlayBuySound();
            BotCatJelly.SetActive(true);
            clickController.Coin -= CatJellyItem.Price;
            CatJellyItem.IsBuy = true;
            StartAutoClick(CatJellyItem.AutoClickTime, CatJellyItem.ClickReward); // 자동 클릭 시작

            BuyCatJellyButton.gameObject.SetActive(false); // 구매 버튼 비활성화
            CatJellyUpgradeButton.gameObject.SetActive(true); // 업글 버튼 활성화
        }
        else
        {
            PrintGiveMeMoreCoinsUI();
        }
    }


    void OnUpgradeButton(ShopItem item) // 아이템 업그레이드
    {
        if (item.IsBuy && clickController.Coin >= item.UpgradePrice)
        {
            AudioManager.Instance.PlayUpgradeSound();
            clickController.Coin -= item.UpgradePrice;
            item.UpgradeLevel++;
            item.UpgradeAction(); // 업그레이드 동작 실행
            item.UpgradePrice += item.UpgradePrice / 2; // 업그레이드 비용 증가
        }
        else
        {
            PrintGiveMeMoreCoinsUI();
        }
    }

    void PrintGiveMeMoreCoinsUI()
    {
        AudioManager.Instance.PlayFailSound();
        GiveMeMoreCoinsUI.SetActive(true);
        Invoke("HideGiveMeMoreCoinsUI", 1f); // 1초 뒤 비활성화
    }

    void HideGiveMeMoreCoinsUI()
    {
        GiveMeMoreCoinsUI.SetActive(false);
    }

    void UpdatePriceText(TextMeshProUGUI priceText, ShopItem item)
    {
        priceText.text = "비용 : " + item.UpgradePrice.ToString();
    }

    void PrintCurrentClickCoinTxt()
    {
        CurrentClickCoinTxt.text = $"현재 : {clickController.ClickReward} <color=blue>(+1)</color>";
    }

    void PrintCurrentAutoTimeTxt()
    {
        CurrentAutoTimeTxt.text = $"현재 : {clickController.AutoClickTime} <color=blue>(-0.05)</color>";
    }

    void PrintCurrentBearJellyAutoTimeTxt()
    {
        CurrentBearJellyAutoTimeTxt.text = $"현재 : {BearJellyItem.AutoClickTime} <color=blue>(-0.05)</color>";
    }

    void PrintCurrentCatJellyAutoTimeTxt()
    {
        CurrentCatJellyAutoTimeTxt.text = $"현재 : {CatJellyItem.AutoClickTime} <color=blue>(-0.05)</color>";
    }

    public void StartAutoClick(float autoClickTime, int clickReward) // 봇 자동 클릭
    {
        StartCoroutine(BotClickCoroutine(autoClickTime, clickReward));
    }

    IEnumerator BotClickCoroutine(float autoClickTime, int clickReward)
    {
        while (true)
        {
            yield return new WaitForSeconds(autoClickTime);
            anim.SetTrigger("isClick");
            AudioManager.Instance.PlayClickSound();
            clickController.count++;
            clickController.Coin += clickReward;
            UpdateCoinText();
        }
    }

    void UpdateCoinText()
    {
        clickController.CoinText.text = clickController.Coin.ToString();
    }
}