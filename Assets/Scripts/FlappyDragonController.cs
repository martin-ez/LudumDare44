using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SpriterDotNetUnity;
using System;
using SpriterDotNet;
using System.Text;
using UnityEditor;

public class FlappyDragonController : MonoBehaviour
{
    public float upForce;
    private bool isDead = false;

    //private Animator anim;
    private UnityAnimator animator;
    private Rigidbody2D rb;

    public float transitionTime = 1f;

    public float MaxSpeed = 5.0f;
    public float DeltaSpeed = 0.2f;
    public float TransitionTime = 1.0f;

    [HideInInspector]
    public float AnimatorSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<SpriterDotNetBehaviour>().Animator;
        rb = GetComponent<Rigidbody2D>();
        animator.Play(GetAnimationDirect(animator, 1));
        animator.Speed = AnimatorSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == false)
        {
            if (Input.GetButtonDown("Flap"))
            {
                //SwitchAnimation(1);

                //EditorApplication.isPaused = true;
                animator.Play(GetAnimationDirect(animator, 0));
                animator.AnimationFinished += AnimatorOnAnimationFinished;
                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2(0, upForce));
            }
        }

    }

    private void AnimatorOnAnimationFinished(string obj)
    {
        animator.Play(GetAnimationDirect(animator, 1));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector2.zero;
        isDead = true;
    }

    private void SwitchAnimation(int offset)
    {
        animator.Play(GetAnimation(animator, offset));
    }

    private void Transition(int offset)
    {
        animator.Transition(GetAnimation(animator, offset), TransitionTime * 1000.0f);
    }

    private void ChangeAnimationSpeed(float delta)
    {
        var speed = animator.Speed + delta;
        speed = Math.Abs(speed) < MaxSpeed ? speed : MaxSpeed * Math.Sign(speed);
        AnimatorSpeed = (float)Math.Round(speed, 1, MidpointRounding.AwayFromZero);
    }

    private void ReverseAnimation()
    {
        AnimatorSpeed *= -1;
    }

    private void PushCharacterMap()
    {
        SpriterCharacterMap[] maps = animator.Entity.CharacterMaps;
        if (maps == null || maps.Length == 0) return;
        SpriterCharacterMap charMap = animator.SpriteProvider.CharacterMap;
        if (charMap == null) charMap = maps[0];
        else
        {
            int index = charMap.Id + 1;
            if (index >= maps.Length) charMap = null;
            else charMap = maps[index];
        }

        if (charMap != null) animator.SpriteProvider.PushCharMap(charMap);
    }

    private string GetVarValues()
    {
        StringBuilder sb = new StringBuilder();

        FrameData frameData = animator.FrameData;

        foreach (var entry in frameData.AnimationVars)
        {
            object value = GetValue(entry.Value);
            sb.Append(entry.Key).Append(" = ").AppendLine(value.ToString());
        }
        foreach (var objectEntry in frameData.ObjectVars)
        {
            foreach (var varEntry in objectEntry.Value)
            {
                object value = GetValue(varEntry.Value);
                sb.Append(objectEntry.Key).Append(".").Append(varEntry.Key).Append(" = ").AppendLine((value ?? string.Empty).ToString());
            }
        }

        return sb.ToString();
    }

    private object GetValue(SpriterVarValue varValue)
    {
        object value;
        switch (varValue.Type)
        {
            case SpriterVarType.Float:
                value = varValue.FloatValue;
                break;
            case SpriterVarType.Int:
                value = varValue.IntValue;
                break;
            default:
                value = varValue.StringValue;
                break;
        }
        return value;
    }

    private string GetTagValues()
    {
        FrameData fd = animator.FrameData;

        StringBuilder sb = new StringBuilder();
        foreach (string tag in fd.AnimationTags) sb.AppendLine(tag);
        foreach (var objectEntry in fd.ObjectTags)
        {
            foreach (string tag in objectEntry.Value) sb.Append(objectEntry.Key).Append(".").AppendLine(tag);
        }

        return sb.ToString();
    }

    private static bool GetAxisDownPositive(string axisName)
    {
        return Input.GetButtonDown(axisName) && Input.GetAxis(axisName) > 0;
    }

    private static bool GetAxisDownNegative(string axisName)
    {
        return Input.GetButtonDown(axisName) && Input.GetAxis(axisName) < 0;
    }

    private static string GetAnimation(UnityAnimator animator, int offset)
    {
        List<string> animations = animator.GetAnimations().ToList();
        int index = animations.IndexOf(animator.CurrentAnimation.Name);
        index += offset;
        if (index >= animations.Count) index = 0;
        if (index < 0) index = animations.Count - 1;
        return animations[index];
    }

    private static string GetAnimationDirect(UnityAnimator animator, int index)
    {
        List<string> animations = animator.GetAnimations().ToList();
        return animations[index];
    }
}
