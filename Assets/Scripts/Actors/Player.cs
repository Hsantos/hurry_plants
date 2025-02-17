﻿using System;
using UnityEngine;

public class Player : SimpleStateMachine
{
    [SerializeField] public Pickable pickable;
    [SerializeField] public Picker picker;
    [SerializeField] public AirMovement airMovement;
    [SerializeField] public GroundMovement groundMovement;
    [SerializeField] public InputHandler inputHandler;
   

    private enum PlayerStates { Idle, Running, Flying, Captured }

    private void Start()
    {
        pickable.OnPicked.AddListener(() => currentState = PlayerStates.Captured);
        pickable.OnThrowed.AddListener(() => currentState = PlayerStates.Flying);

        currentState = PlayerStates.Idle;

    }

    public void OnColliderEnter(Type type)
    {
        Debug.LogWarning("OnColliderEnter : " + type.Name);
    }

    protected override void EarlyGlobalSuperUpdate()
    {
        if (inputHandler.ThrowButton)
            picker.Throw();
    }

    protected override void LateGlobalSuperUpdate()
    {

    }

    private void Running_FixedUpdate()
    {
        if (inputHandler.Direction != Vector3.zero) //Comment this to enable always moving mechanic
            groundMovement.Move(inputHandler.Direction);
        else
            currentState = PlayerStates.Idle;
    }


    private void Idle_EnterState()
    {
        pickable.SetIdle();
    }

    private void Idle_Update()
    {
        if (inputHandler.Direction != Vector3.zero)
            currentState = PlayerStates.Running;
    }

    private void Idle_ExitState()
    {
        
    }

    private void Captured_EnterState()
    {
    }

    private void Captured_UpdateState()
    {

    }

    private void Captured_ExitState()
    {
    }

    private void OnDrawGizmos()
    {
        var from = transform.position + Vector3.up;
        var to = from + transform.forward * 1.5f;
        Gizmos.DrawLine(from, to);
    }

}
