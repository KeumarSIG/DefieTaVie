/*
    J'essayais quelque chose, ce n'est pas concluant pour le moment.
    Je verrai cela plus tard.
/*



using UnityEngine;
using System.Collections;

public static class SpecialFunctions
{
    public static ThrowInDirection(Vector2 lastMovement, GameObject clone)
    {
        if (lastMovement == new Vector2(1, 0)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, -90f); // RIGHT
        else if (lastMovement == new Vector2(-1, 0)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, 90f); // LEFT
        else if (lastMovement == new Vector2(0, 1)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f); // UP
        else if (lastMovement == new Vector2(0, -1)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, 180f); // DOWN
        else if (lastMovement == new Vector2(1, 1)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, -45f); // UP RIGHT
        else if (lastMovement == new Vector2(-1, 1)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, 45f); // UP LEFT
        else if (lastMovement == new Vector2(1, -1)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, -135f); // DOWN RIGHT
        else if (lastMovement == new Vector2(-1, -1)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, 135f); // DOWN LEFT

        return 

    }
}
*/