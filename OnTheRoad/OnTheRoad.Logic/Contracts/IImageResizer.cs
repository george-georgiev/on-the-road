namespace OnTheRoad.Logic.Contracts
{
    public interface IImageResizer
    {
        byte[] ResizeImage(object fileStream);
    }
}
