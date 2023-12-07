using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{

    [SerializeField] GameObject loginOverlay;
    [SerializeField] GameObject loginPage;
    [SerializeField] GameObject signupPage;
    [SerializeField] GameObject centerPage;
    [SerializeField] GameObject playerPage;
    [SerializeField] GameObject eventPage;
    [SerializeField] RectTransform anchor;
    [SerializeField] GameObject codeInputField;
    [SerializeField] GameObject eventPanel;
    [SerializeField] GameObject eventList;
    [SerializeField] GameObject userList;
    [SerializeField] TMP_Text headerText;
    [SerializeField] TMP_Text fakePlayerText;

    [SerializeField] float swipeInterpolation;
    Vector3 targetPosition;

    private Vector2 fingerDown;
    private Vector2 fingerUp;
    public bool detectSwipeOnlyAfterRelease = false;

    [SerializeField] bool hasSwiped = false;

    [SerializeField] bool loggedIn = false;

    int currentPage = 1;

    public float SWIPE_THRESHOLD = 20f;

    // Update is called once per frame
    void Update()
    {

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
                hasSwiped = false;
            }

            //Detects Swipe while finger is still moving
            if (touch.phase == TouchPhase.Moved)
            {
                if (!detectSwipeOnlyAfterRelease)
                {
                    fingerDown = touch.position;
                    checkSwipe();
                }
            }

            //Detects swipe after finger is released
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                checkSwipe();
            }
        }

        anchor.anchoredPosition = Vector3.Lerp(anchor.anchoredPosition, targetPosition, Time.deltaTime * swipeInterpolation);

    }

    void checkSwipe()
    {
        if (!hasSwiped)
        {
            //Check if Vertical swipe
            if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
            {
                //Debug.Log("Vertical");
                if (fingerDown.y - fingerUp.y > 0)//up swipe
                {

                }
                else if (fingerDown.y - fingerUp.y < 0)//Down swipe
                {

                }
                fingerUp = fingerDown;
            }

            //Check if Horizontal swipe
            else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
            {
                //Debug.Log("Horizontal");
                if (fingerDown.x - fingerUp.x > 0)//Right swipe
                {
                    OnSwipeRight();
                    hasSwiped = true;
                }
                else if (fingerDown.x - fingerUp.x < 0)//Left swipe
                {
                    OnSwipeLeft();
                    hasSwiped = true;
                }
                fingerUp = fingerDown;
            }

            //No Movement at-all
            else
            {
                //Debug.Log("No Swipe!");
            }
        }
    }

    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }



    void OnSwipeRight()
    {
        if (loggedIn)
        {
            //Debug.Log("Swipe Left");

            switch(currentPage)
            {
                case 0:
                    ScrollToCenterPage(); break;
                case 1:
                    ScrollToPlayerPage(); break;
            }
        }
    }

    void OnSwipeLeft()
    {
        if (loggedIn)
        {
            //Debug.Log("Swipe Right");

            switch (currentPage)
            {
                case 1:
                    ScrollToEventPage(); break;
                case 2:
                    ScrollToCenterPage(); break;
            }
        }
    }


    

    public void ScrollToPlayerPage()
    {
        currentPage = 2;

        targetPosition = new Vector3(800, 0, 0);

        //anchor. = new Vector3(Screen.width, 0, 0);
    }

    public void ScrollToCenterPage()
    {
        currentPage = 1;

        targetPosition = new Vector3(0, 0, 0);
    }

    public void ScrollToEventPage()
    {
        currentPage = 0;

        targetPosition = new Vector3(-800, 0, 0);
    }

    public void ScrollToFakePlayer()
    {
        currentPage = 3;

        anchor.anchoredPosition = new Vector3(-1600, 0, 0);
        targetPosition = new Vector3(-1600, 0, 0);
    }

    public void ReturnToCenter()
    {
        currentPage = 1;

        anchor.anchoredPosition = new Vector3(0, 0, 0);
        targetPosition = new Vector3(0, 0, 0);
    }

    public void SetFakePlayerName(string text)
    {
        fakePlayerText.text = text;
    }

    public void GoToSignup()
    {
        loginPage.SetActive(false);
        signupPage.SetActive(true);
    }

    public void GoToLogin()
    {
        loginPage.SetActive(true);
        signupPage.SetActive(false);
    }

    public void CloseLogin()
    {
        loginOverlay.SetActive(false);
        loggedIn = true;
    }

    public void ShowCodeInput(bool yes)
    {
        codeInputField.SetActive(yes);
    }

    public void ShowEvent()
    {
        eventPanel.SetActive(true);
    }

    public void HideEvent()
    {
        eventPanel.SetActive(false);
    }

    public void SwitchToUserList()
    {
        userList.SetActive(true);
        eventList.SetActive(false);
    }

    public void SwitchToEventList()
    {
        userList.SetActive(false);
        eventList.SetActive(true);
    }

    public void ChangeText(string text)
    {
        headerText.text = text;
    }
}
