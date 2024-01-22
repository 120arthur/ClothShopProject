using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectSectionButton : MonoBehaviour
{
    [SerializeField]
    private Button m_button;
    [SerializeField]
    private TMP_Text m_buttonName;

    private ItemType m_itemType;
  
    private Action<ItemType> m_action;

    public void Init(ItemType itemType, Action<ItemType> action)
    {
        m_action = action;
        m_itemType = itemType;
        m_button.onClick.AddListener(OnButtonPressed);
        m_buttonName.text = itemType.ToString();
    }

    private void OnButtonPressed()
    {
        Debug.Log("Button pressed: " + m_itemType);
        m_action?.Invoke(m_itemType);
    }
}
