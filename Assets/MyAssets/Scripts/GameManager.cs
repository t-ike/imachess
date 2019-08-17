using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject nightPiece;

    public GameObject generatePoint;

    public GameObject mainCamera;


    public void GenerateNightPiece()
    {
        Debug.LogError("Generating");
        //Instantiate(nightPiece, new Vector3 (0, 0, 0), Quaternion.identity);
        Instantiate(nightPiece, generatePoint.transform.position, Quaternion.identity);
        //Instantiate(nightPiece, generatePoint.transform.position, Quaternion.identity);
        Debug.LogError("Generated");
    }

    public void StartBattlePhase()
    {
        //ToDo 回転させるような動きをつけたい
        Transform mainCameraTransform = mainCamera.transform;
        //Vector3 currentCameraPosition = mainCameraTransform.position;
        //Vector3 currentCameraRotation = mainCameraTransform.eulerAngles;

        Vector3 newCameraPosition = new Vector3(5, 2.3f, 1.8f);
        Vector3 newCameraRotation = new Vector3(30, -90, 0);

        mainCameraTransform.position = newCameraPosition;
        mainCameraTransform.eulerAngles = newCameraRotation;


    }

    public void StartPreparationPhase()
    {
        //ToDo 回転させるような動きをつけたい
        Transform mainCameraTransform = mainCamera.transform;
        //Vector3 currentCameraPosition = mainCameraTransform.position;
        //Vector3 currentCameraRotation = mainCameraTransform.eulerAngles;

        Vector3 newCameraPosition = new Vector3(-1.5f, 5.5f, -3);
        Vector3 newCameraRotation = new Vector3(80, 0, 0);

        mainCameraTransform.position = newCameraPosition;
        mainCameraTransform.eulerAngles = newCameraRotation;


    }

}