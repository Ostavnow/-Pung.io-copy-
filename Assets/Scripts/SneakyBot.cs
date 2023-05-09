using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakyBot : BotLogic,IPlayerData
{
    
    
    public override void Pumping()
    {
        if(punchingBag.Length != 0)
        {
            targetPoint = punchingBag[0].transform.position;
            if(Vector2.Distance(punchingBag[0].transform.position,transform.position) < 2.5f)
            {
                // Debug.Log(punchingBag[0].transform?.name);
                isBotMoving = false;
                Hit();
            }
            else
            {
                isBotMoving = true;
            }
        }
        else
        {
            if(targetPoint == default || Vector2.Distance(targetPoint,transform.position) < 0.5f)
                {
                    targetPoint = new Vector3(Random.Range(-20,20),Random.Range(-25,25),0);
                }
        }
        //Проверка есть ли рядом игроки
        for(int i = 0;i < players.Length;i++)
        {
            if(players[i]?.transform != null && players[i].transform.GetComponent<IPlayerData>().GetPowerScore() > GetPowerScore())
            {
                //Если противник сильнее, то бежим от него в провоположную сторону
                if(Vector2.Distance(players[i].transform.position,transform.position) < 5f)
                {
                    targetPoint = ((players[i].transform.position - transform.position).normalized) * 3;
                    isBotMoving = true;
                    botStates = BotStates.Flight;
                }
            }
        }
    }
        
    public override void Attack()
    {

    }
    public override void Flight()
    {
        if(Vector3.Distance(transform.position,targetPoint) < 0.1f)
        {
            botStates = BotStates.Pumping;
        }
        for(int i = 0;i < players.Length;i++)
        {
            if(players[i]?.transform != null && players[i].transform.GetComponent<IPlayerData>().GetPowerScore() > GetPowerScore())
            {
                //Если противник сильнее, то бежим от него в провоположную сторону
                if(Vector2.Distance(players[i].transform.position,transform.position) < 5f)
                {
                    targetPoint = ((players[i].transform.position - transform.position).normalized) * 3;
                    isBotMoving = true;
                }
            }
        }
    }
    public override void Despair()
    {

    }
}
    
