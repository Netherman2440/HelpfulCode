using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
[RequireComponent(typeof(ScrollRect))]
public class AutoScrollRect : MonoBehaviour
{
    public delegate void Selected(GameObject button);
    public static event Selected OnSelection;
    
    [SerializeField]
    private RectTransform centerPin;
    [SerializeField]
    private float snapTime = 0.3f;

    private RectTransform scrollRectTransform;
    private RectTransform contentPanel;
    private RectTransform selectedRectTransform;  
    
    private GameObject lastSelected;  
    private GameObject selected;

    private void Start()
    {
        scrollRectTransform = GetComponent<RectTransform>();
        contentPanel = GetComponent<ScrollRect>().content;
        lastSelected = null;
        
    }

    private void Update()
    {
        
        selected = EventSystem.current.currentSelectedGameObject;

        if (selected == null)
            return;
        if (selected.transform.parent != contentPanel.transform)
            return;
        if (selected == lastSelected)
            return;

        selectedRectTransform = selected.GetComponent<RectTransform>();
        
        
        float selectedPositionX = Mathf.Abs(selectedRectTransform.anchoredPosition.x);

        float distance = Mathf.Abs(centerPin.anchoredPosition.x - selectedPositionX);

        if (centerPin.anchoredPosition.x - selectedPositionX > 0)
        {
            //using DOTween
            contentPanel.DOAnchorPosX(centerPin.anchoredPosition.x + distance, snapTime, false);

            //contentPanel.anchoredPosition = new Vector2(centerPin.anchoredPosition.x + distance, contentPanel.anchoredPosition.y);
        }

        else
        {
            contentPanel.DOAnchorPosX(centerPin.anchoredPosition.x - distance, snapTime, false);
            
            //contentPanel.anchoredPosition = new Vector2(centerPin.anchoredPosition.x - distance, contentPanel.anchoredPosition.y);
        }
            
        
        lastSelected = selected;
        OnSelection?.Invoke(selected);
    }

    
}
