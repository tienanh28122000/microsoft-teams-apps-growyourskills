// <copyright file="IAcquiredSkillStorageProvider.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.Teams.Apps.Grow.Common.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Teams.Apps.Grow.Models;

    /// <summary>
    /// Giao diện dành cho nhà cung cấp giúp lấy, lưu trữ hoặc cập nhật thông tin chi tiết về kỹ năng có được.
    /// </summary>
    public interface IAcquiredSkillStorageProvider
    {
        /// <summary>
        /// Lưu trữ hoặc cập nhật dữ liệu kỹ năng có được.
        /// </summary>
        /// <param name="entity">Holds acquired skill detail.</param>
        /// <returns>A task that represents acquired skill is saved or updated.</returns>
        Task<bool> UpsertAcquiredSkillAsync(AcquiredSkillsEntity entity);

        /// <summary>
        /// Nhận các kỹ năng có được của một người dùng.
        /// </summary>
        /// <param name="userId">Azure Active Directory id of user.</param>
        /// <returns>A task that represents a collection of acquired skills.</returns>
        Task<IEnumerable<AcquiredSkillsEntity>> GetAcquiredSkillsAsync(string userId);
    }
}
