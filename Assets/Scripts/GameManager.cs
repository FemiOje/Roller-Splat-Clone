using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager singleton;
    private GroundUnitController[] allGroundUnits;

    private void Start()
    {
        SetupNewScene();
    }

    private void Update()
    {
        
    }
    private void SetupNewScene()
    {
        allGroundUnits = FindObjectsOfType<GroundUnitController>();
    }
}
