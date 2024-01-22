using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShopScreen : UI
{
    [Inject]
    private IPersistence m_persistence;
    [Inject]
    IInstantiator m_instantiator;
    [Inject]
    private SignalBus m_signalBus;

    [SerializeField]
    private GameObject m_itemsContainer;
    [SerializeField]
    private GameObject m_butonContainer;
    [SerializeField]
    private SelectButton m_selectButton;
    [SerializeField]
    private TMP_Text m_softCurrency;
    [SerializeField]
    private ShopItem m_shopItem;
    [SerializeField]
    private List<ScriptableItem> m_allScriptableItems;

    private ShopService m_shopService;

    private Dictionary<ItemType, List<ShopItem>> m_shopItems = new Dictionary<ItemType, List<ShopItem>>();

    private ItemType m_currentSection = ItemType.SHIRT;

    [Inject]
    private void Init()
    {
        m_shopService = new ShopService(m_persistence);
        m_softCurrency.text = m_shopService.GetCurrencyAmount().ToString();
        InitButtons();
    }

    protected override void Refresh()
    {
        ChangeSection(ItemType.HAT);
    }

    public void ChangeSection(ItemType itemType)
    {
        if (itemType == m_currentSection)
        {
            return;
        }

        if (m_shopItems.ContainsKey(itemType))
        {

            SetVisibilityOfItems(itemType, true);
        }
        else
        {
            InstantiateNewItens(itemType);
        }

        SetVisibilityOfItems(m_currentSection, false);

        m_currentSection = itemType;
    }

    private void InstantiateNewItens(ItemType itemType)
    {
        List<ShopItem> instantiatedItems = new List<ShopItem>();
        foreach (ScriptableItem item in m_allScriptableItems)
        {
            if (item.ItemType == itemType)
            {
                ShopItem shopitem = m_instantiator.InstantiatePrefabForComponent<ShopItem>(m_shopItem, m_itemsContainer.transform);
                shopitem.Init(item, SellIten, BuyIten);
                shopitem.SetEnabled(true);
                shopitem.ShowBuyButton();
                instantiatedItems.Add(shopitem);
            }
        }

        m_shopItems.Add(itemType, instantiatedItems);
    }

    private void SetVisibilityOfItems(ItemType itemType, bool IsVisible)
    {
        if (m_shopItems.ContainsKey(itemType))
        {
            foreach (ShopItem item in m_shopItems[itemType])
            {
                item.SetEnabled(IsVisible);
                if (m_persistence.HasIten(item.m_sctiptableItem))
                {
                    item.ShowSellButton();
                }
                else
                {
                    item.ShowBuyButton();
                }
            }
        }
    }

    private void BuyIten(ScriptableItem item, ShopItem shopItem)
    {
        m_shopService.BuyIten(item);
        m_softCurrency.text = m_shopService.GetCurrencyAmount().ToString();
        shopItem.ShowSellButton();
    }

    private void SellIten(ScriptableItem item, ShopItem shopItem)
    {
        m_shopService.SellIten(item);
        m_softCurrency.text = m_shopService.GetCurrencyAmount().ToString();
        m_signalBus.Fire(new OnRemoveClothEvent(item));
        shopItem.ShowBuyButton();
    }

    public void InitButtons()
    {
        List<ItemType> itemTypeList = Enum.GetValues(typeof(ItemType)).Cast<ItemType>().ToList();

        for (int i = 0; i < itemTypeList.Count; i++)
        {
            SelectButton selectButton = m_instantiator.InstantiatePrefabForComponent<SelectButton>(m_selectButton, m_butonContainer.transform);
            selectButton.Init(itemTypeList[i], ChangeSection);
        }
    }
}
