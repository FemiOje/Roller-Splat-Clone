using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    //private Vector2 swipePosLastFrame;
    //private Vector2 swipePosCurrentFrame;
    //private Vector2 currentSwipe;
    private bool isSwiping = false;
    [SerializeField] private bool isTraveling;
    public Color solveColor;

    private float minSwipeDistance = 50f; // Minimum distance for a swipe to register


    Rigidbody ballRb;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        ballRb = GetComponent<Rigidbody>();
        solveColor = Random.ColorHSV(0.5f, 1f);
        GetComponent<Renderer>().material.color = solveColor;
    }

    private void Update()
    {
        //if (!isTraveling)
        //{
            // Check for touch or mouse input when ball is not travelling
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

        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        string checkTag = collision.gameObject.tag;
        Debug.Log($"Collision detected: {checkTag}");
        if (checkTag == "Wall" || checkTag == "Obstacle")
        {
            //isTraveling = false;
           // ballRb.velocity = Vector3.zero;
        }
    }


    //swipe detection
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
                    MoveBall(Vector3.right);
                }
                else
                {
                    Debug.Log("Left Swipe");
                    MoveBall(Vector3.left);
                }
            }
            else
            {
                // Vertical Swipe
                if (swipeDirection.y > 0)
                {
                    Debug.Log("Up Swipe");
                    MoveBall(Vector3.forward);
                }
                else
                {
                    Debug.Log("Down Swipe");
                    MoveBall(Vector3.back);
                }
            }
        }
    }

    void MoveBall(Vector3 direction)
    {
        //isTraveling = true;
        ballRb.velocity = speed * direction;
    }
}
