using System.Collections.Generic;

namespace Identity.Test.Core.Model
{
    public class ErrorResponse
    {
        public string Title { get; set; }
        public Dictionary<string, List<string>> Errors { get; set; }
    }
}
