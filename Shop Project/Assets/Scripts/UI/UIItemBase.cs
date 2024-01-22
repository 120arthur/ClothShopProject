using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemBase : MonoBehaviour
{
    [HideInInspector]
    public ScriptableItem m_sctiptableItem;

    [SerializeField]
    private Image m_icon;
    [SerializeField]
    private TMP_Text m_iconName;

    protected virtual void UpdateInfo(ScriptableItem item)
    {
        m_sctiptableItem = item;
        m_icon.sprite = item.ItemIcon;  
        m_iconName.text = item.ItemName;
    }

    public void SetEnabled(bool Active)
    {
        gameObject.SetActive(Active);
    }
}