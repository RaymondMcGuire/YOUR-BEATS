using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class SetDefaultSelectedButton : MonoBehaviour 
{
	public Button firstButton;

	void OnEnable()
	{
		EventSystem eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		eventSystem.SetSelectedGameObject(firstButton.gameObject, new BaseEventData(eventSystem));
	}
}

