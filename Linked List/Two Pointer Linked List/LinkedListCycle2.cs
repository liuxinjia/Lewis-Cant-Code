using System;

public class Solution
{
    public ListNode DetectCycle(ListNode head)
    {
        if (head == null || head.next == null) return null;
        var rabbit = head;
        var turtle = head;

        while (rabbit != null && rabbit.next != null)
        {
            rabbit = rabbit.next.next;
            turtle = turtle.next;

            if (rabbit == turtle) 
            {
                var entry = head;
                while(entry != turtle)
                {
                    entry = entry.next;
                    turtle = turtle.next;
                }
                return entry;
            }
        }
        return null;
    }
}