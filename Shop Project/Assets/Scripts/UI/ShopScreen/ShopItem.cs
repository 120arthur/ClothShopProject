using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShopItem : ItemBase
{
    [SerializeField]
    Button m_BuyButton;
    [SerializeField]
    Button m_SellButton;

    [SerializeField]
    private TMP_Text iconPrice;

    protected override void UpdateInfo(ScriptableItem item)
    {
        base.UpdateInfo(item);
        iconPrice.text = item.ItemPrice.ToString();
    }

    public void Init(ScriptableItem item, Action<ScriptableItem , ShopItem> sell, Action<ScriptableItem, ShopItem> buy)
    {
        UpdateInfo(item);

        m_SellButton.onClick.AddListener(() => { sell?.Invoke(item, this); });
        m_BuyButton.onClick.AddListener(() => { buy?.Invoke(item, this); });
    }

    public void ShowSellButton()
    {
        m_BuyButton.gameObject.SetActive(false);
        m_SellButton.gameObject.SetActive(true);
    }

    public void ShowBuyButton()
    {
        m_BuyButton.gameObject.SetActive(true);
        m_SellButton.gameObject.SetActive(false);
    }

}
