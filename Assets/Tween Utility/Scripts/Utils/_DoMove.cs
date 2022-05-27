using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class _DoMove : _tween
{
    public Transform _target;

    // Start is called before the first frame update
    public override void Start()
    {
        Sequence seq = DOTween.Sequence();
        seq.Pause();

        seq.Append(this.transform.DOMove(_target.position, _tweenParameters._duration));
        _myTween = seq;
        base.Start();

        Events.OnGameStart += (value) =>
        {
            if(value)
            {
                seq.Play();
            }else
            {
                seq.Restart();
                seq.Pause();
            }
        };
    }
}
