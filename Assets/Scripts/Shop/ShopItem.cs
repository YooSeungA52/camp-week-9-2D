using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem
{
    public string ItemName; // ������ �̸�
    public bool IsBuy; // ���� ����
    public int UpgradeLevel = 0; // ���׷��̵� ����
    public int Price; // ����
    public int UpgradePrice; // ���׷��̵� ���
    public System.Action UpgradeAction; // ���׷��̵� ����
}
