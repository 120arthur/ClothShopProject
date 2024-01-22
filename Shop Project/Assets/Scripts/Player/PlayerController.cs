using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 100f)]
    private float m_moveSpeed = 5f;
    [SerializeField]
    private Rigidbody2D m_rb;

    [SerializeField]
    private List<AnimationHandler> m_CharacterAnimatedParts;

    [Inject]
    private SignalBus m_SignalBus;
    [Inject]
    private InventoryScreen m_inventoryScreen;

    public Vector2 m_direction;

    private void Start()
    {
        m_SignalBus.Subscribe<OnUseClothEvent>(UseCloth);
        m_SignalBus.Subscribe<OnRemoveClothEvent>(RemoveCloth);
    }

    public void OnDestroy()
    {
        m_SignalBus.Unsubscribe<OnUseClothEvent>(UseCloth);
        m_SignalBus.Unsubscribe<OnRemoveClothEvent>(RemoveCloth);
    }

    private void Update()
    {
        MovePlayer();
        SeAnimation();
    }

    private void MovePlayer()
    {
        m_rb.velocity = m_direction * m_moveSpeed;
    }

    public void SetDirection(InputAction.CallbackContext value)
    {
        m_direction = value.ReadValue<Vector2>();
    }

    private void SeAnimation()
    {
        if (m_direction.x == 0 && m_direction.y == 0)
        {
            TriggerAnimators("Idle");
        }
        else if (m_direction.y > 0 && m_direction.x == 0)
        {
            SpriteRendererFlipX(false);
            TriggerAnimators("MoveUp");
        }
        else if (m_direction.y < 0 && m_direction.x == 0)
        {
            SpriteRendererFlipX(false);
            TriggerAnimators("MoveDown");
        }
        else if (m_direction.x > 0)
        {
            SpriteRendererFlipX(false);
            TriggerAnimators("Move");
        }
        else if (m_direction.x < 0)
        {
            SpriteRendererFlipX(true);
            TriggerAnimators("Move");
        }
    }

    public void OpenInventory(InputAction.CallbackContext value)
    {
        m_inventoryScreen.OpenScreen();
    }

    private void UseCloth(OnUseClothEvent args)
    {
        foreach (AnimationHandler animation in m_CharacterAnimatedParts)
        {
            animation.ChangeAnimator(args.Item.ItemControllerAnimator, args.Item.ItemType);
        }
    }

    private void RemoveCloth(OnRemoveClothEvent args)
    {
        foreach (AnimationHandler animation in m_CharacterAnimatedParts)
        {
            animation.RemoveAnimator(args.Item.ItemType);
        }
    }

    private void TriggerAnimators(string triggerName)
    {
        foreach (AnimationHandler animation in m_CharacterAnimatedParts)
        {
            animation.SetAnimationTrigger(triggerName);
        }
    }

    private void SpriteRendererFlipX(bool flipX)
    {
        foreach (AnimationHandler animation in m_CharacterAnimatedParts)
        {
            animation.FlipSprite(flipX);
        }
    }
}
