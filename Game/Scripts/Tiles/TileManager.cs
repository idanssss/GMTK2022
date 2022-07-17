using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    private int width, height;

    [SerializeField]
    private float space;

    [SerializeField]
    private GameObject tilePrefab;

    public float shakeDuration = 1.5f;
    [SerializeField] private float shakeStrength = 1f;
    
    public int[] dropped = new int[6];

    public event System.Action OnNewBoard;
    
    public static Tile[,] tiles;

    void Start()
    {
        Generate();
        Assert.IsNotNull(tilePrefab, "Tile prefab is null!");
    }

    public void Generate()
    {
        DestroyAll();
        tiles = new Tile[width, height];
        
        float spacingOffset = tilePrefab.transform.localScale.x + space;
        
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

                Vector2 pos = new Vector2(xPos * spacingOffset, yPos * spacingOffset);
                GameObject tileGo = Instantiate(tilePrefab, pos, Quaternion.identity, transform);
                
                tileGo.name = $"Tile {i} {j}";
                Tile tile = tileGo.GetComponent<Tile>();
                tiles[i, j] = tile;
                
                tile.SetAssociatedNumber(Random.Range(1,7));
                tile.UpdateUI();
            }
        }

        AudioManager.instance.Play("TileAppear");

        OnNewBoard?.Invoke();
    }

    public void DestroyAll()
    {
        if (tiles != null)
        {
            foreach (var tile in tiles)
                if (tile && tile.gameObject) DestroyImmediate(tile.gameObject);
        }

        while (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
                DestroyImmediate(transform.GetChild(i).gameObject);
        }

        tiles = new Tile[width, height];
    }
    
    public void Drop(int number)
    {
        if (number < 1 || number > 6)
        {
            Debug.LogError($"Invalid dice number {number}", gameObject);
            return;
        }

        tiles ??= new Tile[width, height];
        foreach (var tile in tiles)
        {
            if (tile.AssociatedNumber == number)
                tile.Drop(shakeDuration, shakeStrength);
        }

        for (int i = 0; i < dropped.Length; i++)
        {
            if (dropped[i] != 0) continue;
            
            dropped[i] = number;
            break;
        }

        AudioManager.instance.Play("Rumble");
    }

    public void ResetTiles()
    {
        Generate();
        dropped = new int[6];
    }

    public Vector2 GetRandomTile()
    {
        // Get random die number
        int number = Random.Range(1, 7);
        
        while (dropped.Contains(number))
            number = Random.Range(1, 7);

        foreach (var t in tiles)
        {
            if (t.AssociatedNumber == number) return t.transform.position;
        }

        return Vector2.zero;
    }
}
