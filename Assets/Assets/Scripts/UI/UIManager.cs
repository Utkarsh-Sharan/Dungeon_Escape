using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    [SerializeField] private GameObject[] _levelCompleteObjs = new GameObject[2];

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

    public void LevelComplete()
    {
        _levelCompleteObjs[0].SetActive(true);
        _levelCompleteObjs[1].SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
