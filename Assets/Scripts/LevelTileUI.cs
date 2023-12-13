using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelTileUI : MonoBehaviour,IPointerClickHandler
{
    public event Action<LevelTileUI> Clicked; 

    [SerializeField] private TextMeshProUGUI _txt;
    [SerializeField] private GameObject _completeMark;
    [SerializeField] private GameObject _lockMark;
    [SerializeField] private Image _fillImg;

    [SerializeField] private Sprite _completedSprite;
    [SerializeField] private Sprite _notCompletedSprite;

    private ViewModel _mViewModel;


    public ViewModel MViewModel
    {
        get => _mViewModel;
        set
        {
            _txt.text = value.Level.no.ToString();
            _fillImg.sprite = value.Locked ? _notCompletedSprite : _completedSprite;
            _completeMark.SetActive(value.Completed);
            _lockMark.SetActive(value.Locked);
            _mViewModel = value;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked?.Invoke(this);
    }


    public struct ViewModel
    {
        public Level Level { get; set; }
        public bool Locked { get; set; }
        public bool Completed { get; set; }
    }
}