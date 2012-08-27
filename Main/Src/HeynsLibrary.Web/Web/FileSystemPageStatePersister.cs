using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace HeynsLibrary.Web
{
    public class FileSystemPageStatePersister : PageStatePersister
    {
        private const string _viewstateFormFieldId = "__SKM_VIEWSTATEID";
        private const string _stateFileFolderPath = "~/App_Data/StateFiles";


        public FileSystemPageStatePersister(Page page)
            : base(page)
        {

        }

        public override void Load()
        {
            var stateIdentifierValue = HttpContext.Current.Request.Form[_viewstateFormFieldId];
            if (stateIdentifierValue.Length > 0)
            {
                var fileName = stateIdentifierValue;
                var filePath = Path.Combine(HttpContext.Current.Server.MapPath(_stateFileFolderPath), fileName);
                var contents = File.ReadAllText(filePath);

                var state = base.StateFormatter.Deserialize(contents) as Pair;
                if (state != null)
                {
                    base.ViewState = state.First;
                    base.ControlState = state.Second;
                }
            }
        }

        public override void Save()
        {
            if (base.Page.Form != null && (base.ControlState != null || base.ViewState != null))
            {
                var fileName = string.Concat(DateTime.Now.Ticks, 16, "-", HttpContext.Current.Session.SessionID, ".vs");
                var filePath = Path.Combine(HttpContext.Current.Server.MapPath(_stateFileFolderPath), fileName);
                var pair = new Pair(base.ViewState, base.ControlState);
                File.WriteAllText(filePath, base.StateFormatter.Serialize(pair));

                var stateField = string.Format(@"{0}<input type=""hidden"" name=""{1}"" value=""{2}"" />{0}{0}", System.Environment.NewLine, _viewstateFormFieldId, fileName);
                base.Page.Form.Controls.AddAt(0, new LiteralControl(stateField));
            }
        }
    }
}
