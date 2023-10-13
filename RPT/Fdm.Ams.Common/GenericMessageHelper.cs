namespace Fdm.Ams.Common
{
    public class GenericMessageHelper : IGenericMessageHelper
    {
        public string CreateGenericConflictMessage(string apiUrl)
        {
            var conflictedMessage = $"[Conflicted] This {apiUrl.Substring(apiUrl.LastIndexOf("/") + 1)} is assigned to one of the functionality and cannot be deleted.";
            return conflictedMessage;
        }
    }
}