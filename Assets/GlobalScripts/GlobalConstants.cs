using UnityEngine;

public static class GlobalConstants
{
    public const float GroundDistanceThreshold = .5f;

    public const float OffMapYValue = -30f;

    public static class Tags
    {
        public const string Player = "Player";

        public const string Score = "Score";
    }

    public static class LayerMasks
    {
        public static readonly LayerMask Ground = LayerMask.GetMask("Ground");

        public static readonly LayerMask SlipperyGround = LayerMask.GetMask("Slippery_Ground");

        public static readonly LayerMask Player = LayerMask.GetMask("Player");
    }

    public static class ButtonNames
    {
        public const string Jump = "Jump";
    }

    // NOTE: Axes is plural of axis :)
    public static class MovementAxes
    {
        public const string Horizontal = "Horizontal";

        public const string Vertical = "Vertical";
    }

    public static class CarrotIds
    {
        public static string Carrot1 = "Carrot1";
        
        public static string Carrot2 = "Carrot2";

        public static string Carrot3 = "Carrot3";
    }
}
