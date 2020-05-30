using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : Interactable
{
    public UnityEvent onInteraction;

    public override void Use()
    {
        base.Use();

        onInteraction.Invoke();
    }
}
