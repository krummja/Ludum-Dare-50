using UnityEngine;

namespace Gameplay
{
    public enum GameItems
    {
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
        SMOKE_BOMB,
        ROACHES
    }

    public class Inventory : BaseManager<Inventory>
    {
        public GameItems[] InventorySlots = new GameItems[15];

        public void AddToInventory(GameItems newItem)
        {
            if ( GameManager.Instance.IsGamePaused ) return;

            if ( CheckInventory(newItem) ) ItemExists();
            else
            {
                PlaceInInventory(newItem);

                switch ( newItem )
                {
                    case GameItems.SLINGSHOT:
                        GameManager.Instance.RequestNewMessage(1, "You found a slingshot!");
                        break;
                    case GameItems.FAN_CASE:
                        GameManager.Instance.RequestNewMessage(1, "You found a fan case!");
                        break;
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
                        break;
                    case GameItems.ROACHES:
                        break;
                }
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
