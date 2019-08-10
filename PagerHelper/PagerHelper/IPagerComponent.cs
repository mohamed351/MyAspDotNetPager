namespace PagerHelper
{
    public interface IPagerComponent
    {
        int Index { get; }
        int Size { get; }
        int NumberOfPage { get; }
        int TotalOfPage { get; }
        int TotalOfPageBaseOnSearch { get; set; }
    }
}
