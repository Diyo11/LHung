using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : Interactive
{
    public bool isEmpty;

    public HashSet<Holder> linkHolders = new HashSet<Holder>();     //�ϥ�HashSet�T�O�s�x���ߤ@
    public BallName matchBall;
    private Ball currentBall;

    public void CheckBall(Ball ball)
    {
        currentBall = ball;
        if(ball.ballDetails.ballName == matchBall)
        {
            currentBall.isMatch = true;
            currentBall.SetRight();
        }
        else
        {
            currentBall.isMatch = false;
            currentBall.SetWrong();
        }
    }

    public override void EmptyClicked()
    {
        foreach(var holder in linkHolders)
        {
            if(holder.isEmpty)
            {
                //���ʲy
                currentBall.transform.position = holder.transform.position;
                currentBall.transform.SetParent(holder.transform);

                //�洫�y
                holder.CheckBall(currentBall);
                this.currentBall = null;

                //���ܪ��A
                this.isEmpty = true;
                holder.isEmpty = false;

                EventHandler.CallCheckGameStateEvent();     //�C�����ʩI�s
            }
        }
    }
}