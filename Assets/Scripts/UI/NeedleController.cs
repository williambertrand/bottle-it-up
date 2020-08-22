using baseclasses;
using extensions;
using UnityEngine;

public class NeedleController : MonoBehaviorWithInputs
{
    
    private float _balanceLevel;

    private float _balancePlayerInput;

    private PlayerController _playerController;

    private void Start()
    {
        _playerController = PlayerController.Instance;
    }

    private void FixedUpdate()
    {
        var balanceDelta = Time.deltaTime * CalcBalanceDeltaPerSec(_playerController.BalanceInput, _playerController.MonstrosityLevel);
        _balanceLevel = (_balanceLevel + balanceDelta).Clamp(-1, 1);
    }

    private void Update()
    {
        gameObject.transform.eulerAngles = new Vector3(0, 0, -90 * _balanceLevel);
    }

    private float CalcBalanceDeltaPerSec(float playerInput, float monstrosityValue)
    {
        var deltaFromPrevBalance = _balanceLevel * 0.9f;

        return (playerInput + deltaFromPrevBalance) * (1 + monstrosityValue.Interpolate(0, 5));
    }
}
