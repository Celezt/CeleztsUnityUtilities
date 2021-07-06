using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class LayerExtensions
{
    /// <summary>
    /// If the layer exist inside the included layers.
    /// </summary>
    /// <param name="mask">LayerMask.</param>
    /// <param name="layer">Layer to check.</param>
    /// <returns>If the layer exist</returns>
    public static bool Contains(this LayerMask mask, int layer) => ((1 << layer) & mask) != 0;
}
