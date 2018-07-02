using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINoteSelection : MonoBehaviour
{
    public RectTransform button;
    Vector2 from;
    float angle;

    List<Vector2> segments;

    public void Start()
    {
        from = Camera.main.ScreenToViewportPoint(button.position);
    }

    public void Update()
    {
        var to = Camera.main.ScreenToViewportPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        var up = Camera.main.ViewportToScreenPoint((from + new Vector2(from.x, from.y + 1)) - from);
        var mouse = new Vector2(Input.mousePosition.x - from.x, Input.mousePosition.y - from.y);

        //Debug.DrawLine(Camera.main.ViewportToScreenPoint(from), up, Color.cyan);
        //Debug.DrawLine(Camera.main.ViewportToScreenPoint(from), mouse);

        //angle = Mathf.Atan2(Camera.main.ViewportToScreenPoint(mouse.normalized).y, Camera.main.ViewportToScreenPoint(mouse.normalized).x) * Mathf.Rad2Deg; // - Mathf.Atan2(mouse.y, mouse.x)) * Mathf.Rad2Deg;


        //print(Mathf.Atan2(up.x, Camera.main.ViewportToScreenPoint(mouse).x) - Mathf.Atan2(up.y, Camera.main.ViewportToScreenPoint(mouse).y) * Mathf.Rad2Deg);

        //angle = atan2(vector2.y, vector2.x) - atan2(vector1.y, vector1.x);

        angle = Mathf.DeltaAngle
        (
            Mathf.Atan2(from.normalized.y, from.normalized.x)       * Mathf.Rad2Deg, 
            Mathf.Atan2(mouse.normalized.y, mouse.normalized.x) * Mathf.Rad2Deg
        );

        //print(angle);

    }
}