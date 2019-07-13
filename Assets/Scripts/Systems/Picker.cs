﻿using UnityEngine;

public class Picker : MonoBehaviour
{
    public Transform PickedPoint;
    public Pickable Pickable;
    private Animator Animator = null;

    private void OnTriggerEnter(Collider other)
    {
        // Check if we are already carrying something
        if (Pickable != null) return;

        var pickable = other.GetComponent<Pickable>();
        if (pickable == null) return;

        if (pickable.Pick(this))
            Pick(pickable);
    }

    private void Pick(Pickable pickable)
    {
        Pickable = pickable;
        Pickable.transform.SetParent(PickedPoint);
        Pickable.transform.rotation = PickedPoint.rotation;
        Pickable.transform.localPosition = Vector3.zero;
    }

    public void Throw()
    {
        if (Pickable == null) return;

        Animator?.SetTrigger("Throw");
        Pickable.SetThrow();
        Pickable = null;
    }
}