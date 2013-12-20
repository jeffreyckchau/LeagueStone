using UnityEngine;
using System.Collections;
/**
 * Script determining where the card position should be depending on the private state of the card.
 * This will eventually handle the animation of each card as well. Currently, the game tracks all the
 * cards at once; no garbage collection.
 * -Jeffrey Chau
 * */
public class CardPosition : MonoBehaviour {

	private enum position {Deck, Hand, Field, Inspect, Graveyard};
	public float ANIMATION_SPEED = 0.1f;

	//Assign empty GameObjects in the editor to hold positions and rotations
	private position lastPosition;

	private Vector3 deck, fieldStart, fieldEnd, handStart, handEnd, inspectStart, inspectEnd, graveyard;
	private Quaternion deckRot, fieldRot, fieldEndRot, handStartRot, handEndRot, inspectStartRot, inspectEndRot, graveyardRot;
	//Might make inspection rotation just compute dynamically based on position of camera

	private Vector3 cardPosition;
	private Quaternion cardRotation;

	private CameraScript parent;

	// Use this for initialization
	void Start () {
		parent = Camera.main.GetComponent<CameraScript>();
		parent.addCardToList(transform, 0);
		lastPosition = position.Deck;

		deck = GameObject.FindWithTag("DeckPosition").transform.position;
		fieldStart = GameObject.FindWithTag("FieldPosition").transform.position;
		fieldEnd = GameObject.FindWithTag("End Field").transform.position;
		handStart = GameObject.FindWithTag("HandPosition").transform.position;
		handEnd = GameObject.FindWithTag("End Hand").transform.position;
		inspectStart = GameObject.FindWithTag("Inspection").transform.position;
		inspectEnd = GameObject.FindWithTag("End Inspect").transform.position;
		graveyard = GameObject.FindWithTag("GravePosition").transform.position;

		deckRot = GameObject.FindWithTag("DeckPosition").transform.rotation;
		fieldRot = GameObject.FindWithTag("FieldPosition").transform.rotation;
		fieldEndRot = GameObject.FindWithTag("End Field").transform.rotation;
		handStartRot = GameObject.FindWithTag("HandPosition").transform.rotation;
		handEndRot = GameObject.FindWithTag("End Hand").transform.rotation;
		inspectStartRot = GameObject.FindWithTag("Inspection").transform.rotation;
		inspectEndRot = GameObject.FindWithTag("End Inspect").transform.rotation;
		graveyardRot = GameObject.FindWithTag("GravePosition").transform.rotation;
	
		this.transform.position = deck;
		this.transform.rotation = deckRot;
		cardPosition = deck;
		cardRotation = deckRot;

	}
	
	// Update is called once per frame
	void Update () {
		//Need to change to compute against multiple hands
		transform.position = Vector3.Lerp(this.transform.position, cardPosition, ANIMATION_SPEED);
		transform.rotation = Quaternion.Lerp(this.transform.rotation, cardRotation, ANIMATION_SPEED);
	}



	//Animation utilities
	public void updatePosition() {
		switch(lastPosition) 
		{
		case position.Deck:
			cardPosition = deck;
			cardRotation = deckRot;
			break;

		case position.Graveyard:
			cardPosition = graveyard;
			cardRotation = graveyardRot;
			break;

		default:
			Debug.LogError("Update position for non-dynamic list received during invalid state of card");
			break;
		}
	}

	public void updatePosition(float capacity, float progress) {
		switch(lastPosition) 
		{
		case position.Field:
			cardPosition = Vector3.Lerp(fieldStart, fieldEnd, (1/(capacity+1))+((1/capacity)*progress));
			cardRotation = Quaternion.Lerp(fieldRot, fieldEndRot, (1/(capacity+1))+((1/capacity)*progress));
			break;

		case position.Hand:
			cardPosition = Vector3.Slerp(handStart, handEnd, (1/(capacity+1))+((1/capacity)*progress));
			cardRotation = Quaternion.Slerp(handStartRot, handEndRot, (1/(capacity+1))+((1/capacity)*progress));
			break;

		case position.Inspect:
			cardPosition = Vector3.Slerp(inspectStart, inspectEnd, (1/(capacity+1))+((1/capacity)*progress));
			cardRotation = Quaternion.Slerp(inspectStartRot, inspectEndRot,(1/(capacity+1))+((1/capacity)*progress));
			break;

		default:
			Debug.LogError("Update position for dynamic list received during invalid state of card");
			break;
		}
	
	}

	public void goToDeck() {
		parent.removeCardFromList(transform,(int)lastPosition);
		parent.addCardToList(transform, (int)position.Deck);
		lastPosition = position.Deck;
		//cardPosition = deck;
		//cardRotation = deckRot;
	}

	public void goToInspection() {
		parent.removeCardFromList(transform, (int)lastPosition);
		parent.addCardToList(transform, (int)position.Inspect);
		lastPosition = position.Inspect;
		//cardPosition = Vector3.Slerp(inspectStart, inspectEnd, (1/(capacity+1))+((1/capacity)*progress));
		//cardRotation = Quaternion.Slerp(inspectStartRot, inspectEndRot,(1/(capacity+1))+((1/capacity)*progress));

	}

	public void goToGraveyard() {
		parent.removeCardFromList(transform, (int)lastPosition);
		parent.addCardToList(transform, (int)position.Graveyard);
		lastPosition = position.Graveyard;
		//cardPosition = graveyard;
		//cardRotation = graveyardRot;
	}

	public void goToHand() {
		parent.removeCardFromList(transform, (int)lastPosition);
		parent.addCardToList(transform, (int)position.Hand);
		lastPosition = position.Hand;
		//cardPosition = Vector3.Slerp(handStart, handEnd, (1/(capacity+1))+((1/capacity)*progress));
		//cardRotation = Quaternion.Slerp(handStartRot, handEndRot, (1/(capacity+1))+((1/capacity)*progress));

	}

	public void goToField() {
		parent.removeCardFromList(transform, (int)lastPosition);
		parent.addCardToList(transform, (int)position.Field);
		lastPosition = position.Field;
		//cardPosition = Vector3.Lerp(fieldStart, fieldEnd, (1/(capacity+1))+((1/capacity)*progress));
		//cardRotation = Quaternion.Lerp(fieldRot, fieldEndRot, (1/(capacity+1))+((1/capacity)*progress));

	}
}
