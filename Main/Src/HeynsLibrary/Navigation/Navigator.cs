namespace HeynsLibrary.Navigation
{
    public static class Navigator
    {
        private static INavigationWorkflow _navigationWorkflow;
        private static object _navigationArgument;

        public static void Attach(INavigationWorkflow workflow)
        {
            if (workflow != null)
                _navigationWorkflow = workflow;
        }

        public static object Argument
        {
            get { return _navigationArgument; }
        }

        public static void NavigateTo(string view)
        {
            if (_navigationWorkflow != null)
                _navigationWorkflow.NavigateTo(view);
        }

        public static void NavigateTo(string view, object argument)
        {
            if (_navigationWorkflow != null)
            {
                _navigationArgument = argument;
                Navigator.NavigateTo(view);
            }
        }
    }
}