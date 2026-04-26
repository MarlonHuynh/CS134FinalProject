/*

Purpose: used to manage the ending cutscenes

*/

using UnityEngine;
using UnityEngine.UI; 
using TMPro; 
using System.Collections;
using System.Reflection;

public class EndingCutsceneManager : MonoBehaviour
{
    [Header("Refs")]
    public PlayerMovement playerMovement; 
    public GameObject endingBG; 
    public Image endingBGImage;
    public GameObject bodyTextObj; 
    public GameObject headingObj;  
    public TMP_Text bodyText; 
    public TMP_Text headingText; 
    [Header("Audio Refs")]
    public AudioClip openDoorClip; 
    public AudioSource flex2DAudioSource; 
    public AudioSource flex2DAudioSource_looping; 
    public AudioSource mainRoomMusicSource;  

    void Start()
    {
        Color color = endingBGImage.color;
        color.a = 0;
        endingBGImage.color = color;
        bodyTextObj.SetActive(false); 
        headingObj.SetActive(false); 

    }

    // Plays an ending cutscene based on input
    public void playEnding(int num)
    {
        endingBG.SetActive(true); 
        StartCoroutine(endingEnum(num)); 
    }

    IEnumerator endingEnum(int endingNum)
    {
        playerMovement.disableMovement(); 
        endingBG.SetActive(true); 

        float time = 0f;
        Color color = endingBGImage.color;

        while (time < 3f)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, time / 3f);

            color.a = alpha;
            endingBGImage.color = color;

            yield return null;
        }
 
        // Audio 
        flex2DAudioSource_looping.Stop(); 
        mainRoomMusicSource.Stop(); 
        if (endingEnum == 1){
            flex2DAudioSource.clip = openDoorClip; 
            flex2DAudioSource.Play(); 
        }
        
        yield return new WaitForSeconds(3f);

        bodyTextObj.SetActive(true); 
        headingObj.SetActive(true); 
        if (endingNum == 1)
        { 
            bodyText.text = "They seized you instantly. Your captors—your so-called protectors—covered your eyes, plunging you into darkness. They took you somewhere, you don't know where. The air filled with the whir of machinery, and a thick sharp metallic scent burned your nose. Before you could react, they threw you forward. The machine roared, and in seconds, your body was reduced to nothing but pulp, ready to be consumed. You paid the ultimate price for refusing to play their game."; 
            headingText.text = "Ending 1: Meatgrinder"; 
        }
        else if (endingNum == 2)
        {
            bodyText.text = "The eye in the food. The cryptic message in the trash. It all pointed to something—someone—using you as training data. And when that data was bad? You become meat. Realizing you were trapped in an inevitable dilemma—serve your captors or be consumed by the other prisoners—you began to plan. You could hear the wind, faint but real, which meant your block was close to the outside. There had to be a way out. A loose panel in the meditation room reveals a narrow maintenence passage. You step into the darkness, leaving the only source of comfort you know behind."; 
            headingText.text = "Ending 2: Escape"; 
        }
    }
}
