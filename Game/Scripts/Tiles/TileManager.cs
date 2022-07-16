using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    private int Height, Width;

    [SerializeField]
    private GameObject tile; 

    public static List<Tile> tiles = new List<Tile>();

    void Start() => Generate();

    public void Generate()
    {
        for(int i = 0; i < Width; i++)
        {
            for(int o = 0; o < Height; o++)
            {
                GameObject TempReference = GameObject.Instantiate(tile, new Vector2(i,o), Quaternion.identity);
                tiles.Add(TempReference.GetComponent<Tile>());
                TempReference.GetComponent<Tile>().AssociatedNumber = Random.Range(1,7);
            }
        }
    }
}
