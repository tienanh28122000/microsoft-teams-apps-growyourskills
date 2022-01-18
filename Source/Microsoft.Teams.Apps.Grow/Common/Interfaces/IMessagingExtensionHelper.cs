// <copyright file="IMessagingExtensionHelper.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.Teams.Apps.Grow.Common.Interfaces
{
    using System.Threading.Tasks;
    using Microsoft.Bot.Schema.Teams;

    /// <summary>
    /// Giao diện xử lý các hoạt động tìm kiếm cho Tiện ích nhắn tin.
    /// </summary>
    public interface IMessagingExtensionHelper
    {
        /// <summary>
        /// Nhận kết quả bằng cách sử dụng truy vấn tìm kiếm và điền kết quả (thẻ + bản xem trước).
        /// </summary>
        /// <param name="query">Query which the user had typed in Messaging Extension search field.</param>
        /// <param name="commandId">Command id to determine which tab in Messaging Extension has been invoked.</param>
        /// <param name="userObjectId">Azure Active Directory id of the user.</param>
        /// <param name="count">Number of search results to return.</param>
        /// <param name="skip">Number of search results to skip.</param>
        /// <returns><see cref="Task"/>Returns Messaging Extension result object, which will be used for providing the card.</returns>
        Task<MessagingExtensionResult> GetProjectSearchResultAsync(
            string query,
            string commandId,
            string userObjectId,
            int? count,
            int? skip);

        /// <summary>
        /// Nhận giá trị của tham số searchText trong truy vấn Tiện ích nhắn tin.
        /// </summary>
        /// <param name="query">Contains Messaging Extension query keywords.</param>
        /// <returns>A value of the searchText parameter.</returns>
        string GetSearchQueryString(MessagingExtensionQuery query);
    }
}
