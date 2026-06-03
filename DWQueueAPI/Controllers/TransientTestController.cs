//using DWQueueAPI.Interfaces;
//using Microsoft.AspNetCore.Mvc;

//namespace DWQueueAPI.Controllers;

//[Route("api/[controller]")]
//[ApiController]
//public class TransientTestController : ControllerBase
//{
//    private readonly IMessageService _messageService;

//    public TransientTestController(IMessageService messageService)
//    {
//        _messageService = messageService;
//    }

//    [HttpGet]
//    public IActionResult Get()
//    {
//        var result = _messageService.GetHello();
//        return Ok(new
//        {
//            Message = result,
//            Lifetime = "Transient",
//            Description = "هر بار که این صفحه را رفرش کنید، یک نمونه جدید ساخته می‌شود."
//        });
//    }
//}
