namespace MyFirstApp.Models
{
    public class SortViewModel
    {
        public SortState NameSort { get; }
        public SortState AgeSort { get; }
        public SortState CompanySort { get; }
        public SortState Current { get; }
        

        public SortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            AgeSort = sortOrder == SortState.AgeAsc ? SortState.AgeDesc : SortState.AgeAsc;
            CompanySort = sortOrder == SortState.CompanyAsc ? SortState.CompanyDesc : SortState.CompanyAsc;
            Current = sortOrder;         
        }
    }
}
