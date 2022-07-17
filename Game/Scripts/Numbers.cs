using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public static class Numbers
{
    private const string root = "Numbers_";

    public static Sprite GetSprite(int digit)
    {
        Assert.IsTrue(digit >= 0 && digit <= 9, "Digit is outside of bounds!");
        
        string name = root + digit + ".prefab";
        //var p = AssetDatabase.LoadAssetAtPath<GameObject>(name);
        var p = (GameObject) Resources.Load(root + digit + ".prefab");
        var sprite = p.GetComponent<SpriteRenderer>().sprite;

        return sprite;
    }
}