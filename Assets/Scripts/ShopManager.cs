using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public DesktopManager desktopManager; 
    public GoalsManager goalsManager; 
    public MeditationRoomManager meditationRoomManager;
    public bool purchasedFoodForTheDay; 
    public bool purchasedMeditationRoomForTheDay; 

    void start()
    {
        purchasedFoodForTheDay = false; 
        purchasedMeditationRoomForTheDay = false; 
    }
    public void purchaseFood()
    {
        if (desktopManager.captchaPoints >= 2 && purchasedFoodForTheDay == false)
        {
            desktopManager.subtractPoints(2); 
            goalsManager.foodSlotOpen = true; 
            goalsManager.updateGoalText(); 
            purchasedFoodForTheDay = true; 
        }
    }
    public void purchaseMeditationRoomDoorOpen()
    {
        if (desktopManager.captchaPoints >= 1 && purchasedMeditationRoomForTheDay == false)
        {
            desktopManager.subtractPoints(1); 
            meditationRoomManager.openMeditationRoom.openMeditationDoor(); 
            purchasedMeditationRoomForTheDay = true; 
        }
    }
    public void resetPurchaseLimits()
    {
        purchasedFoodForTheDay = false; 
        purchasedMeditationRoomForTheDay = false; 
    }


}
