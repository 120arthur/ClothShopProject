using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private const string MOVE_TRIGGER_NAME = "Move";
    private const string MOVE_UP_TRIGGER_NAME = "MoveUp";
    private const string MOVE_DOWN_TRIGGER_NAME = "MoveDown";
    private const string IDLE_TRIGGER_NAME = "Idle";

    [Inject]
    private SignalBus m_signalBus;
    [Inject]
    private InventoryScreen m_inventoryScreen;

    [SerializeField]
    [Range(1f, 100f)]
    private float m_moveSpeed = 5f;
    [SerializeField]
    private Rigidbody2D m_rb;

    [SerializeField]
    private List<CharacterAnimationController> m_characterAnimatedParts;

    private Vector2 m_direction;

    private void Start()
    {
        m_signalBus.Subscribe<OnUseClothEvent>(UseCloth);
        m_signalBus.Subscribe<OnRemoveClothEvent>(RemoveCloth);
    }

    public void OnDestroy()
    {
        m_signalBus.Unsubscribe<OnUseClothEvent>(UseCloth);
        m_signalBus.Unsubscribe<OnRemoveClothEvent>(RemoveCloth);
    }

    private void Update()
    {
        MovePlayer();
        SetAnimation();
    }

    private void MovePlayer()
    {
        m_rb.velocity = m_direction * m_moveSpeed;
    }

    public void SetDirection(InputAction.CallbackContext value)
    {
        m_direction = value.ReadValue<Vector2>();
    }

    private void SetAnimation()
    {
        if (m_direction.x == 0 && m_direction.y == 0)
        {
            TriggAnimators(IDLE_TRIGGER_NAME);
        }
        else if (m_direction.y > 0 && m_direction.x == 0)
        {
            SpriteRendererFlipX(false);
            TriggAnimators(MOVE_UP_TRIGGER_NAME);
        }
        else if (m_direction.y < 0 && m_direction.x == 0)
        {
            SpriteRendererFlipX(false);
            TriggAnimators(MOVE_DOWN_TRIGGER_NAME);
        }
        else if (m_direction.x > 0)
        {
            SpriteRendererFlipX(false);
            TriggAnimators(MOVE_TRIGGER_NAME);
        }
        else if (m_direction.x < 0)
        {
            SpriteRendererFlipX(true);
            TriggAnimators(MOVE_TRIGGER_NAME);
        }
    }

    public void OpenInventory(InputAction.CallbackContext value)
    {
        m_inventoryScreen.OpenScreen();
    }

    private void UseCloth(OnUseClothEvent args)
    {
        foreach (CharacterAnimationController animation in m_characterAnimatedParts)
        {
            animation.ChangeAnimatorController(args.m_scriptableItem.ItemControllerAnimator, args.m_scriptableItem.ItemType);
        }
    }

    private void RemoveCloth(OnRemoveClothEvent args)
    {
        foreach (CharacterAnimationController animation in m_characterAnimatedParts)
        {
            animation.RemoveAnimatorController(args.m_ScriptableItemItem.ItemType);
        }
    }

    private void TriggAnimators(string triggerName)
    {
        foreach (CharacterAnimationController animation in m_characterAnimatedParts)
        {
            animation.SetAnimationTrigger(triggerName);
        }
    }

    private void SpriteRendererFlipX(bool flipX)
    {
        foreach (CharacterAnimationController animation in m_characterAnimatedParts)
        {
            animation.FlipSprite(flipX);
        }
    }
}
