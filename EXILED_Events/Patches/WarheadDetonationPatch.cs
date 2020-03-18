﻿using System;
using Harmony;
using Mirror;
using UnityEngine;

namespace EXILED.Patches
{
    [HarmonyPatch(typeof(AlphaWarheadController), nameof(AlphaWarheadController.Detonate))]
    public class WarheadDetonationPatch
    {
        public static void Prefix(AlphaWarheadController __instance)
        {
            try
            {
                Events.InvokeWarheadDetonation();
            }
            catch (Exception e)
            {
                Log.Error($"Warhead Detonation event error: {e}");
            }
        }
    }

    [HarmonyPatch(typeof(AlphaWarheadController), nameof(AlphaWarheadController.CancelDetonation), new Type[]{typeof(GameObject)})]
    public class WarheadCancelEvent
    {
        public static bool Prefix(AlphaWarheadController __instance, GameObject disabler)
        {
            ServerLogs.AddLog(ServerLogs.Modules.Warhead, "Detonation cancelled.", ServerLogs.ServerLogType.GameEvent);
            if (!__instance.inProgress || __instance.timeToDetonation <= 10.0)
                return false;
            if (__instance.timeToDetonation <= 15.0 && disabler != null)
                __instance.GetComponent<PlayerStats>().TargetAchieve(disabler.GetComponent<NetworkIdentity>().connectionToClient, "thatwasclose");

            bool allow = true;
            
            Events.InvokeWarheadCancel(disabler, ref allow);

            return allow && !EventPlugin.WarheadLocked;
        }
    }

    [HarmonyPatch(typeof(AlphaWarheadController), nameof(AlphaWarheadController.StartDetonation))]
    public class WarheadStartEvent
    {
        public static bool Prefix(AlphaWarheadController __instance)
        {
            if (Recontainer079.isLocked)
                return false;
            __instance.doorsOpen = false;
            ServerLogs.AddLog(ServerLogs.Modules.Warhead, "Countdown started.", ServerLogs.ServerLogType.GameEvent);
            if ((AlphaWarheadController._resumeScenario != -1 || __instance.scenarios_start[AlphaWarheadController._startScenario].SumTime() != (double) __instance.timeToDetonation) && (AlphaWarheadController._resumeScenario == -1 || __instance.scenarios_resume[AlphaWarheadController._resumeScenario].SumTime() != (double) __instance.timeToDetonation))
                return false;
            bool allow = true;
            Events.InvokeWarheadStart(ref allow);
            if (allow)
                __instance.NetworkinProgress = true;
            return false;
        }
    }
}