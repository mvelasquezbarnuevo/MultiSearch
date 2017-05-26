namespace MultiSearch.Api.Models
{
    public class ApiSearchWord
    {
        public string Word { get; set; }
        public long? Count { get; set; }
        public bool? IsWinner { get; set; }
    }
}