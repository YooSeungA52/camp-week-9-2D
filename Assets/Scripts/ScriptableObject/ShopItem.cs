using UnityEngine;

public enum ItemType
{
    Click, // Click 관련 아이템
    Bot // AutoClick 해주는 봇 아이템
}

[CreateAssetMenu(fileName = "ShopItem", menuName = "ShopItem")]
public class ShopItem : ScriptableObject
{
    [Header("Info")]
    public string ItemName; // 아이템 이름
    public bool IsBuy; // 구매 여부
    public int UpgradeLevel; // 업그레이드 레벨
    public int Price; // 가격
    public int UpgradePrice; // 업그레이드 비용
    public System.Action UpgradeAction; // 업그레이드 동작

    [Header("ItemType")]
    public ItemType type;
    public int ClickReward; // 봇 클릭 당 보상
    public float AutoClickTime; // 봇 자동 클릭 주기

    // 초기값을 저장할 변수들
    [HideInInspector] public bool initialIsBuy;
    [HideInInspector] public int initialUpgradeLevel;
    [HideInInspector] public int initialPrice;
    [HideInInspector] public int initialUpgradePrice;
    [HideInInspector] public int initialClickReward;
    [HideInInspector] public float initialAutoClickTime;
    
    public void SaveInitialValues() // 초기값을 저장하는 함수
    {
        initialIsBuy = IsBuy;
        initialUpgradeLevel = UpgradeLevel;
        initialPrice = Price;
        initialUpgradePrice = UpgradePrice;
        initialClickReward = ClickReward;
        initialAutoClickTime = AutoClickTime;
    }

    public void ResetToInitialValues() // 초기값으로 리셋하는 함수
    {
        IsBuy = initialIsBuy;
        UpgradeLevel = initialUpgradeLevel;
        Price = initialPrice;
        UpgradePrice = initialUpgradePrice;
        ClickReward = initialClickReward;
        AutoClickTime = initialAutoClickTime;
    }
}
