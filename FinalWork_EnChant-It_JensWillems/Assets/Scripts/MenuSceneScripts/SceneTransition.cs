using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string enchantItSceneName;
    public static bool skipTeleportEffect; 

    public void UpdateOption(string[] options)
    {
        if (options.Length != 0)
        {
            LoadScene(options);
            return;
        }

        if (options.Length == 0 || options[0] == "Option")
        {
            return;
        }
    }

    public void LoadScene(string[] options)
    {
        string option = options[0];
        if (option.Equals("start", StringComparison.OrdinalIgnoreCase))
        {
            skipTeleportEffect = true;
            Debug.Log("debug: " + skipTeleportEffect);
            SceneManager.LoadScene(enchantItSceneName);
        }
        else if (option.Equals("sandbox", StringComparison.OrdinalIgnoreCase))
        {
            GameStateManager.Instance.DesiredChapter = ChapterController.Chapter.Chapter4;

            skipTeleportEffect = true;
            SceneManager.LoadScene(enchantItSceneName);
        }
        else if (option.Equals("quit", StringComparison.OrdinalIgnoreCase))
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
