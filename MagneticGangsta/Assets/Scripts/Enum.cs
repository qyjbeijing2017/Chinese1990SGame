using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Polarity
{
    public enum PolarityValue
    {
        None = 0,
        North = 1,
        Sourth = 2
    }

    public PolarityValue Value;
    public Polarity(Polarity.PolarityValue value)
    {
        Value = value;
    }

    public static Polarity None { get { return new Polarity(PolarityValue.None); } }
    public static Polarity North { get { return new Polarity(PolarityValue.North); } }
    public static Polarity Sourth { get { return new Polarity(PolarityValue.Sourth); } }

    public static Polarity operator !(Polarity p)
    {
        if (p.Value == PolarityValue.None)
        {
            return Polarity.None;
        }
        if (p.Value == PolarityValue.North)
        {
            return new Polarity(PolarityValue.Sourth);
        }
        else
        {
            return new Polarity(PolarityValue.North);
        }
    }

    public static bool operator ==(Polarity p1, Polarity p2)
    {
        return p1.Value == p2.Value;
    }

    public static bool operator !=(Polarity p1, Polarity p2)
    {
        return p1.Value != p2.Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public Polarity Copy()
    {
        return new Polarity(Value);
    }
}