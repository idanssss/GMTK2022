using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public static class Numbers
{
    private const string root = "Assets/GMTK2022/Game/Sprites/Numbers/Numbers/Numbers Prefab/Numbers_";

    public static Sprite GetSprite(int digit)
    {
        Assert.IsTrue(digit >= 0 && digit <= 9, "Digit is outside of bounds!");
        
        string name = root + digit + ".prefab";
        var p = AssetDatabase.LoadAssetAtPath<GameObject>(name);
        var sprite = p.GetComponent<SpriteRenderer>().sprite;

        return sprite;
    }
}