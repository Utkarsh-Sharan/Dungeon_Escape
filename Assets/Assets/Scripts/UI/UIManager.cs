using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    [SerializeField] private Image _selectionImg;

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
}
