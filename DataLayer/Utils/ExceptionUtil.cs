using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Utils
{
    public class UnknownException : Exception
    {
        public UnknownException() : base(String.Format("There was an unknown issue")) { }
    }
    public class QueryException : Exception
    {
        public QueryException() : base(String.Format("There was an issue while querying")) { }
    }
    public class InsertException : Exception
    {
        public InsertException() : base(String.Format("There was an issue while inserting")) { }
    }
    public class DeleteException : Exception
    {
        public DeleteException() : base(String.Format("There was an issue while deleting")) { }
    }
    public class UpdateException : Exception
    {
        public UpdateException() : base(String.Format("There was an issue while updating")) { }
    }
}
