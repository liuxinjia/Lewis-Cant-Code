public class Solution {
    public int[] FindOrder (int numCourses, int[][] prerequisites) {
        List<List<int>> map = new List<List<int>> (numCourses);
        for (int i = 0; i < numCourses; i++) {
            map.Add (new List<int> (numCourses));
        }

        int[] degrees = new int[numCourses];
        for (int i = 0; i < prerequisites.Length; i++) {
            map[prerequisites[i][1]].Add (prerequisites[i][0]);
            degrees[prerequisites[i][0]]++;
        }

        var stack = new Queue<int> (numCourses);
        for (int i = 0; i < numCourses; i++) {
            if (degrees[i] == 0) stack.Enqueue (i);
        }

        var pairList = new List<int> (numCourses);
        while (stack.Count > 0) {
            // it is possible to have multiple correct route,
            // but we only need to return one of them.
            var top = stack.Dequeue ();
            pairList.Add (top);
            for (int i = 0; i < map[top].Count; i++) {
                if (--degrees[map[top][i]] == 0) stack.Enqueue (map[top][i]);
            }
        }

        return pairList.Count == numCourses ? pairList.ToArray () : new int[0];
    }

}