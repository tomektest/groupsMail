﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class MyLogEvents
    {
        #region Groups
        public const int GetAll = 1000;
        public const int Delete = 1001;
        public const int GetItem = 1002;
        public const int InsertItem = 1003;
        public const int UpdateItem = 1004;
        public const int DeleteItem = 1005;

        public const int TestItem = 3000;

        public const int GetItemNotFound = 4000;
        public const int UpdateItemNotFound = 4001;
        #endregion Groups
    }
}
