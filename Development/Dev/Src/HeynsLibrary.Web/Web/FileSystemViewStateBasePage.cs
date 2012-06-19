using System;
using System.Linq;
using System.Diagnostics;

namespace HeynsLibrary.Web
{
    public class FileSystemViewStateBasePage : System.Web.UI.Page
    {
        protected override System.Web.UI.PageStatePersister PageStatePersister
        {
            get
            {
                Debugger.Break();
                return new FileSystemPageStatePersister(this);
            }
        }
    }
}
