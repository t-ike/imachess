using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PieceStatusStruct
{

    public int hp;
    public int sp;
    public int maxHp;
    public int maxSp;

    public int str;
    public int def;
    public int agi;
    public int mag;
    public int reg;

    public int MyProperty { get; set; }

    public PieceStatusStruct(int hp, int sp, int maxHp, int maxSp, int str, int def, int agi, int mag, int reg) : this()
    {
        this.hp = hp;
        this.sp = sp;
        this.maxHp = maxHp;
        this.maxSp = maxSp;
        this.str = str;
        this.def = def;
        this.agi = agi;
        this.mag = mag;
        this.reg = reg;
    }


}
