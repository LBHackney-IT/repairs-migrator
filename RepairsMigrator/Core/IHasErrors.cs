using System.Collections.Generic;

namespace Core
{
    public interface IHasErrors
    {
        IList<string> Errors { get; set; }
    }
}