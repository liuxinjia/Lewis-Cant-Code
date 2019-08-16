public class Solution {
    public IList<int> GetRow (int rowIndex) {
        List<int> list = new List<int> ();
        if (rowIndex < 0) return list;
        list.Add (1);
        if (rowIndex == 0) return list;

        var topList = GetRow (rowIndex - 1);
        for (int i = 1; i < topList.Count; i++) {
            list.Add (topList[i - 1] + topList[i]);
        }
        list.Add (1);
        return list;

    }

}