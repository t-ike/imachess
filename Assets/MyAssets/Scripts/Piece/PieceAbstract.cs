using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceSide : int
{

    playerSide,
    enemySide,

}

public abstract class PieceAbstract: MonoBehaviour, PieceInterface
{

    public PieceStatusStruct pieceStatus { get; set; }

    public PieceSide pieceSide { get; set; }

    public Animator animator;

    public virtual void Start()
    {

        animator = GetComponent<Animator>();
        LoadPieceStatus();

    }

    public virtual void LoadPieceStatus() {

    }

    public abstract List<PieceAbstract> Attack(Transform[,] enemyPieceArray);

    public void Idle()
    {

    }

    public void Special()
    {

    }

    //public virtual void GetHit(int str)
    //{

    //}

    public void GetHit(int str)
    {
        // ToDo: pieceAbstractにstatus変化用のメソッドを追加？
        PieceStatusStruct status = this.pieceStatus;
        status.hp -= str;
        this.pieceStatus = status;

        if (animator)
        {
            if (pieceStatus.hp <= 0)
            {
                animator.Play("Death");
            }
            else
            {
                animator.Play("GetHit");
            }
        }
    }

    public void Death()
    {

    }

}
    