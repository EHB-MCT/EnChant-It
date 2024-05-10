using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
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
                Debug.Log("opening book");

            }
        }
    }
}
