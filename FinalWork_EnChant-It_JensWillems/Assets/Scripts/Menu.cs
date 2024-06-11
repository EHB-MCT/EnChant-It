using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject Spellbook;
    public WorldSpaceCanvasController WorldSpaceCanvasController;
    public event Action OnMenuOpenedFirstTime;

    private bool _isMenuOpenedForFirstTime = false;

    private void Start()
    {
        Spellbook.SetActive(false);
    }
    public void UpdateMenu(string[] menuCommands)
    {
        if (menuCommands.Length != 0)
        {
            MenuCommands(menuCommands);
            return;
        }

        if (menuCommands.Length == 0 || menuCommands[0] == "spell")
        {
            return;
        } 
    }

    public void MenuCommands(string[] menuCommands)
    {
        if (menuCommands.Length > 0)
        {
            string menuCommandName = menuCommands[0];

            if (menuCommandName.Equals("book", StringComparison.OrdinalIgnoreCase))
            {
                Spellbook.SetActive(true);
                WorldSpaceCanvasController.DistanceFromTarget = 0.4f;
                WorldSpaceCanvasController.MoveWithCamera = false;

                if (_isMenuOpenedForFirstTime)
                {
                    OnMenuOpenedFirstTime?.Invoke();
                }
            } 
            if (menuCommandName.Equals("close", StringComparison.OrdinalIgnoreCase) )
            {
                Spellbook.SetActive(false);
                WorldSpaceCanvasController.MoveWithCamera = true;
            }
            else if (menuCommandName.Equals("Quit", StringComparison.OrdinalIgnoreCase) && Spellbook == true)
            {
               Application.Quit();

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }


        }
    }
    public void EnableFirstTimeMenuOpening()
    {
        _isMenuOpenedForFirstTime = true;
    }
}
