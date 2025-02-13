using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManagerGTA 
{
    static GunAnimationController Invoker;
    static UnityAction<int> listenr;
    // Start is called before the first frame update
    public static void AddEvenInvoker(GunAnimationController controller)
    {
        Invoker = controller;
        if (listenr != null)
        {
            Invoker.AddenemyHitEvent(listenr);
        }
    }
    public static void AddPlayerEventListenr(UnityAction<int> action)
    {
        listenr = action;
        if (Invoker != null)
        {
            Invoker.AddenemyHitEvent(listenr);
        }
    }

}
