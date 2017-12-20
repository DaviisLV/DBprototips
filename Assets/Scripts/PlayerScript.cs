using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public generator generator;
    private Vector3 previousPlayerPosition;

    // Use this for initialization
    void Start () {
             previousPlayerPosition = generator.GetGridPosition(transform.position);
        }
	
	// Update is called once per frame
	void Update () {
      
            Vector3 playerGridPosition = generator.GetGridPosition(transform.position);
            if (!playerGridPosition.Equals(previousPlayerPosition))
            {
                generator.UpdateGrid(transform.position);
                previousPlayerPosition = playerGridPosition;
            }
        }
    }

