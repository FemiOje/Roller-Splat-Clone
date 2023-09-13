using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundUnitController : MonoBehaviour
{

    public bool hasMadeContact = false;
    public void SetColor(Color newColor)
    {
        GetComponent<MeshRenderer>().material.color = newColor;
    }
}
