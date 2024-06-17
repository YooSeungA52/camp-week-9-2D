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

        
    }

    void Update()
    {
        UpdateClickPriceText();
        UpdateAutoClickPriceText();
        PrintCurrentClickCoinTxt();
        PrintCurrentAutoTimeTxt();
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
            print("coin ����");
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
            print("coin ����");
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
            print("coin ����");
        }
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