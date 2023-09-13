using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private bool isSwiping = false;

    [SerializeField]
    private float minSwipeDistance = 50f; // Minimum distance for a swipe to register

    void Update()
    {
        // Check for touch or mouse input
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
            isSwiping = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            touchEndPos = Input.mousePosition;
            isSwiping = false;

            DetectSwipe();
        }

        // Check for touch drag (optional)
        if (isSwiping)
        {
            // You can add additional code here for handling drag while swiping.
        }
    }

    void DetectSwipe()
    {
        float swipeDistance = Vector2.Distance(touchStartPos, touchEndPos);

        if (swipeDistance > minSwipeDistance)
        {
            // Calculate the direction of the swipe
            Vector2 swipeDirection = touchEndPos - touchStartPos;

            // Check the direction of the swipe
            if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
            {
                // Horizontal Swipe
                if (swipeDirection.x > 0)
                {
                    Debug.Log("Right Swipe");
                    // Add your right swipe logic here.
                }
                else
                {
                    Debug.Log("Left Swipe");
                    // Add your left swipe logic here.
                }
            }
            else
            {
                // Vertical Swipe
                if (swipeDirection.y > 0)
                {
                    Debug.Log("Up Swipe");
                    // Add your up swipe logic here.
                }
                else
                {
                    Debug.Log("Down Swipe");
                    // Add your down swipe logic here.
                }
            }
        }
    }
}

