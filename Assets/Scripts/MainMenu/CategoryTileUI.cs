// /*
// Created by Darsan
// */

using System;
using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MainMenu
{
    public class CategoryTileUI : MonoBehaviour, IPointerClickHandler
    {
        public event Action<CategoryTileUI> Clicked;

        [SerializeField] private TextMeshProUGUI _nameTxt, _progressTxt;
        [SerializeField] private GameObject _lockIcon;
        private LevelGroup _levelGroup;

        public bool IsLocked { get; private set; } = false;

        public LevelGroup LevelGroup
        {
            get => _levelGroup;
            set
            {
                _levelGroup = value;
                var levelSetNumber = value.name.Substring(value.name.Length - 1);
                LocalizedString locString = "Group";
                _nameTxt.text = locString +" "+levelSetNumber;
                _progressTxt.text = $"{ResourceManager.GetCompletedLevel(value.id)}/{value.levels.Count}";
            }
        }

        public bool IsCanActivateNextLevelGroup()
           =>ResourceManager.GetCompletedLevel(LevelGroup.id) == LevelGroup.levels.Count;

        public void InitializeAsLocked()
        {
            IsLocked = true;
            _lockIcon.SetActive(true);
        }
        

        public void OnPointerClick(PointerEventData eventData)
        {
            if(IsLocked)
            {
                return;
            }
            Clicked?.Invoke(this);
        }
    }
}