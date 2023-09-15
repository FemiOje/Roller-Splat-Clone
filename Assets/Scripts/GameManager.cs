using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    private GroundUnitController[] allGroundUnits;


    private void Awake()
    {
        // Check if an instance of GameManager already exists
        if (singleton == null)
        {
            // If not, set this instance as the GameManager and don't destroy it on scene changes
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this GameObject
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        //set up new level for scenes other than main menu
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            SetupNewLevel();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //cache the ground tiles, if we're not in the main menu
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            SetupNewLevel();
        }
    }
    private void SetupNewLevel()
    {
        allGroundUnits = FindObjectsOfType<GroundUnitController>();
        Time.timeScale = 1;
    }

    public void CheckIfComplete()
    {
        bool isComplete = true;

        for (int i = 0; i < allGroundUnits.Length; i++)
        {
            if (allGroundUnits[i].hasMadeContact == false)
            {
                isComplete = false;
                break;
            }
        }

        if (isComplete)
        {
            GoToNextLevel();
        }
    }

    private void GoToNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            //handle completion particle system
            Debug.Log("You have reached the end of the game");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
