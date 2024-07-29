using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Player : LivingCreature
    {
        public int Gold { get; set; }
        public int ExperiencePoints { get; set; }
        public int Level { get; set; }

        public Location CurrentLocation { get; set; }
        public List<InventoryItem> Inventory { get; set; }
        public List<PlayerQuest> Quests { get; set; }


        public Player(int currentHitPoints, int maximumHitPoints,
            int gold, int experiencePoints, int level) :
            base(currentHitPoints, maximumHitPoints)
        {
            Gold = gold;
            ExperiencePoints = experiencePoints;
            Level = level;

            Inventory = new List<InventoryItem>();
            Quests = new List<PlayerQuest>();
        }

        public bool HasRequiredItemToEnterThisLocation(Location location)
        {
            if (location.ItemRequiredToEnter == null)
            {
                //no required item, return true
                return true;
            }

            //see if player has required item in inventory
            foreach (InventoryItem ii in Inventory)
            {
                if (ii.Details.ID == location.ItemRequiredToEnter.ID)
                {
                    //we have the item, return true
                    return true;
                }
            }

            //we don't have the item, so return false
            return false;
        }

        public bool HasThisQuest(Quest quest)
        {
            foreach (PlayerQuest playerQuest in Quests)
            {
                if (playerQuest.Details.ID == quest.ID) { return true; }
            }
            return false;
        }

        public bool CompletedThisQuest(Quest quest)
        {
            foreach (PlayerQuest playerQuest in Quests)
            {
                if (playerQuest.Details.ID == quest.ID)
                {
                    return playerQuest.IsCompleted;
                }
            }
            return false;
        }

        public bool HasAllQuestsCompletionItems(Quest quest)
        {
            //see if player has all the items to complete the quest
            foreach (QuestCompletionItem qci in quest.QuestCompletionItem)
            {
                bool foundItemInPlayersInventory = false;

                //check each item to see if they have it and enough of it.
                foreach (InventoryItem ii in Inventory)
                {
                    //the player has the item
                    if (ii.Details.ID == qci.Details.ID)
                    {
                        foundItemInPlayersInventory = true;
                        //the player does not have enough item to cmplete quest
                        if (ii.Quantity < qci.Quantity)
                        {
                            return false;
                        }

                    }
                }

                //the player does not have item in inventory
                if (!foundItemInPlayersInventory)
                {
                    return false;
                }
            }
            //if we are here, the player does have all the items needed.
            return true;
        }
   
        public void RemoveQuestCompletionItems(Quest quest)
        {
            foreach (QuestCompletionItem qci in quest.QuestCompletionItem)
            {
               foreach (InventoryItem ii in Inventory)
                {
                    if(ii.Details.ID == qci.Details.ID)
                    {
                        //substract the quantity needed to complete the quest
                        ii.Quantity -= qci.Quantity;
                        break;
                    }
                }
            }
        }
    
    
    }
}
