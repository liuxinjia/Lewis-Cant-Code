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

        Vec3 v1 = list2[0];
        Vec3 v2 = list1[0];
        float d = v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;

        if (d > 0) Console.WriteLine (1);
        else if (d == 0) {
            Console.WriteLine (0);
        } else {
            Console.WriteLine (-1);
        }
    }

    public static void SplitLine (string line, List<Vec3> list1, List<Vec3> list2) {
        string[] words = line.Split (' ');

        for (int i = 0; i < 3; i += 3) {
            float x = Par (words[i]);
            float y = Par (words[i + 1]);
            float z = Par (words[i + 2]);
            list1.Add (new Vec3 (x, y, z));
        }

        for (int i = 3; i < 6; i += 3) {
            float x = Par (words[i]);
            float y = Par (words[i + 1]);
            float z = Par (words[i + 2]);
            list2.Add (new Vec3 (x, y, z));
        }

    }

    private static float Par (string words) {
        if (words[0] == '.') words = '0' + words;
        return float.Parse (words);
    }
}