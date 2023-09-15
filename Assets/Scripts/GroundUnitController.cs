using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundUnitController : MonoBehaviour
{
    BallController ballControllerScript;
    public bool hasMadeContact = false;

    private void Start()
    {
        ballControllerScript = GameObject.Find("Ball").GetComponent<BallController>();
    }
    public void SetColor(Color newColor)
    {
        GetComponent<MeshRenderer>().material.color = newColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasMadeContact = true;
            SetColor(ballControllerScript.solveColor);
            GameManager.singleton.CheckIfComplete();
        }
    }
}