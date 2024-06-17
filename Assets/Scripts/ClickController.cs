using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    public TextMeshProUGUI countText;

    [SerializeField] private int count = 0;
    [SerializeField] private int coin = 0;
    [SerializeField] private int clickReward = 1; // Ŭ�� �� ����
    [SerializeField] private float AutoClickTime = 3.0f; // �ڵ� Ŭ�� �ֱ�

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

    public void StartAutoClick() // �ڵ� Ŭ��
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
