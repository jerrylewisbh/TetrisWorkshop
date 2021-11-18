using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject[] blockTypes;
    // Start is called before the first frame update
    void Start()
    {
        SpawnRandomPiece();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnRandomPiece() {

        int randomIndex = Random.Range(0, blockTypes.Length);
        GameObject piece = blockTypes[randomIndex];

        Transform c = piece.transform.GetChild(0);


        Instantiate(piece, transform.position, transform.rotation);


    }
}
