using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface PieceInterface
{
    void LoadPieceStatus();
    List<PieceAbstract> Attack(Transform[,] pieceArray);
    void Idle();
    void Special();
    void GetHit(int str);
    void Death();
}