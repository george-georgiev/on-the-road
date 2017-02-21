namespace OnTheRoad.Logic.Contracts
{
    public interface IImageResizer
    {
        byte[] ResizeImage(byte[] fileStream);
    }
}
