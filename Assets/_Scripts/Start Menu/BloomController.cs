using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BloomController : MonoBehaviour
{
    private enum Direction
    {
        Up,
        Down
    }
    
    [Header("REFERENCES")] [SerializeField]
    private PostProcessVolume postProcessVolume;
    private Bloom bloom;

    private float lowerBound = 20.0f;
    private float upperBound = 45.0f;
    private Direction direction;
    
    private void Awake()
    {
        direction = Direction.Up;
        postProcessVolume = GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out bloom);
    }

    private void Start()
    {
        StartCoroutine(IntensityChanger());
    }
    
    private IEnumerator IntensityChanger()
    {
        while (true)
        {
            while (direction == Direction.Up)
            {
                yield return new WaitForSeconds(.05f);
                bloom.intensity.value += 2f;

                if (bloom.intensity.value > upperBound)
                {
                    ChangeDirection();
                }
            }

            while (direction == Direction.Down)
            {
                yield return new WaitForSeconds(.05f);
                bloom.intensity.value -= 2f;

                if (bloom.intensity.value < lowerBound)
                {
                    ChangeDirection();
                }
            }
        }
    }

    private void ChangeDirection()
    {
        direction = direction == Direction.Down ? Direction.Up : Direction.Down;
    }
   
}
