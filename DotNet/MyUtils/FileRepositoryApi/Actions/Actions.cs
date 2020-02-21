namespace ManifestRepositoryApi.Actions
{
    public class ActionWithPayload
    {
        public string action;
        public string payload;
    }

    public class ActionResult
    {
        public readonly ActionWithPayload action;

        public bool isSuccess;
        public string message;

        public ActionResult(ActionWithPayload _action)
        {
            action = _action;
        }
    }
}