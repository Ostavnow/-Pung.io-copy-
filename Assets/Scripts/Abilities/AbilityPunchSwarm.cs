using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AbilitiesSystem
{
    public class AbilityPunchSwarm : Ability
    {
        PlayerController _playerController;
        protected override void Initialize()
        {
            _timeOfAction = 10f;
            _recoveryTime = 25f;
            _playerController = GameObject.FindObjectOfType<PlayerController>();
        }
        protected override IEnumerator AbilityCorutine()
        {
            _playerController._hitEvent += PunchSwarm;
            _playerController._hitEvent -= _playerController.UsualHit;
            yield return new WaitForSeconds(_timeOfAction);
            _playerController._hitEvent += _playerController.UsualHit;
            _playerController._hitEvent -= PunchSwarm;
        }
        private void PunchSwarm()
        {
            Vector3 randomPosition;
            Vector3 randomValue = new Vector3(Random.Range(-0.25f,0.25f),Random.Range(-0.25f,0.25f));
            if(_playerController._isWillNextBlowBeRightSide)
            {
                for(int i = 0;i < 3;i++)
                {
                    randomPosition = new Vector3(_playerController._handRightPoint.position.x + randomValue.x,_playerController._handRightPoint.position.y + randomValue.y,0);
                    _playerController.HandCreate(new Vector3(Random.Range(randomPosition.x - 1,randomPosition.x + 1),Random.Range(randomPosition.y - 1,randomPosition.y + 1),0),ref _playerController._isWillNextBlowBeRightSide);
                }
            }
            else
            {
                for(int i = 0;i < 3;i++)
                {
                    randomPosition = new Vector3(_playerController._handLeftPoint.position.x + randomValue.x,_playerController._handLeftPoint.position.y + randomValue.y,0);
                    _playerController.HandCreate(new Vector3(Random.Range(randomPosition.x - 1,randomPosition.x + 1),Random.Range(randomPosition.y - 1,randomPosition.y + 1),0),ref _playerController._isWillNextBlowBeRightSide);
                }
            }
        }
    }
}