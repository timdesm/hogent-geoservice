using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Utils
{
    public class ExceptionUtil
    {
        public class ExistException : Exception
        {
            public ExistException(String obj) : base(String.Format("That {0} alreayd exists", obj)) { }
        }

        public class AddException : Exception
        {
            public AddException(String obj) : base(String.Format("Could not add {0}", obj)) { }
        }
        public class FetchException : Exception
        {
            public FetchException(String obj) : base(String.Format("Could not fetch {0}", obj)) { }
        }
        public class UpdateException : Exception
        {
            public UpdateException(String obj) : base(String.Format("Could not update {0}", obj)) { }
        }
        public class DeleteReferenceException : Exception
        {
            public DeleteReferenceException(String obj, String destination) : base(String.Format("Cannot remove {0} because of reference with a {1}", obj, destination)) { }
        }
        public class DeleteException : Exception
        {
            public DeleteException(String obj) : base(String.Format("Could not delete {0}", obj)) { }
        }
    }
}
