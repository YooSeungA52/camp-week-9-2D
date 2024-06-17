using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem
{
    public string ItemName; // 아이템 이름
    public bool IsBuy; // 구매 여부
    public int UpgradeLevel = 0; // 업그레이드 레벨
    public int Price; // 가격
    public int UpgradePrice; // 업그레이드 비용
    public System.Action UpgradeAction; // 업그레이드 동작
}
