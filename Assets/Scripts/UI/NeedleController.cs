using System;
using baseclasses;
using Boo.Lang;
using extensions;
using UnityEngine;
using static util.RandomExtensions;

public class NeedleController : MonoBehaviorWithInputs
{
    private List<Action> _needleReachedMaxListeners;
    
    private float _balanceLevel;

    private float _balancePlayerInput;

    private PlayerController _playerController;

    private bool _isLocked = false;

    private void Awake()
    {
        _needleReachedMaxListeners = new List<Action>();
    }

    private void Start()
    {
        _playerController = PlayerController.Instance;

        ResetNeedleValue();
    }

    private void FixedUpdate()
    {
        if (_isLocked) return;

        // set the balance level to slightly off so it's never totally static.
        if (_balanceLevel.EqZero()) ResetNeedleValue();
        
        var balanceDelta =
            Time.deltaTime * CalcBalanceDeltaPerSec(_playerController.BalanceInput, _playerController.MonstrosityLevel);
        _balanceLevel = (_balanceLevel + balanceDelta).Clamp(-1, 1);
        
        if (IsUnbalanced()) _needleReachedMaxListeners.InvokeAll();
    }

    private bool IsUnbalanced() => Math.Abs(_balanceLevel).SoftEquals(1);

    private void Update()
    {
        gameObject.transform.eulerAngles = new Vector3(0, 0, -90 * _balanceLevel);
    }

    private float CalcBalanceDeltaPerSec(float playerInput, float monstrosityValue)
    {
        var deltaFromPrevBalance = _balanceLevel * 0.9f;

        return (playerInput + deltaFromPrevBalance) * (1 + monstrosityValue.Interpolate(0, 5));
    }

    public void AddNeedleReachedMaxListener(Action onNeedleReachedMaxListener) =>
        _needleReachedMaxListeners.Add(onNeedleReachedMaxListener);
    
    public void RemoveNeedleReachedMaxListener(Action onNeedleReachedMaxListener) =>
        _needleReachedMaxListeners.Remove(onNeedleReachedMaxListener);

    public void SetNeedleLocked(bool isLocked) => _isLocked = isLocked;
    
    public void ResetNeedleValue() => _balanceLevel = RandomSign * 0.01f;
}
