using UnityEngine;

public class GameExitBtn : MonoBehaviour
{
    public GameObject ExitWindow;
    public ShopItem[] shopItems;

    public void GameExit() // 게임 종료
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void CancelExitGame() // 취소
    {
        ExitWindow.SetActive(false);
    }

    public void OnResetButtonClicked()
    {
        // 모든 아이템의 데이터 초기화
        foreach (var item in shopItems)
        {
            item.ResetToInitialValues();
        }
    }
}
