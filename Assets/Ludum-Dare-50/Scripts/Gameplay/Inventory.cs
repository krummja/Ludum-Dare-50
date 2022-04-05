using UnityEngine;

namespace Gameplay
{
    public enum GameItems
    {
        NONE,
        SLINGSHOT,
        FAN_CASE,
        FAN,
        REFRIGERANT,
        WATER,
        CHEMICAL,
        BAKING_SODA,
        GUM,
        KEY_PRESSING,
        KEY_CUT,
        FUSE,
        UNKNOWN_2,
        SMOKE_BOMB,
        ROACHES_1,
        ROACHES_2,
        ROACHES_3
    }

    public class Inventory : BaseManager<Inventory>
    {
        public GameItems[] InventorySlots = new GameItems[15];

        public void AddToInventory(GameItems newItem)
        {
            if ( CheckInventory(newItem) ) ItemExists();
            else
            {
                switch ( newItem )
                {
                    case GameItems.SLINGSHOT:
                        GameManager.Instance.RequestNewMessage(1, "You found a slingshot!");
                        break;
                    // case GameItems.FAN_CASE:
                    //     GameManager.Instance.RequestNewMessage(1, "You found a fan case!");
                    //     break;
                    case GameItems.FAN:
                        GameManager.Instance.RequestNewMessage(1, "You found some fan blades!");
                        break;
                    case GameItems.REFRIGERANT:
                        GameManager.Instance.RequestNewMessage(1, "You found a canister of refrigerant!");
                        break;
                    case GameItems.WATER:
                        GameManager.Instance.RequestNewMessage(1, "You found a huge bottle of water!");
                        break;
                    case GameItems.CHEMICAL:
                        GameManager.Instance.RequestNewMessage(1, "You found an industrial chemical mix!");
                        break;
                    case GameItems.BAKING_SODA:
                        GameManager.Instance.RequestNewMessage(7, "You found a box of baking soda!");
                        break;
                    case GameItems.GUM:
                        GameManager.Instance.RequestNewMessage(8, "You found some chewing gum!");
                        break;
                    case GameItems.SMOKE_BOMB:
                        GameManager.Instance.RequestNewMessage(13, "You found one of your homemade smoke bombs!");
                        break;
                    case GameItems.KEY_PRESSING:
                        break;
                    case GameItems.KEY_CUT:
                        GameManager.Instance.RequestNewMessage(13, "You found a key stuck in the sand. You've seen " +
                                                                   "this somewhere before...");
                        break;
                    case GameItems.ROACHES_1:
                        GameManager.Instance.RequestNewMessage(13, "You found a bunch of roaches! Yuck!");
                        break;
                    case GameItems.ROACHES_2:
                        GameManager.Instance.RequestNewMessage(13, "You found a bunch of roaches! You added them to " +
                                                                   "your collection...");
                        break;
                    case GameItems.ROACHES_3:
                        GameManager.Instance.RequestNewMessage(13, "You found a bunch of roaches! A fine addition...");
                        break;
                }

                PlaceInInventory(newItem);
            }
        }

        public bool CheckInventory(GameItems item)
        {
            for ( int i = 0; i < 15; i++ )
                if ( InventorySlots[i] == item )
                    return true;
            return false;
        }

        public void RemoveFromInventory(GameItems item)
        {
            int removeIndex = -1;

            for ( int i = 0; i < 15; i++ )
            {
                if ( InventorySlots[i] == item )
                {
                    removeIndex = i;
                    break;
                }
            }

            if ( removeIndex >= 0 )
            {
                for ( int i = removeIndex; i < 14; i++ )
                    InventorySlots[i] = InventorySlots[i + 1];

                InventorySlots[14] = 0;
            }
        }

        public void ClearInventory()
        {
            switch ( GameManager.Instance.Day )
            {
                case DaysEnum.SUNDAY:
                    InventorySlots = new GameItems[15];
                    break;
                case DaysEnum.MONDAY:
                    RemoveFromInventory(GameItems.SLINGSHOT);
                    RemoveFromInventory(GameItems.ROACHES_2);
                    break;
                case DaysEnum.TUESDAY:
                    RemoveFromInventory(GameItems.ROACHES_3);
                    RemoveFromInventory(GameItems.GUM);
                    RemoveFromInventory(GameItems.REFRIGERANT);
                    break;
                case DaysEnum.WEDNESDAY:
                    RemoveFromInventory(GameItems.KEY_PRESSING);
                    RemoveFromInventory(GameItems.WATER);
                    break;
                case DaysEnum.THURSDAY:
                    RemoveFromInventory(GameItems.FAN);
                    break;
                case DaysEnum.FRIDAY:
                    break;
            }

        }

        protected override void OnAwake() {}

        private void ItemExists()
        {
            if ( !GameManager.Instance.IsGamePaused )
                GameManager.Instance.RequestNewMessage(18, "You already have this!");
        }

        private void PlaceInInventory(GameItems newItem)
        {
            for ( int i = 0; i < 15; i++ )
            {
                if ( InventorySlots[i] == 0 )
                {
                    InventorySlots[i] = newItem;
                    break;
                }
            }
        }
    }
}
