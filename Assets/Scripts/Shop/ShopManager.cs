using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class ShopManager : MonoBehaviour
{
    public ShopItem clickUpgradeItem; // �⺻ Ŭ�� ������
    public ShopItem autoClickUpgradeItem; // �ڵ� Ŭ�� ������

    public TextMeshProUGUI ClickPriceText;  // �⺻ Ŭ�� ���׷��̵� ���
    public TextMeshProUGUI AutoClickPriceText; // �ڵ� Ŭ�� ���׷��̵� ���
    public TextMeshProUGUI CurrentClickCoinTxt;  // ���� �⺻ Ŭ�� ���η�
    public TextMeshProUGUI CurrentAutoTimeTxt; // ���� �ڵ� Ŭ�� �ð�
    public GameObject GiveMeMoreCoinsUI; // ���� ���� UI

    public Button BuyButton; // ���� ��ư
    public Button ClickUpgradeButton; // Click ���׷��̵� ��ư
    public Button AutoClickUpgradeButton; // AutoClick ���׷��̵� ��ư

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

        GiveMeMoreCoinsUI.SetActive(false);
    }

    void Update()
    {
        UpdateClickPriceText();
        UpdateAutoClickPriceText();
        PrintCurrentClickCoinTxt();

        if (!BuyButton.gameObject.activeSelf)
        {
            PrintCurrentAutoTimeTxt();
        }
        else
        {
            CurrentAutoTimeTxt.text = "";
        }
    }

    void OnBuyButtonClick() // �ڵ� Ŭ�� ����
    {
        if (!autoClickUpgradeItem.IsBuy && clickController.Coin >= autoClickUpgradeItem.Price)
        {
            clickController.Coin -= autoClickUpgradeItem.Price;
            autoClickUpgradeItem.IsBuy = true;
            clickController.StartAutoClick(); // �ڵ� Ŭ�� ����

            BuyButton.gameObject.SetActive(false); // ��ư ��Ȱ��ȭ
        }
        else
        {
            PrintGiveMeMoreCoinsUI();
        }
    }

    void OnClickUpgradeButton() // Ŭ�� ���� ����
    {
        if (clickUpgradeItem.IsBuy && clickController.Coin >= clickUpgradeItem.UpgradePrice)
        {
            clickController.Coin -= clickUpgradeItem.UpgradePrice;
            clickUpgradeItem.UpgradeLevel++;
            clickController.IncreaseClickReward(1); // Ŭ�� ���� ����
            clickUpgradeItem.UpgradePrice += clickUpgradeItem.UpgradePrice / 2; // ���׷��̵� ��� ����
        }
        else
        {
            PrintGiveMeMoreCoinsUI();
        }
    }

    void OnAutoClickUpgradeButtonClick() // �ڵ� Ŭ�� ����
    {
        if (autoClickUpgradeItem.IsBuy && clickController.Coin >= autoClickUpgradeItem.UpgradePrice)
        {
            clickController.Coin -= autoClickUpgradeItem.UpgradePrice;
            autoClickUpgradeItem.UpgradeLevel++;
            clickController.DecreaseClickReward(0.05f); // �ڵ� Ŭ�� �ð� ����
            autoClickUpgradeItem.UpgradePrice += autoClickUpgradeItem.UpgradePrice / 2; // ���׷��̵� ��� ����
        }
        else
        {
            PrintGiveMeMoreCoinsUI();
        }
    }

    void PrintGiveMeMoreCoinsUI()
    {
        GiveMeMoreCoinsUI.SetActive(true);
        Invoke("HideGiveMeMoreCoinsUI", 1f); // 1�� �� ��Ȱ��ȭ
    }

    void HideGiveMeMoreCoinsUI()
    {
        GiveMeMoreCoinsUI.SetActive(false);
    }

    void UpdateClickPriceText()
    {
        ClickPriceText.text = "��� : " + clickUpgradeItem.UpgradePrice.ToString();
    }

    void UpdateAutoClickPriceText()
    {
        AutoClickPriceText.text = "��� : " + autoClickUpgradeItem.UpgradePrice.ToString();
    }

    void PrintCurrentClickCoinTxt()
    {
        CurrentClickCoinTxt.text = $"���� : {clickController.ClickReward} <color=blue>(+1)</color>";
    }

    void PrintCurrentAutoTimeTxt()
    {
        CurrentAutoTimeTxt.text = $"���� : {clickController.AutoClickTime} <color=blue>(-0.05)</color>";
    }
}