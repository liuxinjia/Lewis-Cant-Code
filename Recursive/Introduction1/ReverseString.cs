//time complexity: o(n/2) : space complexity: o(n);

public class Solution {
    public void ReverseString (char[] s) {
        int v = s.Length / 2;
        Reverse (s, s.Length % 2 == 0 ? v : v + 1);
    }

    void Reverse (char[] s, int n) {
        if (s == null || n == s.Length) return;

        Reverse (s, n + 1);
        Swap (s, n);
    }

    void Swap (char[] s, int end) {
        char temp = s[end];
        int start = s.Length - 1 - end;
        s[end] = s[start];
        s[start] = temp;
    }

    //Divide and Conquer
    public void ReverseString (char[] s) {
        s = SubdivideString (s.ToString ()).ToCharArray ();
    }

    string SubdivideString (string str) {
        if (str.Length < 0x2) return str;

        int mid = str.Length / 2;
        var left = SubdivideString (str.Substring (0, mid));
        var right = SubdivideString (str.Substring (mid + 1));

        return $"{right}{left}";
    }
}