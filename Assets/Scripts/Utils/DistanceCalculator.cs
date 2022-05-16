using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Utils
{
    public class DistanceCalculator
    {
       
        public float getDistance(Vector3 position1, Vector3 position2)
        {
            var distance = (position1 - position2).magnitude;
            return distance;

        }
    }
}