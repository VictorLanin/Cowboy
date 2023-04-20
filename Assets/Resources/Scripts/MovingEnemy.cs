using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LaninCode;
using UnityEngine;

namespace LaninCode
{
    public class MovingEnemy : MonoBehaviour
    {
        [SerializeField] private Vector3[] _points;
        private Stack<Vector3> _pointsStack;
        private void Awake()
        {
            _pointsStack = new Stack<Vector3>(_points.Reverse());
            StartCoroutine(MoveCoroutine());
        }
        

        private IEnumerator MoveCoroutine()
        {
            do
            {
                var pos= Vector3.MoveTowards(transform.position, _pointsStack.Peek(), 1f*Time.deltaTime);
                transform.position = pos;
                if(transform.position.Equals(_pointsStack.Peek()))
                {
                    _pointsStack.Pop();
                }
                yield return null;
            }
            while (_pointsStack.Count > 0);
        }
        
    }
}