namespace Nadam.ConsoleTest.Models
{
    public enum Type
    {
        Permanent,
        Visitor,
        Temporary
    }

    public enum State
    {
        Draft,
        Submitted,
        Assigned,
        Canceled,
        Closed,
        Rejected
    }

    public enum Filter
    {
        Rating,
        Color,
        Type,
        State,
        DownloadDate,
        Owner
    }
}
