using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager is null");
            }

            return _instance;
        }
    }

    [SerializeField] private Text _playerGemCount;
    [SerializeField] private Text _gemCountUI;
    [SerializeField] private Image _selectionImg;
    [SerializeField] private Image[] _healthBars = new Image[4];

    private void Awake()
    {
        _instance = this;
    }

    public void UpdateShopSelection(int yPos)
    {
        _selectionImg.rectTransform.anchoredPosition = new Vector2(_selectionImg.rectTransform.anchoredPosition.x, yPos);
    }

    public void OpenShop(int gemCount)
    {
        _playerGemCount.text = gemCount + "G";
    }

    public void UpdateGemCount(int count)
    {
        _gemCountUI.text = "" + count;
    }

    public void UpdateLives(int livesRemaining)
    {
        for(int i = 0; i <= livesRemaining; i++)
        {
            if(i == livesRemaining)
            {
                _healthBars[i].enabled = false;
            }
        }
    }
}
