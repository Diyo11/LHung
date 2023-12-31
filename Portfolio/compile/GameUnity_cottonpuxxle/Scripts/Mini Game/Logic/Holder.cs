using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : Interactive
{
    public bool isEmpty;

    public HashSet<Holder> linkHolders = new HashSet<Holder>();     //使用HashSet確保存儲為唯一
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
                //移動球
                currentBall.transform.position = holder.transform.position;
                currentBall.transform.SetParent(holder.transform);

                //交換球
                holder.CheckBall(currentBall);
                this.currentBall = null;

                //改變狀態
                this.isEmpty = true;
                holder.isEmpty = false;

                EventHandler.CallCheckGameStateEvent();     //每次移動呼叫
            }
        }
    }
}
