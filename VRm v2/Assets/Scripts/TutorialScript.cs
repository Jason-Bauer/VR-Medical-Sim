using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour {

    public List<Sprite> tutorialCards;

    [Header("Game Objects")]
    //sprites of the various tutorial cards, add more public sprites as more cards are made
    public Sprite controlsCard;
    public Sprite teleportationCard;
    public Sprite drawCardWorld;
    public Sprite interactableCard;
    public Sprite armInteractionCard;
    public Sprite armSphereCard;
    public Sprite armSelectionCard;
    public Sprite whiteboardDrawCard;

    [Header("Tutorial Board")]
    //GameObject for the tutorial board itself
    public GameObject tutorialBoard;

    [Header("Card Tracker - 0 = 1")]
    //int used to track what card is currently being looked at.
    int cardTracker;

    [Header("Page Tracker Text")]
    //the "1/X" text below the board
    public TextMesh pageTracker;

	// Use this for initialization
	void Start () {
        tutorialCards = new List<Sprite>(); //initalize the list

        //add the cards
        tutorialCards.Add(controlsCard); 
        tutorialCards.Add(teleportationCard);
        tutorialCards.Add(drawCardWorld);
        tutorialCards.Add(interactableCard);
        tutorialCards.Add(armInteractionCard);
        tutorialCards.Add(armSphereCard);
        tutorialCards.Add(armSelectionCard);
        tutorialCards.Add(whiteboardDrawCard);

        //set the cardTracker to 0 as we are at the first entry in the list
        cardTracker = 0;

        //set the first card as the current one
        tutorialBoard.GetComponent<SpriteRenderer>().sprite = tutorialCards[cardTracker];
        updateTrackerText(); //update the text to say which card we are on
    }
	
	// Update is called once per frame
	void Update () {

	}

    /*
     * Functions for swapping between the tutorial cards.
     */
     //fired when the "Next" button is pressed in
    public void NextCard()
    {
        if(cardTracker < tutorialCards.Count - 1)
        {
            cardTracker += 1;
        }
        else
        {
            cardTracker = 0;
        }

        updateTrackerText();
        updateCard();
    }
    //fired when the "Previous" button is pressed in
    public void PrevCard()
    {
        if (cardTracker > 0)
        {
            cardTracker -= 1;
        }
        else
        {
            cardTracker = tutorialCards.Count - 1;
        }

        updateTrackerText();
        updateCard();
    }
    //the method that actually swaps the cards
    void updateCard()
    {
        tutorialBoard.GetComponent<SpriteRenderer>().sprite = tutorialCards[cardTracker];
    }
    //updates the text at the bottom
    void updateTrackerText()
    {
        pageTracker.text = "" + (cardTracker + 1) + "/" + (tutorialCards.Count);
    }
}
