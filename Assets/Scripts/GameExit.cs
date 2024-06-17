using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExitBtn : MonoBehaviour
{
    public GameObject ExitWindow;

    public void GameExit() // ���� ����
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void CancelExitGame() // ���
    {
        ExitWindow.SetActive(false);
    }
}
