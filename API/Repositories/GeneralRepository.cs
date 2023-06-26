using API.Models;
using API.Contracts;
using API.Data;


namespace API.Repositories
{
    public class GeneralRepository<TEntity> : IGeneralRepository<TEntity> where TEntity : class
    {
        protected readonly BookingDbContext context;   // Mengijeksikan kelas BookingDbContext.
        public GeneralRepository(BookingDbContext Context)   // Konstruktor kelas GeneralRepository yang menerima satu parameter dengan tipe data BookingDbContext yang diberi nama Context.
        {
            this.context = Context;
        }


        public ICollection<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();
        }


        public TEntity? GetByGuid(Guid guid)
        {
            var entity = context.Set<TEntity>().Find(guid);
            context.ChangeTracker.Clear();
            return entity;
            /*return context.Set<TEntity>().Find(guid);*/
        }

        /*public TEntity? GetByName(string name)
        {
            return context.Set<TEntity>().Find(name);
        }*/


        public TEntity? Create(TEntity entity)
        {
            try
            {
                context.Set<TEntity>().Add(entity);
                context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public bool Update(TEntity entity)
        {
            try
            {
                context.Set<TEntity>().Update(entity);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool Delete(Guid guid)
        {
            try
            {
                var entity = GetByGuid(guid);

                if (entity is null)
                {
                    return false;
                }
                context.Set<TEntity>().Remove(entity);
                context.SaveChanges();   // Menyimpan perubahan yang dilakukan ke dalam database.
                return true;
            }
            catch (Exception ex)   // Menangkap pengecualian jika terjadi kesalahan saat menghapus.
            {
                return false;   // Mengembalikan false untuk menandakan bahwa penghapusan gagal.
            }
        }

    }
}
