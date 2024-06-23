using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using ExpenseTracker.Dto;
using ExpenseTracker.Enums;
using ExpenseTracker.IServices;
using ExpenseTracker.Models;
using ExpenseTracker.Roles.Dto;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Services
{
    public class NotificationAppService : ApplicationService, INotificationAppService
    {
        private readonly IRepository<Notifications> _repository;
        private readonly IObjectMapper _objectMapper;

        public NotificationAppService(IRepository<Notifications> repository, IObjectMapper objectMapper)
        {
            _repository = repository;
            _objectMapper = objectMapper;
        }

        public async Task<List<NotificationDTO>> GetNotifications()
        {
            try
            {
                var uId = AbpSession.UserId;
                if (uId == null)
                    return null;

                var notifications = await _repository.GetAllListAsync();
                return new List<NotificationDTO>(ObjectMapper.Map<List<NotificationDTO>>(notifications));
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
