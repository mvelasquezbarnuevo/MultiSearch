namespace MultiSearch.Common.Handlers
{
    public interface IValidation
    {
        bool IsValid<T>(T obj);
    }
}
