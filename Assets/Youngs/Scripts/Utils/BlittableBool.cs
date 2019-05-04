
public struct BlittableBool
{
    public readonly byte Value;

    public BlittableBool(byte value)
    {
        Value = value;
    }   

    public BlittableBool(bool value)
    {
        Value = value ? (byte)1 : (byte)0;
    }

    //Turn type BlittableBool  => type bool b
    public static implicit operator bool(BlittableBool bb)
    {
        return bb.Value != 0;
    }

    //Turn type bool b => type BlittableBool 
    public static implicit operator BlittableBool(bool b)
    {
        return new BlittableBool(b);
    }
}