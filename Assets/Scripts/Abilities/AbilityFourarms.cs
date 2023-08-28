using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityFourarms : Ability
{
    private PlayerController _playerController;
    protected override void Initialize()
    {
        _playerController = MonoBehaviour.FindObjectOfType<PlayerController>();
        _timeOfAction = 10f;
        _recoveryTime = 25f;    
    }
    protected override IEnumerator AbilityCorutine()
    {
        bool isWillNextBlowBeRightSide = true;
        float timeFourarms = _playerController._timeAfterWhichBeNextBlow;
        float timeOfAction = _timeOfAction;
        while(timeOfAction > 0)
        {
            timeFourarms -= Time.deltaTime;
            timeOfAction -= Time.deltaTime;
            if(timeFourarms < 0)
            {
                timeFourarms = 0;
                Vector3 randomPosition;
                float handRadius = _playerController._hand.GetComponent<CircleCollider2D>().radius;
                Vector3 randomValue = new Vector3(Random.Range(-0.25f,0.25f),Random.Range(-0.25f,0.25f));
                if(isWillNextBlowBeRightSide)
                {
                    randomPosition = new Vector3(_playerController._handRightPoint.position.x + randomValue.x,_playerController._handRightPoint.position.y + randomValue.y,0);
                    _playerController.HandCreate(randomPosition,ref isWillNextBlowBeRightSide);
                }
                else
                {
                    randomPosition = new Vector3(_playerController._handLeftPoint.position.x + randomValue.x,_playerController._handLeftPoint.position.y + randomValue.y,0);
                    _playerController.HandCreate(randomPosition,ref isWillNextBlowBeRightSide);
                }
                timeFourarms = _playerController._timeAfterWhichBeNextBlow;
            }
            yield return null;
        }
        
    }
}
