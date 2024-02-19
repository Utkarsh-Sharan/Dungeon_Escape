using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject _shopUI;

    private int _selectedItem;
    private int _selectedItemCost;

    private Player _player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.GetComponent<Player>();
            if(_player != null)
            {
                UIManager.Instance.OpenShop(_player.diamonds);
            }

            _shopUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _shopUI.SetActive(false);
        }
    }

    public void SelectItem(int item)
    {
        //Debug.Log("SelectHit");
        switch(item)
        {
            case 0:
                _selectedItem = 0;
                _selectedItemCost = 200;
                UIManager.Instance.UpdateShopSelection(84);
                break;
            case 1:
                _selectedItem = 1;
                _selectedItemCost = 400;
                UIManager.Instance.UpdateShopSelection(-21);
                break;
            case 2:
                _selectedItem = 2;
                _selectedItemCost = 100;
                UIManager.Instance.UpdateShopSelection(-131);
                break;
        }
    }

    public void BuyItem()
    {
        if(_player.diamonds >= _selectedItemCost)
        {
            if(_selectedItem == 2)
            {
                GameManager.Instance.HasKeyToCastle = true;
            }
            _player.diamonds -= _selectedItemCost;
        }
        else
        {
            _shopUI.SetActive(false);
        }
    }
}
