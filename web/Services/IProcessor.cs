namespace web.Services
{
    public interface IProcessor
    {
        string Result { get; set; }

        OperationResult Execute();
    }
}