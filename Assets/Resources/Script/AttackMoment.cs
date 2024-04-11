using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMoment : SingletonWithMono<AttackMoment>
{
    protected override void Awake()
    {
        base.Awake();
        camPos = Camera.main.transform;
    }

    public bool isShake;// 判断相机是否在抖动
    public Transform camPos;

    private void Start()
    {
        isShake = false;
        
    }

    // 顿帧
    public void HitPause(int duration = 12)
    {
        StartCoroutine(Pause(duration));
    }
    IEnumerator Pause(int duration)
    {
        float pauseTime = duration / 60f;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(pauseTime);
        Time.timeScale = 1;
    }

    // 相机抖动
    public void CamShake(float duration = 0.1f, float strength = 0.065f)
    {
        if (!isShake)
        {
            StartCoroutine(Shake(duration, strength));
        }
    }
    IEnumerator Shake(float duration, float strength)
    {
        isShake = true;
        Vector3 startPos = camPos.position;

        while (duration > 0)
        {
            camPos.position = Random.insideUnitSphere * strength + startPos;
            duration -= Time.deltaTime;

            yield return null;
        }
        isShake = false;

    }

}
