using UnityEngine;

public class GameExitBtn : MonoBehaviour
{
    public GameObject ExitWindow;

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
}
