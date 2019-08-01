using System.Collections.Generic;

public class Solution {
    public int[] Intersect (int[] nums1, int[] nums2) {
        Dictionary<int, int> map = new Dictionary<int, int> ();
        List<int> rList = new List<int> ();

        for (int i = 0; i < nums.Length; i++)
            map.Add (i, nums[i]);

        for (int i = 0; i < nums.Length; i++) {
            if (map.ContainsValue (nums[i])) {
                int removeIndex = -1;
                foreach (var pair in map)
                    if (pair.Value == nums[i]) { removeIndex = pair.Key; break; };
                if (removeIndex != -1) map.Remove (removeIndex);
                rList.Add (nums[i]);
            }
        }

        return rList.ToArray ();
    }

}

//Follow Up:
// 1.What if the given array is already sorted? How would you optimize your algorithm?
// -- -- -- -- -- -- -- -- -- -- -- -- --
// Comparing the sorted list , if both of them in different list is the same theen push it to the rList. 
// But if list1's element is smaller than list2's element , we will access next list1 element until the end one of them.
public int[] Intersect (int[] nums1, int[] nums2) {
    List<int> rList = new List<int> ();

    Array.Sort (nums1);
    Array.Sort (nums2);

    int i = 0, j = 0;
    while (i < nums1.Length && j < nums2.Length) {
        if (nums1[i] == nums2[j]) {
            rList.Add (nums1[i]);
            i++;
            j++;
        } else if (nums1[i] > nums2[j]) {
            j++;
        } else i++;
    }

    return rList;
}
// 2. What if nums1's size is small compared to nums2's size? Which algorithm is better?
// -- -- -- -- -- -- -- -- -- -- -- -- --
// the build-in hashmap search and add spend o(1) time, and search costs o(logn) time in worst case but add still keep the same.
// So in the build-map process, it cost O(m) ~ O(m*logm) time in total;
// Search and Compare in array cost o(n) time 
// So I will choose the samller array as build-map's parameter

public int[] Intersect (int[] nums1, int[] nums2) {
    Dictionary<int, int> map = new Dictionary<int, int> ();
    List<int> rList = new List<int> ();

    if (nums1.Length > nums2.Length) {
        map = CreateMap (nums1);
        rList = CreateList (nums2, map);
    } else {
        map = CreateMap (nums2);
        rList = CreateList (nums1, map);
    }
    return rList.ToArray ();
}

Dictionary<int, int> CreateMap (int[] nums) {
    Dictionary<int, int> map = new Dictionary<int, int> ();

    for (int i = 0; i < nums.Length; i++)
        if (!map.TryAdd (nums[i], 1))
            if (map.ContainsKey (nums[i]))
                map[nums[i]]++;

    return map;
}

List<int> CreateList (int[] nums, Dictionary<int, int> map) {
    List<int> rList = new List<int> ();

    for (int i = 0; i < nums.Length; i++) {
        if (map.ContainsKey (nums[i]) && map[nums[i]] != 0) {
            map[nums[i]]--;
            rList.Add (nums[i]);
        }
    }
    return rList;
}


// 3.What if elements of nums2 are stored on disk, and the memory is limited such that you cannot load all elements into the memory at once?
// -- -- -- -- -- -- -- -- -- -- -- -- --