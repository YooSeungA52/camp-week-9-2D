using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    public TextMeshProUGUI countText;

    [SerializeField] private int count = 0;
    [SerializeField] private int coin = 0;
    [SerializeField] private int clickReward = 1; // 클릭 당 보상
    [SerializeField] private float AutoClickTime = 3.0f; // 자동 클릭 주기

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
        countText.text = "coin: " + coin.ToString();
    }

    public void OnClick()
    {
        anim.SetTrigger("isClick");
        count++;
        coin += clickReward;
        UpdateCountText();
    }

    public void StartAutoClick() // 자동 클릭
    {
        StartCoroutine(AutoClickCoroutine());
    }

    IEnumerator AutoClickCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(AutoClickTime);
            count++;
            coin += clickReward;
            UpdateCountText();
        }
    }
}
