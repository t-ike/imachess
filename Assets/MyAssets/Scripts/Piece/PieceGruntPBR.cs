using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceGruntPBR : PieceAbstract
{
    public override void Start()
    {

        base.Start();

    }

    public override void LoadPieceStatus()
    {
        this.pieceStatus = new PieceStatusStruct(
            hp: 50,
            sp: 10,
            maxHp: 50,
            maxSp: 10,
            str: 5,
            def: 5,
            agi: 100,
            mag: 0,
            reg: 5
            );

    }

    public GameObject getPrefab()
    {
        return (GameObject)Resources.Load("Assets/MyAssets/Prefabs/Piece/Mini Legion Grunt PBR HP/Prefab/GruntHP");
    }

    public override List<PieceAbstract> Attack(Transform[,] enemyPieceArray)
    {
        // 最も近い敵から攻撃（前列、手前優先、自身の位置は気にしない）
        List<PieceAbstract> targetPieces = new List<PieceAbstract>();
        bool canAttack = true;
        for (int i = 0; i < enemyPieceArray.GetLength(0); i++)
        {
            for (int j = 0; j < enemyPieceArray.GetLength(1); j++)
            {
                if (enemyPieceArray[i, j] && canAttack)
                {
                    PieceAbstract piece = enemyPieceArray[i, j].GetComponent<PieceAbstract>();

                    if (animator)
                    {
                        animator.Play("Attack01");
                    }
                    targetPieces.Add(piece);
                    //piece.GetHit(this.pieceStatus.str);
                    //攻撃は一回のみ
                    canAttack = false;

                }
            }
        }

        return targetPieces;

    }

    //public override void GetHit(int str)
    //{
    //    // ToDo: pieceAbstractにstatus変化用のメソッドを追加？
    //    PieceStatusStruct status = this.pieceStatus;
    //    status.hp -= str;
    //    this.pieceStatus = status;


    //    if (animator)
    //    {
    //        if (pieceStatus.hp <= 0)
    //        {
    //            animator.Play("Death");
    //        }
    //        else
    //        {
    //            animator.Play("GetHit");
    //        }
    //    }
    //}

}
