// <copyright file="AcquiredSkillController.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.Teams.Apps.Grow.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.ApplicationInsights;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.CodeAnalysis;
    using Microsoft.Extensions.Logging;
    using Microsoft.Teams.Apps.Grow.Common.Interfaces;
    using Microsoft.Teams.Apps.Grow.Models;

    /// <summary>
    /// Controller để xử lý các hoạt động API kỹ năng có được.
    /// </summary>
    [Route("api/acquiredskill")]
    [ApiController]
    [Authorize]
    public class AcquiredSkillController : BaseGrowController
    {
        /// <summary>
        /// Ghi lỗi và thông tin.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Trường hợp của nhà cung cấp lưu trữ kỹ năng có được.
        /// </summary>
        private readonly IAcquiredSkillStorageProvider acquiredSkillStorageProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="AcquiredSkillController"/> class.
        /// </summary>
        /// <param name="logger">Ghi lỗi và thông tin.</param>
        /// <param name="telemetryClient">The Application Insights telemetry client.</param>
        /// <param name="acquiredSkillStorageProvider">Acquired skill storage provider dependency injection.</param>
        public AcquiredSkillController(
            ILogger<AcquiredSkillController> logger,
            TelemetryClient telemetryClient,
            IAcquiredSkillStorageProvider acquiredSkillStorageProvider)
            : base(telemetryClient)
        {
            this.logger = logger;
            this.acquiredSkillStorageProvider = acquiredSkillStorageProvider;
        }

        /// <summary>
        /// Nhận cuộc gọi để truy xuất danh sách các kỹ năng mà người dùng có được.
        /// Người dùng sẽ có thể xem các kỹ năng có được cho tất cả các dự án có trạng thái là đã đóng.
        /// </summary>
        /// <returns>List of joined projects which are in closed state.</returns>
        [HttpGet("acquired-skills")]
        public async Task<IActionResult> GetAcquiredSkillsAsync()
        {
            try
            {
                this.RecordEvent("Acquired skills - HTTP Get call initiated");

                // Nhận các kỹ năng có được dựa trên id người dùng.
                var acquiredSkills = await this.acquiredSkillStorageProvider.GetAcquiredSkillsAsync(this.UserAadId);
                this.RecordEvent("Acquired skills - HTTP Get call succeeded");

                if (acquiredSkills == null || !acquiredSkills.Any())
                {
                    this.logger.LogInformation($"No acquired skills found for user {this.UserAadId}.");
                    return this.Ok(new List<AcquiredSkillsEntity>());
                }

                return this.Ok(acquiredSkills.OrderByDescending(skill => skill.ProjectClosedDate));
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error while fetching acquired skills for user {this.UserAadId}.");
                throw;
            }
        }
    }
}