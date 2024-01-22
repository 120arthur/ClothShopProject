using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemBase : MonoBehaviour
{
    [HideInInspector]
    public ScriptableItem m_sctiptableItem;

    [SerializeField]
    private Image m_icon;
    [SerializeField]
    private TMP_Text iconName;

    protected virtual void UpdateInfo(ScriptableItem item)
    {
        m_sctiptableItem = item;
        m_icon.sprite = item.ItemIcon;  
        iconName.text = item.ItemName;
    }

    public void SetEnabled(bool Active)
    {
        gameObject.SetActive(Active);
    }
}