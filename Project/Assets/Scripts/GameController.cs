using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public int turn = 1;
    public bool targetting = false;
    public bool moving = false;

    public void OnClickEndTurn()
    {
        if (turn == 1)
        {
            turn = 2;
        }
        else
        {
            turn = 1;
        }
    }

    public void OnClickFire()
    {
        targetting = true;
        moving = false;
    }

    public void OnClickMove()
    {
        moving = true;
        targetting = false;
    }
}
