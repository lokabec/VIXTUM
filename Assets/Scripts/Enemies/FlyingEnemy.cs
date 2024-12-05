using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class FlyingEnemy : Enemy
{
    EnemyDive dive;
    [SerializeField] private float _diveSpeed;
    [SerializeField] private float _returnSpeed;
    [SerializeField] private float _diveDistanceThreshold; 
    private Coroutine _coroutine;

    protected override void Start()
    {
        base.Start();
        dive = new(rb, _diveSpeed, _returnSpeed, _diveDistanceThreshold);
    }

    protected override void Update()
    {
        if (detectObjects.Detect(detectRange, layer).Count() > 0 && !dive.isDiving)
        {
            dive.targets = detectObjects.Detect(detectRange, layer);
            dive.RequestDive();
        }
        base.Update();
    }

    protected override void FixedUpdate()
    {
        if (dive.isDiving)
        {
            dive.DiveLogic();
            _coroutine ??= StartCoroutine(DiveTimer());
            
        }
        else if (!dive.isDiving && dive.diveBuffer == 0)
        {
            dive.ReturnLogic();
            _coroutine ??= StartCoroutine(ReturnTimer());
        }
        else
        {
            float moveInput = target.position.x - transform.position.x;
            movement.Move(moveInput);
        }


    }

    private IEnumerator DiveTimer()
    {
        yield return new WaitForSeconds(1);
        dive.isDiving = false;
        _coroutine = null;
        
    }

    public IEnumerator ReturnTimer()
    {
        yield return new WaitForSeconds(1);
        dive.diveBuffer = 1;
        _coroutine = null;

    }
}
