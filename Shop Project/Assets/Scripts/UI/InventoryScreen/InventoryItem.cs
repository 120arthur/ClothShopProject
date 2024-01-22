using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InventoryItem : UIItemBase
{
    [Inject]
    private SignalBus m_signalBus;
    [SerializeField]
    private Button m_useButton;
    [SerializeField]
    private Button m_removeButton;

    public void Init(ScriptableItem item)
    {
        UpdateInfo(item);

        m_useButton.onClick.AddListener(OnUseButton);
        m_removeButton.onClick.AddListener(OnRemoveButton);
    }

    public void OnUseButton()
    {
        m_signalBus.Fire(new OnUseClothEvent(m_sctiptableItem));
    }
    
    public void OnRemoveButton()
    {
        m_signalBus.Fire(new OnRemoveClothEvent(m_sctiptableItem));
    }
}