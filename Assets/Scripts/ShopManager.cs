using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public DesktopManager desktopManager; 
    public GoalsManager goalsManager; 
    public MeditationRoomManager meditationRoomManager;

    public void purchaseFood()
    {
        if (desktopManager.captchaPoints >= 2)
        {
            desktopManager.subtractPoints(2); 
            goalsManager.foodSlotOpen = true; 
            goalsManager.updateGoalText(); 
        }
    }
    public void purchaseMeditationRoomDoorOpen()
    {
        if (desktopManager.captchaPoints >= 1)
        {
            desktopManager.subtractPoints(1); 
            meditationRoomManager.openMeditationRoom.openMeditationDoor(); 
        }
    }


}
