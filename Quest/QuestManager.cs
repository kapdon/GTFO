﻿using EFT;

namespace GTFO
{
    internal class QuestManager
    {

        internal QuestDataService questDataService;

        public void Initialize(ref GameWorld gameWorld, ref Player player)
        {
            questDataService = new QuestDataService(ref gameWorld, ref player);
            SetupInitialQuests();
        }
        public void Deinitialize()
        {
            GTFOComponent.Logger.LogInfo("Deinitializing QuestManager.");

            // Perform any cleanup required for the quest data service
            if (questDataService != null)
            {
                questDataService.Cleanup();
                questDataService = null;
            }
        }
        internal void SetupInitialQuests()
        {
            if (!StayInTarkov.AkiSupport.Singleplayer.Utils.InRaid.RaidChangesUtil.IsScavRaid)
            {
                GTFOComponent.Logger.LogInfo("Calling Reload Quest Data from SetupInitial Quests");
                questDataService.InitialQuestData(ZoneDataHelper.GetAllTriggers());
            }
            else
            {
                GTFOComponent.Logger.LogInfo("Not calling Setup Quests as its a SCAV Raid.");
            }
        }

        internal void OnQuestsChanged(Quest bsgQuest)
        {
            if (!StayInTarkov.AkiSupport.Singleplayer.Utils.InRaid.RaidChangesUtil.IsScavRaid)
            {
#if DEBUG
                GTFOComponent.Logger.LogInfo("Calling Update Quest Completed Conditions from OnQuestsChanged");
#endif
                questDataService.UpdateQuestCompletedConditions(bsgQuest);
            }
        }

        internal void OnConditionalQuestsChanged(string id)
        {
            if (!StayInTarkov.AkiSupport.Singleplayer.Utils.InRaid.RaidChangesUtil.IsScavRaid)
            {
#if DEBUG
                GTFOComponent.Logger.LogInfo("Calling Update Quest Completed Conditionals from OnQuestsChanged");
#endif
                questDataService.UpdateQuestCompletedConditionals(id);
            }
        }
    }
}
