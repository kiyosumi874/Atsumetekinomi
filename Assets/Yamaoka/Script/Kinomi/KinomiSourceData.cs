using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Kinomi/KinomiData")]
public class KinomiSourceData : ScriptableObject
{
    public enum GenerationLocation
    {
        Near,    // ‘ƒ‚Ì‹ß‚­
        Far,     // ‘ƒ‚©‚ç‰“‚¢
        Middle   // ’†ŠÔ
    }

    [SerializeField]
    private int id;  // –Ø‚ÌÀ¯•Ê—pID
    [SerializeField]
    private string name;  // –Ø‚ÌÀ‚Ì–¼‘O
    [SerializeField]
    private GenerationLocation location;  // –Ø‚ÌÀ‚Ì¶¬êŠ

    /// <summary>
    /// –Ø‚ÌÀ‚ÌID‚ğæ“¾
    /// </summary>
    public int kinomiID
    {
        get { return id; }
    }
    /// <summary>
    /// –Ø‚ÌÀ‚Ì–¼‘O‚ğæ“¾
    /// </summary>
    public string kinomiName
    {
        get { return name; }
    }
    /// <summary>
    /// –Ø‚ÌÀ‚Ì¶¬êŠ
    /// </summary>
    public GenerationLocation kinomiGenerationLocation
    {
        get { return location; }
    }
}
