using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : UIItemBase
{
    [SerializeField]
   private Button m_buyButton;
    [SerializeField]
   private Button m_sellButton;

    [SerializeField]
    private TMP_Text m_iconItemPrice;

    protected override void UpdateInfo(ScriptableItem scriptableItem)
    {
        base.UpdateInfo(scriptableItem);
        m_iconItemPrice.text = scriptableItem.ItemPrice.ToString();
    }

    public void Init(ScriptableItem item, Action<ScriptableItem , ShopItem> sell, Action<ScriptableItem, ShopItem> buy)
    {
        UpdateInfo(item);

        m_sellButton.onClick.AddListener(() => { sell?.Invoke(item, this); });
        m_buyButton.onClick.AddListener(() => { buy?.Invoke(item, this); });
    }

    public void ShowSellButton()
    {
        m_buyButton.gameObject.SetActive(false);
        m_sellButton.gameObject.SetActive(true);
    }

    public void ShowBuyButton()
    {
        m_buyButton.gameObject.SetActive(true);
        m_sellButton.gameObject.SetActive(false);
    }

}
