using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UI
{    
    public class Timer : MonoBehaviour
    {
        [HideInInspector]
        public float time;
        void Update()
        {
            if(time >= 0)
            {
                time -= Time.deltaTime;
            }
        }
    }
}
