using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class BaseMenu {

    private CanvasPanel canvasPanel;
    private EventTrigger eventTrigger;

    public BaseMenu()
    {
        eventTrigger = null;
        canvasPanel = null;
    }

    /// <summary>
    /// Handler for clicks in this menu
    /// </summary>
    /// <param name="obj"></param>
    abstract protected void HandleMenuClicks(GameObject obj);


    /// <summary>
    /// Used to set the active status of a menu, through the canvas
    /// </summary>
    /// <param name="value">true to show, false to hide</param>
    public void SetActive(bool value)
    {
        canvasPanel.ShowCanvas(value);
    }


    /// <summary>
    /// Used for instantiating and offsetting an Object.
    /// </summary>
    /// <param name="Obj">GameObject to be cloned</param>
    /// <param name="Name">Name/Display text</param>
    /// <param name="vec">Reference to offset vector</param>
    /// <param name="Parent">The parent of the created GameObject</param>
    /// <returns>The created GameObject</returns>
    private GameObject InstantiateObject(GameObject ObjToInstantiate, string Name, GameObject Parent, bool clickable = false)
    {
        GameObject obj = Object.Instantiate(ObjToInstantiate) as GameObject;
        obj.name = Name;
        obj.transform.SetParent(Parent.transform);

        //if (clickable)
        //{
            // TODO: Deal with items I want to click on that are not buttons...something like:
            // if (Input.GetMouseDown){ do stuff}...has to be an event trigger to check Input.GetMouseDown on mouseover
        //}

        return obj;
    }



    /// <summary>
    /// Creates a Button prefab then sets the positioning and text for the button
    /// </summary>
    /// <param name="ObjToInstantiate">Prefab that is being instantiated</param>
    /// <param name="DisplayText">Text to display on the button</param>
    /// <param name="Parent">Parent to attach this to</param>
    /// <param name="Position">Position to place the button</param>
    /// <param name="Offset">Offset to set after the button has been set</param>
    protected void InstantiateButton(GameObject ObjToInstantiate, string DisplayText, GameObject Parent, ref Vector3 Position, Vector3 Offset = new Vector3())
    {
        Position += Offset;
        GameObject obj = Object.Instantiate(ObjToInstantiate) as GameObject;
        obj.name = DisplayText;
        obj.transform.SetParent(Parent.transform);
        SetupButton(obj, Position, DisplayText);
    }


    // https://answers.unity.com/questions/781726/how-do-i-add-a-listener-to-onpointerenter-ugui.html
    /// <summary>
    /// Custom function to add event triggers to an object.
    /// </summary>
    /// <param name="action">The action to perform</param>
    /// <param name="triggerType">The trigger type</param>
    protected void AddEventTrigger(UnityAction action, EventTriggerType triggerType)
    {
        // Create a new TriggerEvent and add a listener
        EventTrigger.TriggerEvent trigger = new EventTrigger.TriggerEvent();
        trigger.AddListener((eventData) => action()); // you can capture and pass the event data to the listener

        // Create and initialise EventTrigger.Entry using the created TriggerEvent
        EventTrigger.Entry entry = new EventTrigger.Entry() { callback = trigger, eventID = triggerType };

        // Add the EventTrigger.Entry to delegates list on the EventTrigger
        eventTrigger.triggers.Add(entry);
    }


    protected void SetupButton(GameObject obj, Vector3 Location, string DisplayText)
    {
        Button btn = obj.GetComponent<Button>() as Button;
        // A little check never hurt anyone
        if (btn == null)
        {
#if UNITY_EDITOR
            Debug.LogError("SetupButton did not receive an object with a button");
#endif
            return;
        }

        obj.GetComponentInChildren<Text>().text = DisplayText;
        obj.transform.position = Location;
        btn.onClick.AddListener(delegate { HandleMenuClicks(obj); });        
    }
}
