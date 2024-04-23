using System.Reflection;
using StayInTarkov;
using Comfort.Common;
using EFT;
using HarmonyLib;
using UnityEngine;

// AKI = SIT
// GClass3205 = GClass3216
// GClass1249 = Quest

namespace GTFO
{
    public class TryNotifyConditionChangedPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {

            return AccessTools.Method(typeof(GClass3216), nameof(GClass3216.TryNotifyConditionChanged));
        }

        [PatchPostfix]
        public static void Postfix(ref Quest quest)
        {
            if (Singleton<GameWorld>.Instance == null)
            {
                Debug.LogError("TryNotifyConditionChanged Postfix: GameWorld instance is null.");
                return;
            }

            if (Singleton<GameWorld>.Instance.TryGetComponent<GTFOComponent>(out GTFOComponent gtfo))
            {
                if (gtfo != null && quest != null)
                {
                    if (GTFOComponent.questManager != null)
                    {
                        GTFOComponent.questManager.OnQuestsChanged(quest);
                    }
                    else
                    {
                        Debug.LogError("TryNotifyConditionChanged Postfix: QuestManager is null within GTFOComponent.");
                    }
                }
                else
                {
                    Debug.LogError($"TryNotifyConditionChanged Postfix: Either 'gtfo' is null ({gtfo == null}) or 'quest' is null ({quest == null}).");
                }
            }
            else
            {
                Debug.LogError("TryNotifyConditionChanged Postfix: Failed to retrieve GTFOComponent from GameWorld.");
            }


        }
    }
}