public class Solution {

    public IList<int> DiffWaysToCompute (string input) {
        Dictionary<string, List<int>> map = new Dictionary<string, List<int>> ();
        return HelperDiff (input, map);
    }

    public List<int> HelperDiff (string input, Dictionary<string, List<int>> map) {
        var list = new List<int> ();

        if (map.ContainsKey (input)) return map[input];

        for (int i = 0; i < input.Length; i++) {
            char cur = input[i];
            if (input[i] < '0' || input[i] > '9') {
                List<int> left = HelperDiff (input.Substring (0, i), map);
                List<int> right = HelperDiff (input.Substring (i + 1), map);

                foreach (var item1 in left) {
                    foreach (var item2 in right) {
                        int temp = 0;
                        switch (input[i]) {
                            case '+':
                                temp = item1 + item2;
                                break;
                            case '-':
                                temp = item1 - item2;
                                break;
                            case '*':
                                temp = item1 * item2;
                                break;
                        }
                        list.Add (temp);
                    }

                }
            }
        }

        int pNum = 0;
        if (list.Count == 0 && Int32.TryParse (input, out pNum))
            list.Add (pNum);

        map.Add (input, list);
        return list;
    }

    void FindSubList (Dictionary<string, List<int>> map, List<int> list, string str) {
        if (!map.ContainsKey (str)) {
            list.AddRange (HelperDiff (str, map));
            map.Add (str, list);
        } else
            list.AddRange (map[str]);
    }

}