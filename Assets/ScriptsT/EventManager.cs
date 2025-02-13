using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
   static List<ParentTank> instances = new List<ParentTank>();
    static UnityAction<int> unityActionspalyer;
    static UnityAction<int> unityActionsEnemy;

    public static void AddEvenInvoker(ParentTank invoker)
    {
        instances.Add(invoker);
        if (unityActionsEnemy != null)
        {
            invoker.AddenemyHitEvent(unityActionsEnemy);
        }
        else
        {
            Debug.Log("NULL enemy");
        }
        if (unityActionspalyer != null)
        {
            Debug.Log("Assigning player hit listener to invoker: " + invoker.name);

            invoker.AddplayerHitEvent(unityActionspalyer);
        }
        else
        {
            Debug.Log("NULL player");
        }
    }
    public static void AddPlayerEventListenr(UnityAction<int> action)
    {
        unityActionspalyer=action;
        foreach (ParentTank tank in instances) { 
         tank.AddplayerHitEvent(action);
        }
    }
    public static void AddEnemyEventListenr(UnityAction<int> action)
    {
        unityActionsEnemy = action;
        foreach (ParentTank tank in instances)
        {
            tank.AddenemyHitEvent(action);
        }
    }
}
