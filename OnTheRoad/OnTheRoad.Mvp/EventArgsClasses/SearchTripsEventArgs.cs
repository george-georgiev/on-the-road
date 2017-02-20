namespace OnTheRoad.Mvp.EventArgsClasses
{
    public class SearchTripsEventArgs : GetTripsEventArgs
    {
        public string SearchPattern { get; set; }
    }
}
