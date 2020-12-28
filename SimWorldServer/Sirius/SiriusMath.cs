using System;

public class CSVer2
{
    public double x;
    public double y;
}

public class CSVer3
{
    public double x;
    public double y;
    public double z;

    public CSVer3()
    {
    }

    public CSVer3(CSVer3 v)
    {
        this.x = v.x;
        this.y = v.y;
        this.z = v.z;
    }
    public CSVer3 Add(CSVer3 pos)
    {
        x += pos.x;
        y += pos.y;
        z += pos.z;
        return this;
    }
    public CSVer3(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public void Copy(CSVer3 pos)
    {
        this.x = pos.x;
        this.y = pos.y;
        this.z = pos.z;
    }

    public void Normal()
    {
        double l = x * x + y * y + z * z;
        l = Math.Sqrt(l);
        x = x / l; y = y / l; z = z / l;
    }

    public double Length()
    {
        return Math.Sqrt(x * x + y * y + z * z);
    }

    public double DistanceTo(CSVer3 pos)
    {
        return Math.Sqrt( (pos.x-x)* (pos.x - x) + (pos.y - y) * (pos.y - y) + (pos.z - z) * (pos.z - z) );
    }

    public static CSVer3 operator +( CSVer3 v0, CSVer3 v1)
    {
        return new CSVer3(v0.x + v1.x, v0.y + v1.y, v0.z + v1.z);
    }

    public static CSVer3 operator -(CSVer3 v0, CSVer3 v1)
    {
        return new CSVer3(v0.x - v1.x, v0.y - v1.y, v0.z - v1.z);
    }

    public static CSVer3 operator *(CSVer3 v0, int v)
    {
        return new CSVer3(v0.x*v, v0.y * v, v0.z * v);
    }

    public static CSVer3 operator *(CSVer3 v0, float v)
    {
        return new CSVer3(v0.x * v, v0.y * v, v0.z * v);
    }

    public static CSVer3 operator *(CSVer3 v0, long v)
    {
        return new CSVer3(v0.x * v, v0.y * v, v0.z * v);
    }

    public static CSVer3 operator *(CSVer3 v0, double v)
    {
        return new CSVer3(v0.x * v, v0.y * v, v0.z * v);
    }

    public static bool operator ==(CSVer3 v0, CSVer3 v1)
    {
        return v0.x == v1.x && v0.y == v1.y && v0.z == v1.z;
    }

    public static bool operator !=(CSVer3 v0, CSVer3 v1)
    {
        return !(v0.x == v1.x && v0.y == v1.y && v0.z == v1.z);
    }

    public CSVer3 MoveTo(CSVer3 aim, float l)
    {
        CSVer3 dir = aim - this;
        if (dir.Length() <= l)
            return new CSVer3(aim.x, aim.y, aim.z);
        else
        {
            dir.Normal();
            return this + dir * l;
        }
    }

    public bool SelfMoveTo(CSVer3 aim, float l, float minl )
    {
        CSVer3 dir = aim - this;
        double len = dir.Length();
        if (l > len - minl)
        {
            dir.Normal();
            dir = this + dir * (len-minl);
            x = dir.x;
            y = dir.y;
            z = dir.z;

            return true;
        }
        else
        {
            dir.Normal();
            dir = this + dir * l;
            x = dir.x;
            y = dir.y;
            z = dir.z;
            return false;
        }

    }


}

