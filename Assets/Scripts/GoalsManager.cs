using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using System.Collections;

public class GoalsManager : MonoBehaviour
{
    public int dayIncludingFillerDays; // Date including filler days
    public int trueDay; // Day to keep track of plot
    public ChatManager chatManager; 
    public TMP_Text dayTextInSleepCutscene;
    public TMP_Text dayTextInGoalBar;      
    public TMP_Text goalText;   
    public Image itemUIImage; 
    public Sprite foodSprite; 
    public GameObject sleepBG; 
    public GameObject holdingBG; 
    public bool goalUseComputer, goalEatFood, goalMeditate; 
    public bool holdingFood, foodSlotOpen; 
    public GameObject player;

    void Start()
    {
        holdingFood = false; 
        foodSlotOpen = false;  
        dayIncludingFillerDays = 1;  
        trueDay = 1; 
        resetGoals(); 
        updateDayText(); 
        playSleepCutscene_ButDontMarkGoal(); 
        chatManager.switchTextBasedOnDay(dayIncludingFillerDays); 
    }

    void Update()
    {
        /*
        if (Input.GetKeyUp(KeyCode.L))
        {
            goalUseComputer = true; 
            updateGoalText(); 
        }*/
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

    
    public void updateGoalText()
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

    // Returns false if not met conditions for sleeping
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
            // Update chats
            chatManager.restrictChat();  
            // TODO : RESET CAPTCHAs so player can get more points 
        }
    } 

    public void playSleepCutscene_ButDontMarkGoal()
    {
        // Play sleep cutscene
        sleepBG.SetActive(true); 
        StartCoroutine(sleepCoroutine(2f));  
        // Reset Goals
        resetGoals();  
        updateGoalText(); 
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
        goalEatFood = true; 
        updateGoalText(); 
    }

    public void meditate()
    {
        goalMeditate = true; 
        updateGoalText(); 
    }
}
