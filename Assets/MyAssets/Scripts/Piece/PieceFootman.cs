using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceFootman : PieceAbstract
{
    public override void Start()
    {
        base.Start();
        
    }

    public override void LoadPieceStatus()
    {
        this.pieceStatus = new PieceStatusStruct(
            hp: 30,
            sp: 30,
            maxHp: 30,
            maxSp: 30,
            str: 10,
            def: 10,
            agi: 10,
            mag: 0,
            reg: 10
            );

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
    //        if(pieceStatus.hp <= 0)
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
