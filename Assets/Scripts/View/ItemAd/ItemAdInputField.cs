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

    void Start()
    {
        field.onValidateInput += ValidateInput;
    }

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
                    return value > 0 && value <= 12;
                } catch (Exception)
                {
                    return false;
                }

            case Verifier.YEAR:
                return text.Length >= length;

            default:
                return false;
        }
    }

    private char ValidateInput(string text, int charIndex, char addedChar)
    {
        if (verifier == Verifier.MONTH || verifier == Verifier.YEAR)
        {
            if (!char.IsDigit(addedChar))
                return '\0';
        }

        return addedChar;
    }

    public string GetText()
    {
        return field.text;
    }

    public enum Verifier
    {
        EMAIL = 1,
        MONTH = 2,
        YEAR = 3
    }
}