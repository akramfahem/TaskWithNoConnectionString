using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using APII.Data;
using APII.Model;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APII.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoggerController : Controller
{
    private LoggerRepositry logger = LoggerRepositry.GetLogger;
    [HttpGet("{user}/{logType}")]
    public async Task<ActionResult<(DateTime From, DateTime To)>> GetDate(string user, string logType)
    {
        LogType logTypeEnum = (LogType)Enum.Parse(typeof(LogType), logType, true);

        return Ok(logger.GetDate(user, logTypeEnum));

    }
    [HttpGet("{user}/{year:int}/{month:int}/{day:int}/{logtype}/{CurrentPage:int?}")]
    public async Task<ActionResult<IEnumerable<LogMessage>>> Get(string user, int year, int month, int day, string logtype, int? CurrentPage)
    {
        logtype = logtype.ToUpper();
        if (CurrentPage is null || CurrentPage == 0)
            CurrentPage = 1;

        if (Enum.TryParse<LogType>(logtype, out LogType logTypeenum))
            return Ok(await logger.GetLogs(user, logTypeenum, year,month,day,CurrentPage));
        else
            return NotFound();

    }
   

    [HttpPost("{user}")]
    public async Task<ActionResult> Add(string user,[FromBody] LogMessage logMessage)
    {
        try
        {
            await logger.Logging(user,logMessage.LogType, logMessage.Message!);
        }
        catch (Exception ex)
        {

            return StatusCode((int)HttpStatusCode.InternalServerError);

        }

        return Created("Log is Added", logMessage);
    }
    
    [HttpGet("{user}/{year:int}/{month:int}/{day:int}/{logType}")]
    public async Task<ActionResult<int>>GetDirectoryCount(string user, int year, int month, int day, LogType logType)
    {
        return Ok(logger.GetDirectoryCount(user, logType, year, month, day));
    }
    [HttpGet]
    public async Task<ActionResult<List<string>>>GetUsers()
    {
        return Ok(logger.GetUsers());
    }
}

