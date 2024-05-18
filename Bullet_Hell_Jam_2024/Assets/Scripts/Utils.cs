using UnityEngine;

public class Utils
{
    public static Vector2 RandomPonitOnRectEdge(float length, float width)
    {
        Vector2 pos;
        length = length / 2f;
        width = width / 2f;
        float x, y;
        // 50% chance x is fixed to width or -width, y is some random value between -length and length
        // 50% chance y is length or -length, x is some random value between -width and width
        if (Random.value > 0.5)
        {
            x = length * (Random.value > 0.5 ? 1 : -1);
            y = Random.Range(-width, width);
        }
        else
        {
            y = width * (Random.value > 0.5 ? 1 : -1);
            x = Random.Range(-length, length);
        }
        pos = new Vector2(x, y);
        return pos;
    }
}
