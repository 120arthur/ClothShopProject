using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
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
    private SelectSectionButton m_selectButton;
    [SerializeField]
    private TMP_Text m_softCurrency;
    [SerializeField]
    private ShopItem m_shopItem;
    [SerializeField]
    private List<ScriptableItem> m_allScriptableItems;

    private ShopService m_shopService;

    private Dictionary<ItemType, List<ShopItem>> m_instantiatedshopItems = new Dictionary<ItemType, List<ShopItem>>();

    private ItemType m_currentItemsSection;

    [Inject]
    private void Init()
    {
        m_shopService = new ShopService(m_persistence);
        m_softCurrency.text = m_shopService.GetCurrencyAmount().ToString();
        InitSectionButtons();
    }

    protected override void Refresh()
    {
        ChangeSection(ItemType.HAT);
    }

    public void ChangeSection(ItemType itemType)
    {
        SetShopItemstVisibility(m_currentItemsSection, false);

        if (m_instantiatedshopItems.ContainsKey(itemType))
        {
            SetShopItemstVisibility(itemType, true);
        }
        else
        {
            InstantiateNewIShoptems(itemType);
        }
        m_currentItemsSection = itemType;
    }

    private void InstantiateNewIShoptems(ItemType itemType)
    {
        List<ShopItem> instantiatedItems = new List<ShopItem>();
        foreach (ScriptableItem item in m_allScriptableItems)
        {
            if (item.ItemType == itemType)
            {
                ShopItem shopitem = m_instantiator.InstantiatePrefabForComponent<ShopItem>(m_shopItem, m_itemsContainer.transform);
                shopitem.Init(item, SellShopItem, BuyShopIten);
                shopitem.SetEnabled(true);
                CheckIfPlayerHasThisShopItem(shopitem);
                instantiatedItems.Add(shopitem);
            }
        }

        m_instantiatedshopItems.Add(itemType, instantiatedItems);
    }
    private void SetShopItemstVisibility(ItemType itemType, bool IsVisible)

    {
        if (m_instantiatedshopItems.ContainsKey(itemType))
        {
            foreach (ShopItem item in m_instantiatedshopItems[itemType])
            {
                item.SetEnabled(IsVisible);
                CheckIfPlayerHasThisShopItem(item);
            }
        }
    }

    private void CheckIfPlayerHasThisShopItem(ShopItem Shopitem)
    {
        if (m_persistence.HasScriptableItem(Shopitem.m_sctiptableItem))
        {
            Shopitem.ShowSellButton();
        }
        else
        {
            Shopitem.ShowBuyButton();
        }
    }

    private void BuyShopIten(ScriptableItem scriptableItem, ShopItem shopItem)
    {
        m_shopService.BuyScriptableItem(scriptableItem);
        m_softCurrency.text = m_shopService.GetCurrencyAmount().ToString();
        shopItem.ShowSellButton();
    }

    private void SellShopItem (ScriptableItem scriptableItem, ShopItem shopItem)
    {
        m_shopService.SellScriptableItem(scriptableItem);
        m_softCurrency.text = m_shopService.GetCurrencyAmount().ToString();
        m_signalBus.Fire(new OnRemoveClothEvent(scriptableItem));
        shopItem.ShowBuyButton();
    }

    public void InitSectionButtons()
    {
        List<ItemType> itemTypeList = Enum.GetValues(typeof(ItemType)).Cast<ItemType>().ToList();

        for (int i = 0; i < itemTypeList.Count; i++)
        {
            if (itemTypeList[i] != ItemType.BODY)
            {
                SelectSectionButton selectButton = m_instantiator.InstantiatePrefabForComponent<SelectSectionButton>(m_selectButton, m_butonContainer.transform);
                selectButton.Init(itemTypeList[i], ChangeSection);
            }
        }
    }
}
