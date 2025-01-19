using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Define the interface
public interface Activable
{
    bool isHeld { get; set; }
    void ActivateItem();

    void OnPickup();
}