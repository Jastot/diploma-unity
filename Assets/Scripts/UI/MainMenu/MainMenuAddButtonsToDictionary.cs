using System.Collections.Generic;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using UnityEngine.UI;

namespace Diploma.UI
{
    public class MainMenuAddButtonsToDictionary
    {
        private readonly List<Button> _buttons;
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly int[] _usedMenus = new[] {3, 2, 8, 11, 16};

        public MainMenuAddButtonsToDictionary(List<Button> buttons, GameContextWithViews gameContextWithViews)
        {
            _buttons = buttons;
            _gameContextWithViews = gameContextWithViews;
            int i = 0;
            foreach (var button in _buttons)
            {
                _gameContextWithViews.AddMainMenuButtons((LoadingParts)_usedMenus[i],button);
                i++;
            }
        }
    }
}