using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class UniqueIdentifiers
    {
        public static SortedSet<int> IdSet = new SortedSet<int>();

        public bool AddIdentifier(int Id)
        {
            if (!IdSet.Contains(Id))
            {
                IdSet.Add(Id);
                return true;
            }
            return false;
        }
    }
}
