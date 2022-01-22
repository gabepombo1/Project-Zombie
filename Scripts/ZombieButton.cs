using System;
using UnityEngine;
using UnityEngine.UI;

public class ZombieButton : MonoBehaviour
{
    //private SpriteRenderer buttonSpriteRenderer;
    
    [SerializeField] public Sprite buttonDownSprite;
    
    //this is the sprite that will be on when the button isnt toggled
    [SerializeField] public Sprite buttonUpSprite;

    [SerializeField] public Toggle thisToggle = GameObject.FindWithTag("ZombieButton").GetComponent<Toggle>();
    
    //this is the camera since the camera will have PlayerController attached to it;
    [SerializeField] public GameObject player = GameObject.FindWithTag("Player");

    private void Update()
    {
        
        //Toggle buttonToggle = thisButton.GetComponent<Toggle>();

        PlayerController playerController = player.GetComponent<PlayerController>();
        
        SpriteRenderer buttonSpriteRenderer = thisToggle.GetComponent<SpriteRenderer>();
        
        //Sprite buttonSprite = buttonSpriteRenderer.sprite;
        
        if (thisToggle.isOn)
        {

            buttonSpriteRenderer.sprite = buttonDownSprite;
            
            //changes state to ZOMBIE so that the mouse can spawn zombies on civilians
            playerController.pressState = PlayerController.PressState.ZOMBIE;

        }
        
        else
        {
            
            buttonSpriteRenderer.sprite = buttonUpSprite;

            playerController.pressState = PlayerController.PressState.NONE;

        }
        
        //buttonToggle.isOn ? buttonSpriteRenderer.sprite = buttonDownSprite : buttonSpriteRenderer.sprite = buttonUpSprite;

    }

    //I think making a new scene just for the main menu would be a good idea
    
}
