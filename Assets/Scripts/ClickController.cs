using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    public TextMeshProUGUI countText;
    private int count = 0;
    private int clickReward = 1;

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

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
        anim.SetTrigger("isClick");
        count += clickReward;
        UpdateCountText();
    }
}
