public class Solution {
    public IList<string> LetterCombinations (string digits) {
        var list = new List<string> ();
        if (digits.Length == 0) return list;

        BackTrack (digits, 0, list, new StringBuilder (digits.Length));
        return list;
    }

    void BackTrack (string digits, int depth, List<string> list, StringBuilder letters) {
        if (depth == digits.Length) {
            list.Add (letters.ToString ());
            return;
        }

        int num = (int) digits[depth] - (int)
        '2';
        char startChar = num == 6 ? 't' : num == 7 ? 'w' : (char) ('a' + num * 3);
        int limit = num > 4 ? num == 6 ? startChar + 3 : startChar + 4 : startChar + 3;
        for (char c = startChar; c < limit; c++) {
            // letters.Insert(depth, c);
            letters.Append (c);
            BackTrack (digits, depth + 1, list, letters);
            letters.Remove (depth, 1);
        }
    }

}