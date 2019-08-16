/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
 */
public class Solution {

    //iteration solution
    //Time: O(n) : Space Complexity: O(1);
    // fuck you linked list
    public ListNode ReverseList (ListNode head) {
        ListNode prev = null;
        var cur = head;
        while (cur != null) {
            var temp = cur.next;
            cur.next = prev;
            prev = cur;
            cur = temp;

        }

        return prev;
    }

    //Recursion solution
    //Time: O(n) : Space Complexity: O(1);

    //Attention!!! don't get stuck in a circle.
    public class Solution {
        public ListNode ReverseList (ListNode head) {
            if (head == null || head.next == null) return head;

            var tail = ReverseList (head.next);
            head.next.next = head;
            head.next = null;
            return tail;
        }
    }
}