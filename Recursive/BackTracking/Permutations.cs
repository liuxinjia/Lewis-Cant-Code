public class Solution {
    public IList<IList<int>> Permute(int[] nums) {
        List<IList<int>> rLists = new List<IList<int>>();
        BackTrack(nums, rLists, new HashSet<int>());
        return rLists;
    }
    
    void BackTrack(int[] nums,List<IList<int>> rLists, HashSet<int> set){
        if(set.Count == nums.Length) {
            rLists.Add(set.ToList());
            return;
        }
        
        for(int i=0; i<nums.Length; i++){
            if(!set.Contains(nums[i])){
                set.Add(nums[i]);
                BackTrack(nums, rLists, set);
                set.Remove(nums[i]);
            }
        }
    }
}