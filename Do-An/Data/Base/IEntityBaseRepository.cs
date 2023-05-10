using Do_An.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Do_An.Data.Base
{
    /// <summary>
    /// Áp dụng Repository Pattern
    /// Interface này định nghĩa các thao tác cơ bản CRUD.
    /// C - Create,
    /// R - Read,
    /// U - Update,
    /// D - Delete
    /// </summary>
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        /// <summary>
        /// Lấy toàn bộ dữ liệu của một table trong db
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync();
        /// <summary>
        /// Lấy toàn bộ dữ liệu của một table trong db
        /// Có bao gồm khoá ngoại từ table khác
        /// </summary>
        /// <param name="includeProperties">Property là khóa ngoại của table trong db</param>
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        /// <summary>
        /// Lấy 1 record từ table trong db
        /// </summary>
        /// <param name="id">Khóa chính</param>
        Task<T> GetByIdAsync(int id);
        /// <summary>
        /// Thêm 1 record vào db
        /// </summary>
        Task AddAsync(T entity);
        /// <summary>
        /// Cập nhật 1 record vào db
        /// </summary>
        /// <param name="id">Id của record cần cập nhật</param>
        Task UpdateAsync(int id, T entity);
        /// <summary>
        /// Xóa 1 record trong db
        /// </summary>
        /// <param name="id">Id của record cần delete</param>
        Task DeleteAsync(int id);
    }
}
