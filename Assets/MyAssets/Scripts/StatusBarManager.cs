using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBarManager : MonoBehaviour
{
    private PieceAbstract piece;

    public Image hpGreenBar;
    public Image hpRedBar;

    void Start()
    {
        piece = transform.parent.gameObject.GetComponent<PieceAbstract>();
        this.initParameter();
    }

    void Update()
    {
        hpGreenBar.fillAmount = (float)piece.pieceStatus.hp / piece.pieceStatus.maxHp;
    }

    private void initParameter()
    {
        hpGreenBar = transform.Find("HpStatusBarGreen").GetComponent<Image>();
        hpGreenBar.fillAmount = 1;

        hpRedBar = transform.Find("HpStatusBarRed").GetComponent<Image>();
        hpRedBar.fillAmount = 1;
    }
}
