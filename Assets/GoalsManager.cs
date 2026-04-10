using UnityEngine;
using UnityEngine.UI; 
using TMPro;


public class GoalsManager : MonoBehaviour
{
    public int day; 
    public TMP_Text dayText;   

    public bool goalUseComputer, goalEatFood, goalMeditate, goalSleep; 

    void Start()
    {
        day = 1; 
        resetGoals(); 
        updateDayText(); 
    }

    void resetGoals()
    {
        goalUseComputer = false; 
        goalEatFood = false; 
        goalMeditate = false; 
        goalSleep = false; 
    }

    void updateDayText()
    {
        dayText.text = "Day " + day; 
    }

    void checkAndProgressDay()
    {
        if (goalUseComputer && goalEatFood && goalMeditate && goalSleep){
            day += 1; 
            updateDayText(); 
        }
    }
}
