namespace PL.Models.SearchViewModels
{
    public class SortViewModel
    {
        public SortState NameSort { get; set; }
        public SortState AuthorSort { get; set; }
        public SortState GenreSort { get; set; }
        public SortState Current { get; set; }
        public SortViewModel(SortState sortOrder)
        {
            NameSort = SortState.NameAsc;
            AuthorSort = SortState.AuthorAsc;
            GenreSort = SortState.GenreAsc;

            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            AuthorSort = sortOrder == SortState.AuthorAsc ? SortState.AuthorDesc : SortState.AuthorAsc;
            GenreSort = sortOrder == SortState.AuthorAsc ? SortState.AuthorDesc : SortState.AuthorAsc;
            Current = sortOrder;
        }
    }
}
