/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
 */
public class Solution {
    public ListNode MergeTwoLists (ListNode l1, ListNode l2) {
        if (l1 == null) return l2;
        if (l2 == null) return l1;

        var cur = new ListNode (-1);
        var head = cur;
        cur.next = l1;

        while (l1 != null && l2 != null) {
            if (l1.val < l2.val)
                l1 = l1.next;
            else {
                var prv = cur.next;
                cur.next = l2;
                var nxt = l2.next;
                l2.next = prv;
                l2 = nxt;
            }
            cur = cur.next;
        }
        cur.next = l1 == null ? l2 : l1;
        return head.next;
    }
}