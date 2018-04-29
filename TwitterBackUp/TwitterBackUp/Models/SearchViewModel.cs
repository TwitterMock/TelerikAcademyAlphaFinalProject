namespace TwitterBackUp.Models
{
    public class SearchViewModel
    {
        public bool IsSavedTwitter { get; set; }

        public bool IsSuccess { get; set; }

        public string SearchString { get; set; }
        
        public TwitterViewModel SearchedTwitter { get; set; }
    }
}