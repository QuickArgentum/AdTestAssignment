using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
[RequireComponent(typeof(Animation))]
public class ItemAdInputField : MonoBehaviour
{
    public InputField field;
    public Animation anim;
    public Verifier verifier;
    public int length = 0;

    public bool Verify()
    {
        bool isValid = IsTextValid();
        if (!isValid)
            anim.Play(Animations.INPUT_FIELD_SHAKE);

        return isValid;
    }

    private bool IsTextValid()
    {
        string text = field.text;
        switch (verifier)
        {
            case Verifier.EMAIL:
                Regex regex = new Regex(@".+\@.+\..+");
                return regex.IsMatch(text);

            case Verifier.MONTH:
                try
                {
                    int value = int.Parse(text);
                    return value <= 12;
                } catch (Exception)
                {
                    return false;
                }

            case Verifier.LENGTH:
                return text.Length >= length;

            default:
                return false;
        }
    }

    public string GetText()
    {
        return field.text;
    }

    public enum Verifier
    {
        EMAIL = 1,
        MONTH = 2,
        LENGTH = 3
    }
}