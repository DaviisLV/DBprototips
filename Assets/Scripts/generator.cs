using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class generator : MonoBehaviour {

    public GameObject GroundPanel;
    public Transform Player;
    private int radiusAroundPlayer = 7;
    private float panelSizeX;
    private float panelSizeZ;
    private List<Vector3>  Grid = new List<Vector3>();
    private Vector3 startPosition;

    public List<GameObject> ActiveGameObjects = new List<GameObject>();
    private GameObject plane;

    private void Awake()

    {   //get start position
        startPosition = Player.transform.position;  
        //get panel size
        panelSizeX = GroundPanel.GetComponent<Renderer>().bounds.size.x;
        panelSizeZ = GroundPanel.GetComponent<Renderer>().bounds.size.z;
        //ganerate platform around player on start
        for (int z = -radiusAroundPlayer; z <= radiusAroundPlayer; z++)
        {
            for (int x = -radiusAroundPlayer; x <= radiusAroundPlayer; x++)
            {
               // if (x * x + z * z < radiusAroundPlayer * radiusAroundPlayer)
              //  {
                    plane = Instantiate(GroundPanel);         
                    plane.transform.position = new Vector3(startPosition.x + x * panelSizeX, 0, startPosition.z + z * panelSizeZ);
                                      
                    Grid.Add(new Vector3(startPosition.x + x * panelSizeX, 0, startPosition.z + z * panelSizeZ));
                    ActiveGameObjects.Add(plane);
              //  }
            }
        }    
    }

    private List<Vector3> GetPlayerRadiusCoordiates(Vector3 gridPosition)
    {
        List<Vector3> newCoordinates = new List<Vector3>();

        for (int z = -radiusAroundPlayer; z <= radiusAroundPlayer; z++)
        {
            for (int x = -radiusAroundPlayer; x <= radiusAroundPlayer; x++)
            {
             //   if (x * x + z * z < radiusAroundPlayer * radiusAroundPlayer)
              //  {
                   Vector3 vector = new Vector3(startPosition.x + (gridPosition.x * panelSizeX) + (x * panelSizeX), 0, startPosition.z + (gridPosition.z * panelSizeZ) + (z * panelSizeZ));
                    newCoordinates.Add(vector);           
              //  }
            }
        }      
        return newCoordinates;
    }
    public void add(Vector3 poz)
    {
        plane = Instantiate(GroundPanel);
        plane.transform.position = poz;
        ActiveGameObjects.Add(plane);
        Grid.Add(poz);
    }
    public void remove(Vector3 poz)
    {
        GameObject responseP = ActiveGameObjects.Find(obj => obj.transform.position == poz);
        ActiveGameObjects.Remove(responseP);
        Destroy(responseP);
    }
    public void UpdateGrid(Vector3 PlayerPosition)
    {
         Vector3 gridPosition = GetGridPosition(PlayerPosition);
         List<Vector3> newPositions = GetPlayerRadiusCoordiates(gridPosition); 

         List<Vector3> positionsToRemove = Grid.Except(newPositions).ToList();
         List<Vector3> positionsToGenerate = newPositions.Except(Grid).ToList();

        Debug.Log("jaunas");
        foreach (var position in positionsToGenerate)
        {
            Debug.Log(position);
            add(position);
        }

        Debug.Log("vecas");
        foreach (var position in positionsToRemove)
        {
             remove(position);
         //  Debug.Log(position);
        }
    }
    public Vector3 GetGridPosition(Vector3 playerPosition)
    {     
        int x = (int)Mathf.Floor(playerPosition.x / panelSizeX);
        int z = (int)Mathf.Floor(playerPosition.z / panelSizeZ);
        Debug.Log(x +"/"+ z);
        return new Vector3(x,0,z);
    }
}
