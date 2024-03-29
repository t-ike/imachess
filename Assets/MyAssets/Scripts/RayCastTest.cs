﻿using UnityEngine;

public class RayCastTest : MonoBehaviour
{
    GameObject gameObject;

    void Update()
    {
        // Click確認
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                gameObject = hit.collider.gameObject;
                Debug.DrawRay(ray.origin, ray.direction * 3, Color.green, 5, false);
                Debug.Log(gameObject);
            }
        }
    }
}