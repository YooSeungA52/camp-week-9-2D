using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
            UpgradePrice = 30,
            UpgradeAction = () =>
            {
                clickController.IncreaseClickReward(1); // Ŭ�� ���� ����
            }
        };

        autoClickUpgradeItem = new ShopItem()
        {
            ItemName = "AutoClick",
            IsBuy = false,
            UpgradeLevel = 0,
            Price = 100,
            UpgradePrice = 100,
            UpgradeAction = () =>
            {
                clickController.DecreaseClickReward(0.05f); // �ڵ� Ŭ�� �ð� ����
            }
        };

        BuyButton.onClick.AddListener(OnBuyAutoClickButton);
        //ClickUpgradeButton.onClick.AddListener(OnClickUpgradeButton);
        //AutoClickUpgradeButton.onClick.AddListener(OnAutoClickUpgradeButton);
        ClickUpgradeButton.onClick.AddListener(() => OnUpgradeButton(clickUpgradeItem));
        AutoClickUpgradeButton.onClick.AddListener(() => OnUpgradeButton(autoClickUpgradeItem));

        GiveMeMoreCoinsUI.SetActive(false);
    }

    void Update()
    {
        /*
        UpdateClickPriceText();
        UpdateAutoClickPriceText();
        */
        UpdatePriceText(ClickPriceText, clickUpgradeItem);
        UpdatePriceText(AutoClickPriceText, autoClickUpgradeItem);

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

    void OnBuyAutoClickButton() // �ڵ� Ŭ�� ����
    {
        if (!autoClickUpgradeItem.IsBuy && clickController.Coin >= autoClickUpgradeItem.Price)
        {
            AudioManager.Instance.PlayBuySound();
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

    void OnUpgradeButton(ShopItem item) // ������ ���׷��̵�
    {
        if (item.IsBuy && clickController.Coin >= item.UpgradePrice)
        {
            AudioManager.Instance.PlayUpgradeSound();
            clickController.Coin -= item.UpgradePrice;
            item.UpgradeLevel++;
            item.UpgradeAction(); // ���׷��̵� ���� ����
            item.UpgradePrice += item.UpgradePrice / 2; // ���׷��̵� ��� ����
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
        Invoke("HideGiveMeMoreCoinsUI", 1f); // 1�� �� ��Ȱ��ȭ
    }

    void HideGiveMeMoreCoinsUI()
    {
        GiveMeMoreCoinsUI.SetActive(false);
    }

    void UpdatePriceText(TextMeshProUGUI priceText, ShopItem item)
    {
        priceText.text = "��� : " + item.UpgradePrice.ToString();
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