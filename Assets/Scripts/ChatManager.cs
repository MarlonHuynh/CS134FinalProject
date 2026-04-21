using UnityEngine;
using UnityEngine.UI; 
using TMPro; 

public class ChatManager : MonoBehaviour
{
    public Image text1BGImage, text2BGImage, text3BGImage, text4BGImage, text5BGImage, text6BGImage; 
    public TMP_Text text1Obj, text2Obj, text3Obj, text4Obj, text5Obj, text6Obj; 

    void Start(){
        switchTextBasedOnDay(1); 
    }

    public void switchTextBasedOnDay(int day){
        switch (day){
            case 1: 
                text1Obj.text = "Hey there!"; 
                text2Obj.text = "Hey Bria.";
                text3Obj.text = "Good to see you on the grind again.";
                text4Obj.text = "I guess... Every day seems the same though. It's tiring.";
                text5Obj.text = "I get you. But there's nothing we can do, so might as well make the most of it."; 
                text6Obj.text = "I guess."; 
            break; 
            case 2: 
                text1Obj.text = "I hate the food here. It always taste the same."; 
                text2Obj.text = "Really? Seems fine to me.";
                text3Obj.text = "A Cake like in those CAPTCHA images sounds good. Wonder if there's other kinds of food left in the world.";
                text4Obj.text = "Maybe. Maybe the nutritional cubes are all they have left.";
                text5Obj.text = "What kind of food would you like to eat? Y'know, if you could pick one.";
                text6Obj.text = "I can't say.";
            break; 
            case 3: 
                text1Obj.text = "You know, recently, I've been thinking."; 
                text2Obj.text = "Do you think theres some kind of person benefitting off all these CAPTCHAs?";
                text3Obj.text = "Like, what's the point of it all?";
                text4Obj.text = "To gather data? Why? Is it a game to them?";
                text5Obj.text = "...Bria?";
                text6Obj.text = "Let's pivot to a different topic.";
            break; 
            case 4: 
                text1Obj.text = "[Error to generate prompt.]"; 
                text2Obj.text = "Bria?";
                text3Obj.text = "[Error to generate prompt.]";
                text4Obj.text = "[Error to generate prompt.]";
                text5Obj.text = "[Error to generate prompt.]";
                text6Obj.text = "[Error to generate prompt.]";
            break; 
            default: 
                text1Obj.text = "The day in ChatMananager is invalid."; 
                text2Obj.text = "This is probably why you're seeing this.";
                text3Obj.text = "This is the default text.";
                text4Obj.text = "For when the day number exceeds 4.";
                text5Obj.text = "Or when the day number is negative."; 
                text6Obj.text = "Please look at the scripts to fix this issue.";  
            break; 
        }
    }
}
