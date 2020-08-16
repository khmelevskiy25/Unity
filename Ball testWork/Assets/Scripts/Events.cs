using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Events 
{
    public static event Action OnCleared;

    public static void NotifyAllTargetsCleared()
    {
        if (OnCleared != null)
            OnCleared();
    }
}
