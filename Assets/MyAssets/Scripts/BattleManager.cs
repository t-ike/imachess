using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BattleManager : MonoBehaviour
{

    public GameObject playerTilesParent;
    public GameObject enemyTilesParent;

    public GameObject hpStatusBarCanvas;
    
    public int tileWidth = 3;
    public int tileHeight = 3;

    Transform[,] enemyPieceArray;
    Transform[,] playerPieceArray;
    List<Transform> attackOrder = new List<Transform>();

    public void SetupBattle()
    {
        playerPieceArray = new Transform[tileWidth, tileHeight];
        enemyPieceArray = new Transform[tileWidth, tileHeight];


        foreach (Transform tileTransform in playerTilesParent.transform)
        {
            //tileにPieceが配置されている場合、enemyPieceArrayに保持
            if (tileTransform.HasChild())
            {
                // x は奥行（奥がマイナス）、 zは幅（右がプラス）
                // Rowをz, Columnを-xに定義する
                int pieceColumn = (-(int)-tileTransform.localPosition.x) + 1;
                int pieceRow = (-(int)tileTransform.localPosition.z) + 1;

                //管理用の配列にpieceオブジェクトを保持
                Transform playerPiece = tileTransform.GetChild(0);
                playerPieceArray[pieceRow, pieceColumn] = playerPiece;

                //pieceの配置チームを指定
                playerPiece.GetComponent<PieceAbstract>().pieceSide = PieceSide.playerSide;

                //pieceオブジェクトにstatusBarを配置
                GameObject statusBar = Instantiate(hpStatusBarCanvas);

                statusBar.transform.parent = playerPiece;
                statusBar.transform.localPosition = new Vector3(0, 2.5f, 0);
                statusBar.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
                statusBar.transform.localRotation = Quaternion.Euler(0, 90, 0);

                statusBar.AddComponent<StatusBarManager>();
            }

        }

        foreach (Transform tileTransform in enemyTilesParent.transform)
        {
            //tileにPieceが配置されている場合、enemyPieceArrayに保持
            if (tileTransform.HasChild())
            {
                // x は奥行（奥がマイナス）、 zは幅（右がプラス）
                // Rowをz, Columnを-xに定義する
                int pieceColumn = (-(int)-tileTransform.localPosition.x) + 1;
                int pieceRow = (int)tileTransform.localPosition.z + 1;

                //管理用の配列にpieceオブジェクトを保持
                Transform enemyPiece = tileTransform.GetChild(0);
                enemyPieceArray[pieceRow, pieceColumn] = enemyPiece;

                //pieceの配置チームを指定
                enemyPiece.GetComponent<PieceAbstract>().pieceSide = PieceSide.enemySide;

                //pieceオブジェクトにstatusBarを配置
                GameObject statusBar = Instantiate(hpStatusBarCanvas);

                statusBar.transform.parent = enemyPiece;
                statusBar.transform.localPosition = new Vector3(0, 2.5f, 0);
                statusBar.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
                statusBar.transform.localRotation = Quaternion.Euler(0, 90, 0);

                statusBar.AddComponent<StatusBarManager>();
            }
            
        }

        CalcOrder();

    }

    public void CalcOrder()
    {

        foreach (int i in Enumerable.Range(0, tileHeight))
        {
            foreach (int j in Enumerable.Range(0, tileWidth))
            {
                Debug.Log(enemyPieceArray[i, j]);
                if (enemyPieceArray[i, j] != null)
                {

                    attackOrder.Add(enemyPieceArray[i, j]);

                }
                if (playerPieceArray[i, j] != null)
                {

                    attackOrder.Add(playerPieceArray[i, j]);

                }

            }
        }

        // agiの降順に並び替え
        attackOrder.Sort(
            (a, b) => b.GetComponent<PieceAbstract>().pieceStatus.agi - a.GetComponent<PieceAbstract>().pieceStatus.agi
            );
        Debug.Log(attackOrder);
    }

    IEnumerator ActionAttackAll()
    {
        
        foreach (Transform piece in attackOrder)
        {

            PieceAbstract pieceAbstract = piece.GetComponent<PieceAbstract>();
            List<PieceAbstract> targetPieces = new List<PieceAbstract>();

            if (pieceAbstract.pieceSide == PieceSide.playerSide)
            {
                //pieceの配置チームを指定
                targetPieces = pieceAbstract.Attack(enemyPieceArray);
            }
            else if( pieceAbstract.pieceSide == PieceSide.enemySide)
            {
                targetPieces = pieceAbstract.Attack(playerPieceArray);
            }
            
            Animator anim = piece.GetComponent<Animator>();
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length / 2);
            //yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);

            float maxGetHitLength = 0;
            foreach (PieceAbstract targetPiece in targetPieces)
            {
                // ダメージ処理 & アニメーション開始
                targetPiece.GetHit(pieceAbstract.pieceStatus.str);

                // 攻撃対象の中で最も長いアニメーションフレーム数を取得
                Animator targetAnim = targetPiece.GetComponent<Animator>();
                float targetAnimLength = targetAnim.GetCurrentAnimatorStateInfo(0).length;

                if (maxGetHitLength < targetAnimLength)
                {
                    maxGetHitLength = targetAnimLength;
                }

            }
            // ダメージアニメーションを終えてから次の攻撃処理へ
            yield return new WaitForSeconds(maxGetHitLength);
        }

    }

    public void NextAction()
    {
        StartCoroutine(ActionAttackAll());
    }

    //test用
    public void GetHit()
    {
        foreach (Transform piece in attackOrder)
        {

            piece.GetComponent<PieceAbstract>().GetHit(10);
        }   

    }


    public void CheckBattleStatus()
    {

    }
}
