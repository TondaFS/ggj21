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
    public const int InteractableObject = 8;



    public static class Mask
    {
        public static int DetectInteractableObject = ~((1 << Default ) | (1 << TransparentFX) | (1 << IgnoreRaycast) | 
            (1 << Water) | (1 << UI));
    }
}
