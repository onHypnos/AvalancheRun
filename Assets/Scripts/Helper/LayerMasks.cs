public static class LayerMasksConsts
{
    /// <summary>
    /// Warp layer masc for PlayerController
    /// </summary>
    public readonly static int WarpCheckLayerMask = ~((1 << 7) | (1 << 2));
    public readonly static int MovementRaycastLayerMask = (1 << 6) | (1 << 7);
    public readonly static int WarpWallAnimationLayerMask = ~((1 << 2) | (1 << 6));




}
