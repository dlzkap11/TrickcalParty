using UnityEngine;

public class Define
{
    public enum WorldObject
    {
        Unknown,
        Player,
        AI,

    }

    public enum Tiles
    {
        Unknown,
        GreenTile,
        RedTile,
        BlueTile,
        StratTile,
    }

    public enum State
    {
        Moving,
        Idle,

    }

    public enum UIEvent
    {
        Click,
        Drag,
    }

    public enum MouseEvent
    {
        Press,
        PointerDown,
        PointerUp,
        Click,
    }

    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game,
    }

    public enum Sound
    {
        BGM,
        Effact,
        MaxCount,
    }

    public enum CameraMode
    {
        QuarterView,
    }
}
