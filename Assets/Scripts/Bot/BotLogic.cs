using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(PlayerBot)),RequireComponent(typeof(BotPlayerController))]
public class BotLogic : MonoBehaviour
{
    private enum BotStates {Pumping, Attack, Flight, Despair};
    private BotStates botStates;
    [HideInInspector]
    private Collider2D[] punchingBag;
    private Collider2D[] players;
    private Collider2D[] Hand;
    private PlayerBot player;
    private BotPlayerController botController;
    [SerializeField,Range(1,100)]
    private float _intellect;
    [SerializeField,Range(1,100)]
    private float _ferocity;
    private void Start()
    {
        player = GetComponent<PlayerBot>();
        botController = GetComponent<BotPlayerController>();
        Debug.Log(botController);
    }
    private void FixedUpdate()
    {
        punchingBag = AlgorithmsBot.OverlapBoxAllPuchingBag(transform.position);
        players = AlgorithmsBot.OverlapBoxAllPlayer(transform.position);
        if(botStates == BotLogic.BotStates.Pumping)
        {
            Pumping();
        }
        else if(botStates == BotLogic.BotStates.Attack)
        {
            Attack();
        }
        else if(botStates == BotLogic.BotStates.Flight)
        {
            Flight();
        }
        else if(botStates == BotLogic.BotStates.Despair)
        {
            Despair();
        }
    }
    public void Pumping()
    {
        if(punchingBag[0] != null)
        {
            botController.TargetPoint = punchingBag[0].transform.position;
            if(IsDistanceTargetLess(punchingBag[0].transform.position,2.5f))
            {
                botController._isBotMoving = false;
                botController.Hit();
            }
            else
            {
                botController._isBotMoving = true;
            }
        }
        else if(botController.TargetPoint == default || IsDistanceTargetLess(botController.TargetPoint,0.5f))
        {
            botController.TargetPoint = new Vector3(Random.Range(-20,20),Random.Range(-25,25),0);
        }
        else
        {
            botController._isBotMoving = true;
        }
        //Проверка есть ли рядом игроки
        for(int i = 0;players[i]?.transform;i++)
        {
            if(IsOpponentStronger(i))
            {
                //Если противник сильнее, то бежим от него в провоположную сторону
                if(IsDistanceTargetLess(players[i].transform.position,5))
                {
                    botController.TargetPoint = transform.position + (transform.position - players[i].transform.position).normalized * 2;
                    botController._isBotMoving = true;
                    botStates = BotStates.Flight;
                    Debug.Log("Убегаем");
                }
            }
            else if(!IsOpponentStronger(i))
            {
                if(IsDistanceTargetLess(players[i].transform.position,5))
                {
                    botController.TargetPoint = players[i].transform.position;
                    botController._isBotMoving = true;
                    botStates = BotStates.Attack;
                    Debug.Log("Атакуем");
                }
            }
        }
    }
        
    public void Attack()
    {
        botController.TargetPoint = players[0].transform.position;
        if(IsDistanceTargetLess(players[0].transform.position,2.5f))
        {
            botController._isBotMoving = false;
            botController.Hit();
        }
        else
        {
            botController._isBotMoving = true;
        }
    }
    public void Flight()
    {
        if(IsDistanceTargetLess(botController.TargetPoint,0.1f))
        {
            botStates = BotStates.Pumping;
        }
        for(int i = 0;players[i]?.transform;i++)
        {
            if(players[i]?.transform != null && IsOpponentStronger(i))
            {
                //Если противник сильнее, то бежим от него в провоположную сторону
                if(Vector2.Distance(players[i].transform.position,transform.position) < 5f)
                {
                    botController.TargetPoint = transform.position + (transform.position - players[i].transform.position).normalized * 2;
                    botController._isBotMoving = true;
                }
            }
        }
    }
    public void Despair()
    {

    }
    private bool IsOpponentStronger(int index) => players[index].transform.GetComponent<IPlayerInfo>().GetPowerScore() > player.GetPowerScore();
    private bool IsDistanceTargetLess(Vector2 target,float distance) => Vector2.Distance(transform.position,target) < distance;
    // private IEnumerator Rage()
    // {
    //     yield return WaitForSeconds(10);
    // }
}