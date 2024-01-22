using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField]
    ShopScreen m_shopScreen;
    [SerializeField]
    InventoryScreen m_inventoryScreen;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<OnUseClothEvent>();
        Container.DeclareSignal<OnRemoveClothEvent>();

        Container.Bind(typeof(IPersistence)).To(typeof(InventoryPersistence)).AsSingle();

        Container.BindInstance(m_shopScreen);
        Container.BindInstance(m_inventoryScreen);
    }
}