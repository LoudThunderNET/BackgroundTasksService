namespace BackgroundTasksService.AppServices.Services
{
    public interface ITypeMapper
    {
        TDest Map<TSource, TDest>(TSource source);
        void Map<TSource, TDest>(TSource source, TDest dest);
    }
}
