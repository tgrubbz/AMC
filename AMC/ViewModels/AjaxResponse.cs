namespace AMC.WEB.ViewModels
{
    public class AjaxResponse
    {
        public string Error { get; set; }
        public object Data { get; set; }

        public AjaxResponse()
        {
            Error = null;
        }
    }
}
