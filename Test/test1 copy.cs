using System;
using System.Collections.Generic;

class Program {

    public class Vec3 {
        public float x;
        public float y;
        public float z;

        public Vec3 (float _x, float _y, float _z) {
            x = _x;
            y = _y;
            z = _z;
        }
    }

    public static void Main () {
        var map = new Dictionary<int, int> ();
        string line = Console.ReadLine ();
        var list1 = new List<Vec3> ();
        var list2 = new List<Vec3> ();
        SplitLine (line, list1, list2);

        Vec3 v1 = GetPos (list1);
        Vec3 v2 = GetPos (list2);
        float d = Math.Abs (v1.x - v2.x) + Math.Abs (v1.y - v2.y) + Math.Abs (v1.z - v2.z);
        float p1 = GetHalf (v1);
        float p2 = GetHalf (v2);

        if (p1 + p2 < d) Console.WriteLine (1);
        else {
            Console.WriteLine (0);
        }
    }

    private static float GetHalf (Vec3 pos) {
        float p1 = pos.x * pos.x + pos.y * pos.y + pos.z * pos.z;
        return p1;
    }

    public static Vec3 GetPos (List<Vec3> list1) {
        float hX, hY, hZ;
        float x1 = Math.Abs (list1[0].x - list1[1].x);
        float y1 = Math.Abs (list1[0].y - list1[1].y);
        float z1 = Math.Abs (list1[0].z - list1[1].z);

        hX = x1 / 2;
        hY = y1 / 2;
        hZ = z1 / 2;

        return new Vec3 (hX, hY, hZ);
    }

    public static void SplitLine (string line, List<Vec3> list1, List<Vec3> list2) {
        string[] words = line.Split (' ');

        for (int i = 0; i < 6; i += 3) {
            float x = float.Parse (words[i]);
            float y = float.Parse (words[i + 1]);
            float z = float.Parse (words[i + 2]);
            list1.Add (new Vec3 (x, y, z));
        }

        for (int i = 6; i < 12; i += 3) {
            float x = float.Parse (words[i]);
            float y = float.Parse (words[i + 1]);
            float z = float.Parse (words[i + 2]);
            list2.Add (new Vec3 (x, y, z));
        }

    }

}