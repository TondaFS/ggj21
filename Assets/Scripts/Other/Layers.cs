using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layers : MonoBehaviour
{
    //unity predifined layers
    public const int Default = 0;
    public const int TransparentFX = 1;
    public const int IgnoreRaycast = 2;
    public const int Water = 4;
    public const int UI = 5;

    //my layers
    public const int InteractableObject = 6;
    public const int Player = 7;
    public const int PlayerBlocker = 8;
    public const int Storm = 9;
    public const int Spawn = 10;

    public static class Mask
    {
        public static int DetectInteractableObject = ~((1 << Default ) | (1 << TransparentFX) | (1 << IgnoreRaycast) | 
            (1 << Water) | (1 << UI) | (1 << Player) | (1 << PlayerBlocker) | (1 << Storm) | (1 << Spawn));
    }
}
