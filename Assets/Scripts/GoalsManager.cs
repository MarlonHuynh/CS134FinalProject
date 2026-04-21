using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using System.Collections;

public class GoalsManager : MonoBehaviour
{
    public int day; 
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
        day = 1;  
        resetGoals(); 
        updateDayText(); 
        playSleepCutscene_ButDontMarkGoal(); 
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
        dayTextInGoalBar.text = "Day " + day; 
        dayTextInSleepCutscene.text = "Day " + day; 
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
    public bool checkIfCanSleepAndSleepIfAble(){
        // Check if fulfilled conditions for sleeping
        if (goalUseComputer && goalEatFood && goalMeditate){
            // Play sleep cutscene
            sleepBG.SetActive(true); 
            StartCoroutine(delayCoroutine(2f));  
            // Reset Goals
            resetGoals();  
            updateGoalText(); 
            // Update day text
            day += 1; 
            updateDayText();  
            return true; 
        }
        else{
            Debug.Log("Cannot sleep. Have not finished all daily tasks."); 
            return false; 
        }
    } 

    public void playSleepCutscene_ButDontMarkGoal()
    {
        // Play sleep cutscene
        sleepBG.SetActive(true); 
        StartCoroutine(delayCoroutine(2f));  
        // Reset Goals
        resetGoals();  
        updateGoalText(); 
    }

    IEnumerator delayCoroutine(float delay)
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

    public void getFood()
    { 
        holdingBG.SetActive(true); 
        itemUIImage.sprite = foodSprite; 
        holdingFood = true;  
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
