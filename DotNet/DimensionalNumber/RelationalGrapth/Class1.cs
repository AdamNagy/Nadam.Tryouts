namespace RelationalGrapth
{
    public class MessageGraphNode
    {
        public int PersonA { get; set; }
        public int PersonB { get; set; }
        public string Message { get; set; } = "";
    }

    public enum GraphNodeRelationType
    {
        Message, Favourite, Seen
    }

    public class Person
    {

    }
}