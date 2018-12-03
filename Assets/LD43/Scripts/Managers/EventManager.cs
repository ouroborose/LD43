using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VuLib;

public static class EventManager  {
    public static readonly EventCallback OnStartGame = new EventCallback();
    public static readonly EventCallback<float> OnProgress = new EventCallback<float>();
    public static readonly EventCallback OnFinale = new EventCallback();

    public static readonly EventCallback OnLoseGame = new EventCallback();
    public static readonly EventCallback OnWinGame = new EventCallback();
}
