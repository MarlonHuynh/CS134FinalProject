/*

Purpose: Used to manage shop feature on the computer.

*/

using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Refs")]
    public DesktopManager desktopManager; 
    public GoalsManager goalsManager; 
    public MeditationRoomManager meditationRoomManager;
    [Header("Internal vars for debugging only (DO NOT EDIT)")]
    public bool purchasedFoodForTheDay; 
    public bool purchasedMeditationRoomForTheDay; 

    void start()
    {
        purchasedFoodForTheDay = false; 
        purchasedMeditationRoomForTheDay = false; 
    }
    // Purchase food (opens food slot) on computer
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

    // Purchase opening of meditation room 
    public void purchaseMeditationRoomDoorOpen()
    {
        if (desktopManager.captchaPoints >= 1 && purchasedMeditationRoomForTheDay == false)
        {
            desktopManager.subtractPoints(1); 
            meditationRoomManager.openMeditationRoom.openMeditationDoor(); 
            purchasedMeditationRoomForTheDay = true; 
        }
    }

    // Reset purchase limits (only 1 of each for now)
    public void resetPurchaseLimits()
    {
        purchasedFoodForTheDay = false; 
        purchasedMeditationRoomForTheDay = false; 
    }


}
