namespace BookStore.Models.Repos
{
    public interface IBookStoreRepository<TEntity>
    {
        IList<TEntity> GetAll();
        TEntity Find(int id);
        void Add(TEntity entity);
        void Update(int id , TEntity entity);
        void Delete(int id);
        public List<TEntity> Search(string term);
    }
}
