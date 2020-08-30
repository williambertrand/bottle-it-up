using System;
using System.Collections.Generic;
using baseclasses;
using UnityEngine;
using extensions;
using HumanStateManagement;
using util;
using UnityEditor;

public class PlayerController : MonoBehaviorWithInputs
{
    public static PlayerController Instance; 
    private Vector3 _moveInput = Vector3.zero;
    private Rigidbody _rb;
    private Vector3 initialForward;
    public NeedleController needleController;
    public float moveSpeed = 3;
    public float timeSpentAsMonsterSec = 3;
    public Camera camera;
 
    private PerlinAxis _monstrosityNoise;

    public HashSet<GameObject> humanObjects = new HashSet<GameObject>();

    public float MonstrosityLevel { get; private set; } = 1;

    public float BalanceInput { get; private set; }
    
    public bool IsMonster { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;

        // Can't use this inline because it uses Time.fixedTime
        _monstrosityNoise = new PerlinAxis(0.2f);
        
        _rb = GetComponent<Rigidbody>();
        
        needleController = needleController == null ? GameObjectExtensions.FindComponentByTag<NeedleController>("Needle") : needleController;
        
        InputActions.Base.Let(i =>
        {
            i.Movement.AddListeners<Vector2>(
                performed: v => _moveInput = v.ToVector3Horizontal(),
                canceled: _ => _moveInput = Vector3.zero
            );

            i.Balance.AddListeners<float>(
                performed: f => BalanceInput = f,
                canceled: _ => BalanceInput = 0
            );
        });
        
        Human.OnHumanSpawn += HandleHumanSpawn;
        Human.OnHumanDiedOrLeftStore += HandleHumanDiedOrLeftStore;

        initialForward = gameObject.transform.forward;
    }

    private void OnDisable()
    {
        Human.OnHumanSpawn -= HandleHumanSpawn;
        Human.OnHumanDiedOrLeftStore -= HandleHumanDiedOrLeftStore;
    }

    private void Start()
    {
        needleController.AddNeedleReachedMaxListener(OnUserLosesIt);
    }

    private void OnDestroy()
    {
        if (needleController)
            needleController.RemoveNeedleReachedMaxListener(OnUserLosesIt);
    }

    private void OnUserLosesIt()
    {
        needleController.SetNeedleLocked(true);
        Invoke(nameof(ReturnToHumanForm), timeSpentAsMonsterSec);
        
        IsMonster = true;
    }

    private void ReturnToHumanForm()
    {
        IsMonster = false;
        needleController.ResetNeedleValue();
        needleController.SetNeedleLocked(false);
    }

    private void Update()
    {
        var camAdjustedMoveInput = Quaternion.FromToRotation(Vector3.forward, initialForward) * _moveInput;
        _rb.velocity = camAdjustedMoveInput * moveSpeed;
        transform.rotation = Quaternion.LookRotation(_rb.velocity.normalized);
    }

    private const float MetersVeryClose = 5;
    private const float MetersClose = 10;
    private const float MetersMidDistance = 20;
    private float CalculateCloseness()
    {
        var numHumansMidDistance = 0;
        var numHumansClose = 0;
        var numHumansVeryClose = 0;

        humanObjects.ForEach(go =>
        {
            var dist = (gameObject.transform.position - go.transform.position).magnitude;
            if (dist < MetersVeryClose) numHumansVeryClose++;
            else if (dist < MetersClose) numHumansClose++;
            else if (dist < MetersMidDistance) numHumansMidDistance++;
        });

        return (numHumansVeryClose * 0.33f + numHumansClose * 0.1f + numHumansMidDistance * 0.05f)
            .Clamp(max: 1f);
    }

    private void FixedUpdate()
    {
        var closenessToOtherHumans = CalculateCloseness();
        var noiseValue = _monstrosityNoise.GetValue().Squared();
        MonstrosityLevel = noiseValue.Interpolate(0, 0.25f) + closenessToOtherHumans.Interpolate(0, 0.75f);
    }


    private void OnDrawGizmos()
    {
        if (IsMonster)
        {
            Handles.Label(transform.position + (transform.up * 1.5f), "Monster");
        }
    }

    private void HandleHumanSpawn(GameObject g) => humanObjects.Add(g);

    private void HandleHumanDiedOrLeftStore(GameObject g) => humanObjects.Remove(g);
}
