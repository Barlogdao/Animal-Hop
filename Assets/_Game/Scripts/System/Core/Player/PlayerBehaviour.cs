using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Animator))]

public class PlayerBehaviour : MonoBehaviour
{
    private const string JumpTrigger = "Jump";

    PlayerMovement _playerMovement;
    PlayerInput _playerInput;
    Animator _animator;

    [SerializeField] private PlayerRenderer _playerRenderer;
    [SerializeField] private AnimalProvider _animalProvider;
    [SerializeField] private PlayerDataProvider _playerDataProvider;

    bool _triggerFirstMove = false;

    Vector3 lastCollidePosition;

    public event Action OnFirstJump;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerInput = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _playerDataProvider.AnimalChanged += SetAnimalView;
    }

    private void OnDisable()
    {
        _playerDataProvider.AnimalChanged -= SetAnimalView;
    }

    private void Start()
    {
        int id = _playerDataProvider.GetCurrentAnimalID();
        SetAnimalView(id);
    }

    private void SetAnimalView(int id)
    {
        AnimalConfig config = _animalProvider.GetConfig(id);
        _playerRenderer.SetRenderer(config);
    }

    public void FirstJump()
    {
        if (_triggerFirstMove) return;

        _triggerFirstMove = true;
        OnFirstJump?.Invoke();
        Jump();
    }

    private void Jump()
    {
        _playerMovement.Jump();
        _animator.SetTrigger(JumpTrigger);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_triggerFirstMove) return;
        Vector3 contactNormal = collision.GetContact(0).normal;
        if (Mathf.Abs(contactNormal.x) == 1f) return;

        //lastCollidePosition = transform.position;
        lastCollidePosition = collision.gameObject.GetComponentInParent<Platform>().SavePoint;
        Jump();

        SoundController.Instance.PlayAudio(AudioType.JUMP);
    }

    public void OnGameOver()
    {
        gameObject.SetActive(false);
    }

    internal void Revive()
    {
        _triggerFirstMove = false;
        _playerMovement.Revive(lastCollidePosition);
        _playerInput.ResetFirstMove();
        gameObject.SetActive(true);
    }
}
