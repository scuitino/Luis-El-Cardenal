﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Drag and Drop item.
/// </summary>
public class DragAndDropItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public static bool dragDisabled = false;										// Drag start global disable

	public static DragAndDropItem draggedItem;                                      // Item that is dragged now
	public static GameObject icon;                                                  // Icon of dragged item
    public static GameObject children;                                              // children of dragged item
    public static DragAndDropCell sourceCell;                                       // From this cell dragged item is

	public delegate void DragEvent(DragAndDropItem item);
	public static event DragEvent OnItemDragStartEvent;                             // Drag start event
	public static event DragEvent OnItemDragEndEvent;                               // Drag end event

	private static Canvas canvas;                                                   // Canvas for item drag operation
	private static string canvasName = "DragAndDropCanvas";                   		// Name of canvas
	private static int canvasSortOrder = 100;										// Sort order for canvas

    // drag and drop sound clips
    [SerializeField]
    AudioClip _dragSound, _dropSound;

    // to play d&d sounds
    AudioSource _aSource;

    [SerializeField]
    int _itemID;                                                                    // to check results on activities    

    public static bool _isGrabbing;                                                 // to know if the player is grabbing something

    /// <summary>
    /// Awake this instance.
    /// </summary>
    void Awake()
	{
		if (canvas == null)
		{
			GameObject canvasObj = new GameObject(canvasName);
			canvas = canvasObj.AddComponent<Canvas>();
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			canvas.sortingOrder = canvasSortOrder;
		}
	}

    private void Start()
    {
        _aSource = CDNDAudiosSource._instance.GetComponent<AudioSource>();
    }

    /// <summary>
    /// This item started to drag.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
	{
        if (dragDisabled == false && !_isGrabbing)
        {
            _isGrabbing = true;
            // play sound
            _aSource.clip = _dragSound;
            _aSource.Play();

            sourceCell = GetCell();                                                 // Remember source cell
            draggedItem = this;                                                     // Set as dragged item
                                                                                    // Create item's icon
            icon = new GameObject();
            icon.transform.SetParent(canvas.transform);
            icon.name = "Icon";
            Image myImage = GetComponent<Image>();
            myImage.raycastTarget = false;                                          // Disable icon's raycast for correct drop handling
            Image iconImage = icon.AddComponent<Image>();
            iconImage.raycastTarget = false;
            iconImage.sprite = myImage.sprite;
            RectTransform iconRect = icon.GetComponent<RectTransform>();

            // creating children
            children = new GameObject();
            children.transform.SetParent(icon.transform);            
            Image thisChildImage = transform.GetChild(0).GetComponent<Image>();
            thisChildImage.raycastTarget = false;
            Image newChildrenImage = children.AddComponent<Image>();
            newChildrenImage.raycastTarget = false;
            newChildrenImage.sprite = thisChildImage.sprite;
            RectTransform newChildRect = newChildrenImage.GetComponent<RectTransform>();            

            // Set icon's dimensions
            RectTransform myRect = GetComponent<RectTransform>();
            iconRect.pivot = new Vector2(0.5f, 0.5f);
            iconRect.anchorMin = new Vector2(0.5f, 0.5f);
            iconRect.anchorMax = new Vector2(0.5f, 0.5f);
            iconRect.sizeDelta = new Vector2(myRect.rect.width, myRect.rect.height);

            // set new child dimensions
            newChildRect.pivot = new Vector2(0.5f, 0.5f);
            newChildRect.anchorMin = new Vector2(0.5f, 0.5f);
            newChildRect.anchorMax = new Vector2(0.5f, 0.5f);
            newChildRect.sizeDelta = new Vector2(myRect.rect.width, myRect.rect.height);

            if (OnItemDragStartEvent != null)
            {
                OnItemDragStartEvent(this);                                         // Notify all items about drag start for raycast disabling
            }
        }
	}

	/// <summary>
	/// Every frame on this item drag.
	/// </summary>
	/// <param name="data"></param>
	public void OnDrag(PointerEventData data)
	{
		if (icon != null)
		{
			icon.transform.position = Input.mousePosition;                          // Item's icon follows to cursor in screen pixels
		}
	}

	/// <summary>
	/// This item is dropped.
	/// </summary>
	/// <param name="eventData"></param>
	public void OnEndDrag(PointerEventData eventData)
	{
        // play sound
        _aSource.clip = _dropSound;
        _aSource.Play();
        ResetConditions();
	}

	/// <summary>
	/// Resets all temporary conditions.
	/// </summary>
	private void ResetConditions()
	{
		if (icon != null)
		{
			Destroy(icon);                                                          // Destroy icon on item drop
		}
		if (OnItemDragEndEvent != null)
		{
			OnItemDragEndEvent(this);                                       		// Notify all cells about item drag end
		}
		draggedItem = null;
		icon = null;
		sourceCell = null;

        _isGrabbing = false;
    }

	/// <summary>
	/// Enable item's raycast.
	/// </summary>
	/// <param name="condition"> true - enable, false - disable </param>
	public void MakeRaycast(bool condition)
	{
		Image image = GetComponent<Image>();
		if (image != null)
		{
			image.raycastTarget = condition;
		}
	}

	/// <summary>
	/// Gets DaD cell which contains this item.
	/// </summary>
	/// <returns>The cell.</returns>
	public DragAndDropCell GetCell()
	{
		return GetComponentInParent<DragAndDropCell>();
	}

	/// <summary>
	/// Raises the disable event.
	/// </summary>
	void OnDisable()
	{
		ResetConditions();
	}

    /// <summary>
	/// ID that identifies the item category
	/// </summary>
	public int GetItemID()
    {
        return _itemID;
    }
}
