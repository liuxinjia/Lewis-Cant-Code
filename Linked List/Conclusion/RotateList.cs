//  * Definition for singly-linked list.
//  * public class ListNode {
//  *     public int val;
//  *     public ListNode next;
//  *     public ListNode(int x) { val = x; }
//  * }

public class Solution {
    public ListNode RotateRight (ListNode head, int k) {
        if (head == null) return null;
        if (k == 0 ) return head;
        
        int length = 0;
        var iter = head;
        while (iter != null) {
            iter = iter.next;
            length++;
        }

        int N = k % (length);
        if (N == 0) return head;
        return RotateN (head, length - N - 1);
    }

    ListNode RotateN (ListNode head, int N) {
        ListNode fHead = head.next;
        ListNode slice = head;

        for (int i = 0; i < N; i++) {
            slice = slice.next;

            if (i == N - 1)
                fHead = slice.next;
        }
        slice.next = null;

        ListNode sHead = fHead;
        while (sHead.next != null) {
            sHead = sHead.next;
        }
        sHead.next = head;

        return fHead;
    }
}