using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FallingCubeManager : MonoBehaviour
{
    public GameObject[] cubes;
    [SerializeField]
    private Vector3[] cubesPositions, cubesRotations;

    public bool playerGotKilled;

    void Start()
    {
        cubesPositions = new Vector3[cubes.Length];
        cubesRotations = new Vector3[cubes.Length];
        for (int i = 0; i < cubes.Length; i++)
        {
            cubesPositions[i] = cubes[i].transform.position;
        }

        for (int i = 0; i < cubes.Length; i++)
        {
            cubesRotations[i] = cubes[i].transform.eulerAngles;
        }
    }


    public void ResetCubesPositions()
    {
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].transform.position = cubesPositions[i];
            cubes[i].transform.eulerAngles  = cubesRotations[i];
        }
    }
}