using ExpenseTracker.Dto;
using ExpenseTracker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.IServices
{
    public interface INotificationAppService
    {
        Task<List<NotificationDTO>> GetNotifications();
    }
}
