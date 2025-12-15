using System;
using Microsoft.EntityFrameworkCore; 
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq; 

namespace AdvanceCRM.Web.EF.Models
{
    public class AdvanceCRMContext : DbContext
    {
        public AdvanceCRMContext(DbContextOptions<AdvanceCRMContext> options) : base(options)
        {
        }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Exceptions> Exceptions { get; set; } 
        public DbSet<MessageLog> MessageLogs { get; set; }  
        public DbSet<User> Users { get; set; }

            public DbSet<UserPermission> UserPermissions { get; set; }
            public DbSet<Role> Roles { get; set; }
            public DbSet<RolePermission> RolePermissions { get; set; }
            public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserPreference> UserPreferences { get; set; }
        //AuditLog
        public DbSet<AuditLog> AuditLog { get; set; }
        //ExcelImportTemplate
        public DbSet<ExcelImportTemplate> ExcelImportTemplates { get; set; }
        //ExcelImport
        public DbSet<ExcelImport> ExcelImports { get; set; }
        //SapDatabase
        public DbSet<SapDatabase> SapDatabases { get; set; }
        //Country
        public DbSet<Country> Country { get; set; }
        //City
        public DbSet<City> City { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

             base.OnModelCreating(modelBuilder);
           

            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = 1,
                Username = "admin",
                DisplayName = "admin",
                Email = "admin@dummy.com",
                Source = "site",
                Company = 1,
                PasswordHash = "rfqpSPYs0ekFlPyvIRTXsdhE/qrTHFF+kKsAUla7pFkXL4BgLGlTe89GDX5DBysenMDj8AqbIZPybqvusyCjwQ",
                PasswordSalt = "hJf_F",
                InsertDate = new DateTime(2014, 1, 1),  
                InsertUserId = 1,
                IsActive = 1
            });

            modelBuilder.Entity<Language>().HasData(
                new Language {Id = 1, LanguageId = "en", LanguageName = "English" },
                new Language { Id =2 , LanguageId = "ru", LanguageName = "Russian" },
                new Language { Id = 3, LanguageId = "es", LanguageName = "Spanish" },
                new Language { Id = 4, LanguageId = "tr", LanguageName = "Turkish" },
                new Language { Id = 5, LanguageId = "de", LanguageName = "German" },
                new Language { Id = 6, LanguageId = "zh-CN", LanguageName = "Chinese (Simplified)" },
                new Language { Id = 7, LanguageId = "it", LanguageName = "Italian" },
                new Language { Id = 8, LanguageId = "pt", LanguageName = "Portuguese" },
                new Language { Id = 9, LanguageId = "pt-BR", LanguageName = "Portuguese (Brazil)" },
                new Language { Id = 10, LanguageId = "fa", LanguageName = "Farsi" },
                new Language { Id = 11, LanguageId = "vi-VN", LanguageName = "Vietnamese (Vietnam)" }
            );  

            }

        }

    
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AdvanceCRMContext _context;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(AdvanceCRMContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories.Keys.Contains(typeof(TEntity)))
                return _repositories[typeof(TEntity)] as IRepository<TEntity>;

            var repository = new Repository<TEntity>(_context);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            _repositories = null;
        }
    }

    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        public Repository(DbContext context)
        {
            Context = context;
        }

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }
    }

}
