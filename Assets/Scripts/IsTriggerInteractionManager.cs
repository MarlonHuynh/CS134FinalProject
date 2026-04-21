/*

THIS IS A TEMPORARY SCRIPT TO AVOID CONFLICTS WITH INTERACTIONMANAGER + COMPUTER LOGIC. REPLACE WITH ACTUAL INTERACTION SCRIPT

*/

using UnityEngine;
using System.Collections; 

public class IsTriggerInteractionManager : MonoBehaviour
{
    public GoalsManager goalsManager; 
    
    public bool inBedTriggerRange, inMeditationRoomTriggerRange, inFoodDispenserTriggerRange, inKitchenTriggerRange; 

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (inBedTriggerRange)
            {
                goalsManager.playSleepCutscene();  
            }
            else if (inMeditationRoomTriggerRange)
            {
                goalsManager.meditate(); 
            }
            else if (inFoodDispenserTriggerRange && goalsManager.foodSlotOpen)
            {
                Debug.Log("Getting food! (IsTriggerInteractionManager)");
                goalsManager.getFood();  
            }
            else if (inKitchenTriggerRange)
            {
                goalsManager.consumeFood();  
            }
        }
    }
    void OnTriggerEnter(Collider other)
    { 
        if (other.name == "BedTriggerCollider")
        {
            inBedTriggerRange = true; 
        }
        else if (other.name == "KitchenTableTriggerCollider")
        {
            inKitchenTriggerRange = true; 
        }
        else if (other.name == "FoodSlotTriggerCollider")
        {
            inFoodDispenserTriggerRange = true; 
        }
        else if (other.name == "MeditationRoomTriggerCollider")
        {
            inMeditationRoomTriggerRange = true; 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "BedTriggerCollider")
        {
            inBedTriggerRange = false; 
        }
        else if (other.name == "KitchenTableTriggerCollider")
        {
            inKitchenTriggerRange = false; 
        }
        else if (other.name == "FoodSlotTriggerCollider")
        {
            inFoodDispenserTriggerRange = false; 
        }
        else if (other.name == "MeditationRoomTriggerCollider")
        {
            inMeditationRoomTriggerRange = false; 
        }
    }
}
