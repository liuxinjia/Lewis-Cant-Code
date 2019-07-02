using System;
using System.Collections.Generic;

public class Solution
{
    public bool HasCycle(ListNode head)
    {
        if (head == null) return false;

        var rabbit = head;
        var turtle = head;
        while (rabbit.next != null && rabbit.next.next != null)
        {
            rabbit = rabbit.next.next;
            turtle = turtle.next;
            if (turtle == rabbit)
                return true;
        }
        return false;
    }
}