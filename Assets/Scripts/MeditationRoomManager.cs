using UnityEngine;
using System.Collections;

public class MeditationRoomManager : MonoBehaviour
{
    public OpenMeditationRoom openMeditationRoom; 
    public Material floorMat; 
    public Material wallMat; 
    public Material roofMat;
    public AudioSource meditationMusicSource; 

    public Sprite beachFloor, beachWall, beachRoof; 
    public AudioClip beachMusic; 
    public Sprite grassyFloor, grassyWall, grassyRoof; 
    public AudioClip grassyMusic; 

    void Start()
    {
        switchToBeach(); 
    }
    
    void Update(){
        if (Input.GetKeyUp(KeyCode.O)){
            switchToBeach(); 
        }
        if (Input.GetKeyUp(KeyCode.P)){
            switchToGrassyField(); 
        }
    }

    void switchToBeach(){
        floorMat.SetTexture("_BaseMap", beachFloor.texture);
        wallMat.SetTexture("_BaseMap", beachWall.texture);
        roofMat.SetTexture("_BaseMap", beachRoof.texture);
        meditationMusicSource.clip = beachMusic; 
        meditationMusicSource.Play(); 
    }

    void switchToGrassyField(){
        floorMat.SetTexture("_BaseMap", grassyFloor.texture);
        wallMat.SetTexture("_BaseMap", grassyWall.texture);
        roofMat.SetTexture("_BaseMap", grassyRoof.texture);
        meditationMusicSource.clip = grassyMusic; 
        meditationMusicSource.Play(); 
    } 
}
