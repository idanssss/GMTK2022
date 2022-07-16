using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    private int height, width;

    [SerializeField]
    private float space;

    [SerializeField]
    private GameObject tilePrefab; 

    public static List<Tile> tiles = new List<Tile>();

    void Start()
    {
        Generate();
        
        Assert.IsNotNull(tilePrefab, "Tile prefab is null!");
    }

    public void Generate()
    {
        foreach (var tile in tiles)
        {
            if (tile && tile.gameObject) DestroyImmediate(tile.gameObject);
        }
        
        for(int i = 0; i < width; i++)
        {
            float xPos = i - width / 2;
            if (width % 2 == 0)
                xPos += 0.5f;
            
            for(int j = 0; j < height; j++)
            {
                float yPos = j - height / 2;
                if (height % 2 == 0)
                    yPos += 0.5f;

                Vector2 pos = new Vector2(xPos * (1 + space), yPos * (1 + space));
                GameObject tileGo = Instantiate(tilePrefab, pos, Quaternion.identity, transform);
                
                tileGo.name = $"Tile {i} {j}";
                Tile tile = tileGo.GetComponent<Tile>();
                tiles.Add(tile);
                
                tile.SetAssociatedNumber(Random.Range(1,7));
                tile.UpdateUI();
            }
        }
    }
}
