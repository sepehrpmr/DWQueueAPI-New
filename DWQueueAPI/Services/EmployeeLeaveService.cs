using DWQueue.Events;
using DWQueueAPI.Data;
using DWQueueAPI.Data.Entities;
//using DWQueueAPI.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace DWQueueAPI.Services
{
    public class EmployeeLeaveService
    {
        private readonly DWQueueContext _context;
        private readonly IPublishEndpoint _publishEndpoint;

        public EmployeeLeaveService(DWQueueContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<IEnumerable<EmployeeLeaves>> GetAllLeavesAsync()
        {
            return await _context.EmployeeLeaves.Include(l => l.Employee).ToListAsync();
        }

        public async Task<EmployeeLeaves> GetLeaveByIdAsync(int id)
        {
            return await _context.EmployeeLeaves.Include(l => l.Employee).FirstOrDefaultAsync(l => l.LeaveId == id);
        }

        public async Task<EmployeeLeaves> AddLeaveAsync(EmployeeLeaves leave)
        {
            // ۱. ذخیره مرخصی در دیتابیس
            _context.EmployeeLeaves.Add(leave);
            await _context.SaveChangesAsync();

            // ۲. لود کردن اطلاعات کارمند متصل به این مرخصی برای دسترسی به نام و ایمیل واقعی
            await _context.Entry(leave).Reference(l => l.Employee).LoadAsync();

            // ۳. ساخت آبجکت پیام برای ارسال به صف MassTransit
            var leaveEvent = new LeaveApprovedEvent
            {
                
                LeaveId = leave.LeaveId,
                EmployeeName = leave.Employee?.Name ?? "نام نامشخص",
                EmployeeEmail = "employee@test.com",
                StartDate = leave.StartDate,
                EndDate = leave.EndDate
            };

            // ۴. ارسال شیک و آسنکرون پیام به رابیت‌ام‌کیو توسط MassTransit
            await _publishEndpoint.Publish(leaveEvent);

            return leave;
        }

        public async Task<EmployeeLeaves> UpdateLeaveAsync(EmployeeLeaves leave)
        {
            _context.EmployeeLeaves.Update(leave);
            await _context.SaveChangesAsync();

            return leave;
        }

        public async Task<bool> DeleteLeaveAsync(int id)
        {
            var leave = await _context.EmployeeLeaves.FindAsync(id);
            if (leave == null)
            {
                return false;
            }
            _context.EmployeeLeaves.Remove(leave);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}


//using DWQueueAPI.Data;
//using DWQueueAPI.Data.Entities;
//using DWQueueAPI.Events;
//using MassTransit;

////using DWQueueAPI.Interfaces;
//using Microsoft.EntityFrameworkCore;

//namespace DWQueueAPI.Services
//{
//    public class EmployeeLeaveService
//    {
//        private readonly DWQueueContext _context;
//        private readonly IPublishEndpoint _publishEndpoint;
//        //private readonly IMessageService _messageService;

//        public EmployeeLeaveService(DWQueueContext context, IPublishEndpoint publishEndpoint  /*IMessageService messageService*/)
//        {
//            _context = context;
//            _publishEndpoint = publishEndpoint;
//            //_messageService = messageService;
//        }

//        public async Task<IEnumerable<EmployeeLeaves>> GetAllLeavesAsync()
//        {
//            return await _context.EmployeeLeaves.Include(l => l.Employee).ToListAsync();
//        }

//        public async Task<EmployeeLeaves> GetLeaveByIdAsync(int id)
//        {
//            return await _context.EmployeeLeaves.Include(l => l.Employee).FirstOrDefaultAsync(l => l.LeaveId == id);

//        }

//        public async Task<EmployeeLeaves> AddLeaveAsync(EmployeeLeaves leave)
//        {
//            _context.EmployeeLeaves.Add(leave);
//            await _context.SaveChangesAsync();

//            var leaveEvent = new LeaveApprovedEvent
//            {
//                LeaveId = leave.LeaveId, // آیدی مرخصی که تازه در دیتابیس ثبت شده
//                EmployeeName = "نام کارمند", // این رو می‌تونی از روی اطلاعات دیتابیس پر کنی
//                EmployeeEmail = "test@yourcompany.com", // فعلاً یک ایمیل تستی بذار
//                StartDate = leave.StartDate,
//                EndDate = leave.EndDate
//            };
//            //_messageService.PublishMessage("leave-approved-queue", leaveEvent);


//            await _context.Entry(leave).Reference(l => l.Employee).LoadAsync();
//            return leave;
//        }
//        public async Task<EmployeeLeaves> UpdateLeaveAsync(EmployeeLeaves leave)
//        {
//            _context.EmployeeLeaves.Update(leave);
//            await _context.SaveChangesAsync();


//            return leave;
//        }

//        public async Task<bool> DeleteLeaveAsync(int id)
//        {
//            var leave = await _context.EmployeeLeaves.FindAsync(id);
//            if (leave == null)
//            {
//                return false;
//            }
//            _context.EmployeeLeaves.Remove(leave);
//            await _context.SaveChangesAsync();
//            return true;
//        }

//    }
//}
