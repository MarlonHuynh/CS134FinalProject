/*

Purpose: Manages the general gameplay loop and keeps track of goals 

*/
using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using System.Collections; 
public class GoalsManager : MonoBehaviour
{ 
    [Header("Refs")]
    public DesktopManager desktopManager; 
    public ChatManager chatManager; 
    public MeditationRoomManager meditationRoomManager; 
    public PlayerMovement playerMovement; 
    public PlayerCamera playerCamera; 
    public ShopManager shopManager; 
    public InteractionManager interactionManager; 
    public TMP_Text dayTextInSleepCutscene;
    public TMP_Text dayTextInGoalBar;      
    public TMP_Text goalText;   
    public Image itemUIImage; 
    public Sprite foodSprite; 
    public Sprite foodSpriteWithEye; 
    public Sprite foodSpriteWithMetalSpoon; 
    public GameObject foodDisplayObj; 
    public Image foodDisplayImg; 
    public GameObject sleepBG; 
    public GameObject introObject; 
    public GameObject holdingBG; 
    public GameObject player;
    public GameObject loosePanelObj; 
    [Header("Audio Refs")]
    public AudioClip eatingAudio; 
    public AudioClip knockingAudio; 
    public AudioSource flex2DAudioSource; 
    public AudioSource flex2DAudioSource_looping; 
    public CaptchaManager captchaManager;

    [Header("Internal Vars for debugging (DONT EDIT)")]
    public int dayIncludingFillerDays; // Day including filler days
    public int trueDay; // Day to keep track of plot
    public bool goalUseComputer, goalEatFood, goalMeditate; 
    public bool holdingFood, foodSlotOpen;  
    public int AIAngerMeter;  
    public bool angerEndingReached; 
    public bool waitOnIntro; 
    public bool introActive; 

    void Start()
    {
        holdingFood = false; 
        foodSlotOpen = false;  
        dayIncludingFillerDays = 1;  
        trueDay = 1; 
        AIAngerMeter = 0; 
        angerEndingReached = false; 
        waitOnIntro = true; 
        introActive = true; 
        foodDisplayObj.SetActive(false); 
        loosePanelObj.SetActive(false); 
        resetGoals(); 
        updateDayText(); 
        initialSleepCutscene(); 
        chatManager.switchTextBasedOnDay(dayIncludingFillerDays); 
        interactionManager.disableInteraction(); 
    } 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && introActive)
        { 
            waitOnIntro = false; 
            introActive = false; 
            sleepBG.SetActive(false); 
            introObject.SetActive(false);  
            interactionManager.enableInteraction(); 
        }
    }
    
    // Resets goal
    void resetGoals()
    {
        goalUseComputer = false; 
        goalEatFood = false; 
        goalMeditate = false;  
        updateGoalText(); 
    }

    // Updates TMPro UI 
    void updateDayText()
    {
        dayTextInGoalBar.text = "Day " + dayIncludingFillerDays; 
        dayTextInSleepCutscene.text = "Day " + dayIncludingFillerDays; 
    }

    
    // Updates goal text in order of goals completion
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
 
    // Deals with sleeping and resets goals after sleeping
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
            // Reset Meditation door position  
            meditationRoomManager.openMeditationRoom.closeMeditationDoorImmediately(); 
            // Reset purchase limits
            shopManager.resetPurchaseLimits(); 
            // Reset CAPTCHAs so player can get more points 
            captchaManager.ResetForNewDay(trueDay);
            // Reset notifs on desktop
            desktopManager.resetDesktopNotification(); 

            if (trueDay > 4)
            {
                // Trigger ending 2 day
                loosePanelObj.SetActive(true); 
                interactionManager.disableOtherInteractablesBesidesEscapePanel = true; 
                meditationRoomManager.openMeditationRoom.openMeditationDoorImmediately(); 
                goalText.text = "Find a way out."; 
            }
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
            // Disable notifs on desktop
            desktopManager.disableDesktopNotification(); 
            // Reset Meditation door position  
            meditationRoomManager.openMeditationRoom.closeMeditationDoorImmediately(); 
            // Reset purchase limits
            shopManager.resetPurchaseLimits(); 
            // Reset CAPTCHAs so player can get more points 
            captchaManager.ResetForNewDay(dayIncludingFillerDays); 
            // Increment anger counter
            AIAngerMeter += 1; 
            if (AIAngerMeter >= 3)
            { 
                // Trigger Meatgrinder ending
                // Change goal text
                goalText.text = "Answer the door."; 
                // Disable all other interactables besides metal door for ending
                interactionManager.disableOtherInteractablesBesidesMetalDoor = true; 
                // Play knocking sound effect
                flex2DAudioSource_looping.clip = knockingAudio; 
                flex2DAudioSource_looping.Play();  
            }
            else
            {
                interactionManager.StartCoroutine(interactionManager.DelayedHintCoroutine("You forgot to complete all your tasks before sleeping. Hopefully nothing bad will happen.", 3f, 3f)); 
            }
        } 
    } 

    // Initial sleep cutscene without updating date to be played on the very first day only
    public void initialSleepCutscene() 
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

    // Plays wakeup animation
    IEnumerator sleepCoroutine(float delay)
    { 

        yield return new WaitForSeconds(delay);   
        
        while (waitOnIntro == true)
        {
            yield return null; // keep waiting 1 frame until waitOnIntro is false
        }

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

    // Updates inventory with food
    public bool getFood()
    { 
        if (foodSlotOpen){
            holdingBG.SetActive(true); 
            if (trueDay == 3)
            {
                itemUIImage.sprite = foodSpriteWithEye; 
            }
            else if (trueDay == 4)
            {
                itemUIImage.sprite = foodSpriteWithMetalSpoon; 
            }
            else{
                itemUIImage.sprite = foodSprite; 
            }
            holdingFood = true;  
            return true; 
        }
        else
        {
            return false; 
        }
    }

    // Consumes held food and display food eating cutscene
    public void consumeFood()
    {
        foodSlotOpen = false; 
        holdingBG.SetActive(false); 
        itemUIImage.sprite = null; 
        holdingFood = false; 
        // Play eating animation
        StartCoroutine(eatAnimation()); 
        goalEatFood = true; 
        updateGoalText(); 
    }

    // Disables some features while eating cutscene plays
    private IEnumerator eatAnimation()
    {   
        playerMovement.disableMovement();
        if (trueDay == 3)
        {
            foodDisplayImg.GetComponent<Image>().sprite = foodSpriteWithEye; 
        }
        else if (trueDay == 4)
        {
            foodDisplayImg.GetComponent<Image>().sprite = foodSpriteWithMetalSpoon; 
        }
        else{
            foodDisplayImg.GetComponent<Image>().sprite = foodSprite; 
        }
        foodDisplayObj.SetActive(true); 
        flex2DAudioSource.clip = eatingAudio; 
        flex2DAudioSource.Play(); 
        yield return new WaitForSeconds(3f); 
        foodDisplayObj.SetActive(false); 
        playerMovement.enableMovement(); 
    }

    // Meditate and plays cutscene 
    public void meditate()
    {
        goalMeditate = true; 
        meditationRoomManager.playMeditationRoomCutscene(); 
        updateGoalText(); 
    } 
}
