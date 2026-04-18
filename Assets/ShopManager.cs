using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public DesktopManager desktopManager; 
    public GoalsManager goalsManager; 
    public MeditationRoomManager meditationRoomManager;

    public void purchaseFood()
    {
        if (desktopManager.captchaPoints >= 3)
        {
            desktopManager.captchaPoints -= 3; 
            goalsManager.getFood(); 
        }
    }
    public void purchaseMeditationRoomDoorOpen()
    {
        if (desktopManager.captchaPoints >= 1)
        {
            desktopManager.captchaPoints -= 1; 
            meditationRoomManager.openMeditationRoom.openMeditationDoor(); 
        }
    }


}
