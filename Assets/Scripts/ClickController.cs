using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    public TextMeshProUGUI countText;
    private int count = 0;
    private int clickReward = 1;

    void Start()
    {
        UpdateCountText();
    }

    void UpdateCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

    public void OnClick()
    {
        count += clickReward;
        UpdateCountText();
    }
}
