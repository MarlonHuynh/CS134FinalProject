using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using System.Collections;
using System.Diagnostics;

public class GoalsManager : MonoBehaviour
{ 
    [Header("Refs")]
    public ChatManager chatManager; 
    public MeditationRoomManager meditationRoomManager; 
    public PlayerMovement playerMovement; 
    public ShopManager shopManager; 
    public TMP_Text dayTextInSleepCutscene;
    public TMP_Text dayTextInGoalBar;      
    public TMP_Text goalText;   
    public Image itemUIImage; 
    public Sprite foodSprite; 
    public GameObject foodDisplayObj; 
    public AudioClip eatingAudio; 
    public AudioSource flex2DAudioSource; 
    public GameObject sleepBG; 
    public GameObject holdingBG; 
    public GameObject player;

    [Header("Internal Vars for debugging (DONT EDIT)")]
    public int dayIncludingFillerDays; // Day including filler days
    public int trueDay; // Day to keep track of plot
    public bool goalUseComputer, goalEatFood, goalMeditate; 
    public bool holdingFood, foodSlotOpen;  
    public int AIAngerMeter; 

    void Start()
    {
        holdingFood = false; 
        foodSlotOpen = false;  
        dayIncludingFillerDays = 1;  
        trueDay = 1; 
        AIAngerMeter = 0; 
        foodDisplayObj.SetActive(false); 
        resetGoals(); 
        updateDayText(); 
        initialSleepCutscene(); 
        chatManager.switchTextBasedOnDay(dayIncludingFillerDays); 
    } 
    
    void resetGoals()
    {
        goalUseComputer = false; 
        goalEatFood = false; 
        goalMeditate = false;  
        updateGoalText(); 
    }

    void updateDayText()
    {
        dayTextInGoalBar.text = "Day " + dayIncludingFillerDays; 
        dayTextInSleepCutscene.text = "Day " + dayIncludingFillerDays; 
    }

    
    public void updateGoalText() // Update goal text in order of goals completion
    {
        if (!goalUseComputer)
        {
            goalText.text = "Do Work on Computer"; 
            return; 
        }
        else if (!goalEatFood)
        {
            goalText.text = "Eat Food"; 
            return; 
        }
        else if (!goalMeditate)
        {
            goalText.text = "Meditate"; 
            return; 
        }
        else
        {
            goalText.text = "Sleep"; 
            return; 
        }
    }
 
    public void checkIfTasksCompletedAndSleep(){
        // Check if fulfilled conditions for sleeping
        if (goalUseComputer && goalEatFood && goalMeditate){
            // Play sleep cutscene
            sleepBG.SetActive(true); 
            StartCoroutine(sleepCoroutine(2f));  
            // Reset Goals
            resetGoals();  
            updateGoalText(); 
            // Update day text
            trueDay += 1; 
            dayIncludingFillerDays += 1; 
            updateDayText();   
            // Update chats based on true day
            chatManager.enableChat(); 
            chatManager.switchTextBasedOnDay(trueDay); 
            // TODO : RESET CAPTCHAs so player can get more points 


        } 
        else{ // Sleep without completing tasks
            // Play sleep cutscene
            sleepBG.SetActive(true); 
            StartCoroutine(sleepCoroutine(2f));  
            // Reset Goals
            resetGoals();  
            updateGoalText(); 
            // Update day text
            dayIncludingFillerDays += 1; 
            updateDayText();   
            // Restrict chat as punishment
            chatManager.restrictChat();  
            // TODO : RESET CAPTCHAs so player can get more points 

            
            // Increment anger counter
            AIAngerMeter += 1; 
            if (AIAngerMeter >= 3)
            { 
                // Trigger Meatgrinder ending
                
            }
        }
        // Reset Meditation door positioneither way
        meditationRoomManager.openMeditationRoom.closeMeditationDoorImmediately(); 
        // Reset purchase limits
        shopManager.resetPurchaseLimits(); 
    } 

    public void initialSleepCutscene() // Initial sleep cutscene without updating date - PLAYS ONCE. 
    {
        // Play sleep cutscene
        sleepBG.SetActive(true); 
        StartCoroutine(sleepCoroutine(2f));  
        // Reset Goals
        resetGoals();  
        updateGoalText(); 
        // Update chats based on true day
        chatManager.enableChat(); 
        chatManager.switchTextBasedOnDay(trueDay); 
    }

    IEnumerator sleepCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);  
        sleepBG.SetActive(false);

        if (player != null)
        {
            // hide capsule mesh
            MeshRenderer capsuleMesh = player.GetComponentInChildren<MeshRenderer>();
            if (capsuleMesh != null) capsuleMesh.enabled = false;

            // trigger wakeup animation
            Animator anim = player.GetComponentInChildren<Animator>();
            if (anim != null)
                anim.SetTrigger("wakeup");

            // wait for animation to finish then show capsule again
            yield return new WaitForSeconds(2f);
            if (capsuleMesh != null) capsuleMesh.enabled = true;
        }
    }

    public bool getFood()
    { 
        if (foodSlotOpen){
            holdingBG.SetActive(true); 
            itemUIImage.sprite = foodSprite; 
            holdingFood = true;  
            return true; 
        }
        else
        {
            return false; 
        }
    }

    public void consumeFood()
    {
        holdingBG.SetActive(false); 
        itemUIImage.sprite = null; 
        holdingFood = false; 
        // Play eating animation
        StartCoroutine(eatAnimation()); 
        goalEatFood = true; 
        updateGoalText(); 
    }

    private IEnumerator eatAnimation()
    {   
        playerMovement.disableMovement(); 
        foodDisplayObj.SetActive(true); 
        flex2DAudioSource.clip = eatingAudio; 
        flex2DAudioSource.Play(); 
        yield return new WaitForSeconds(3f); 
        foodDisplayObj.SetActive(false); 
        playerMovement.enableMovement(); 
    }

    public void meditate()
    {
        goalMeditate = true; 
        meditationRoomManager.playMeditationRoomCutscene(); 
        updateGoalText(); 
    }
}
