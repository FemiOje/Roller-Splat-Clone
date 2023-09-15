using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
public class UIHandler : MonoBehaviour
{
    public void GoToLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void ExitGame()   //confirm that thhis works in a built app
    {
#if UNITY_EDITOR
        // In the Unity Editor, stop play mode
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // In a built application, quit the application
            Application.Quit();
#endif
    }
}