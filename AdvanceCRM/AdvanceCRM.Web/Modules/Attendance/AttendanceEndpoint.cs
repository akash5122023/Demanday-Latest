    
namespace AdvanceCRM.Attendance.Endpoints
{
    using AdvanceCRM.Attendance.Repositories;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System;    
    using System.Data;    
    

    using AdvanceCRM.Masters; // Add this namespace to use AttendanceTypeMaster

   // using SerenityAuth = Serenity.Authorization;

    using MyRepository = Repositories.AttendanceRepository;
    using MyRow =AttendanceRow;
    using AdvanceCRM.Administration;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Caching.Memory;
    using Serenity.Abstractions;

    [Route("Services/Attendance/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class AttendanceController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;
        private readonly IPermissionService permissionService;
        private readonly IRequestContext requestContext;
        private readonly IMemoryCache memoryCache;
        private readonly ITypeSource typeSource;
        private readonly IUserRetrieveService userRetriever;
        public AttendanceController(
           ISqlConnections connections,
           IPermissionService permissionService,
           IRequestContext requestContext,
           IMemoryCache memoryCache,
           ITypeSource typeSource,
           IUserRetrieveService userRetriever)
        {
           
            this.permissionService = permissionService;
            this.requestContext = requestContext;
            this.memoryCache = memoryCache;
            this.typeSource = typeSource;
            this.userRetriever = userRetriever;
            _connections = connections;
        }
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
           return new MyRepository(Context, _connections).Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
           return new MyRepository(Context, _connections).Update(uow, request);
        }

        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request)
        {
           return new MyRepository(Context, _connections).Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
             return new MyRepository(Context, _connections).Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request)
        {
            return new MyRepository(Context, _connections).List(connection, request);
        }



        [HttpPost, Authorize]
        public SaveResponse PunchIn(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            var userId = Context.User.GetIdentifier();
            var today = DateTime.Today;

            // Fetch the logged-in user's details
            var user = uow.Connection.TryFirst<UserRow>(
                UserRow.Fields.UserId == userId);

            if (user == null)
            {
                throw new ValidationError("User Not Found", "Could not retrieve user details.");
            }

            var existingPunch = uow.Connection.TryFirst<MyRow>(
                MyRow.Fields.UserId == userId & MyRow.Fields.PunchIn >= today);

            if (existingPunch != null)
            {
                throw new ValidationError("Already Punched In", "You have already punched in today.");
            }

            if (request.Entity == null || string.IsNullOrEmpty(request.Entity.Coordinates))
            {
                throw new ValidationError("Location Error", "Coordinates are required.");
            }

            var row = new MyRow
            {
                UserId = userId,
                PunchIn = DateTime.Now,
                PunchOut = DateTime.Now,
                DateNTime = DateTime.Now,
                Type = AttendanceTypeMaster.HalfDay,
                Coordinates = request.Entity.Coordinates,
                Location = request.Entity.Location ?? "Unknown",
                ApprovedBy = null,
                Name = user.UserId 
            };

            new MyRepository(Context, _connections).Create(uow, new SaveRequest<MyRow> { Entity = row });

            return new SaveResponse();
        }





        [HttpPost, Authorize]
        public SaveResponse PunchOut(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            var userId =Context.User.GetIdentifier().ToString();
            var today = DateTime.Today;

            // Find today's punch-in record for the user
            var existingPunch = uow.Connection.TryFirst<MyRow>(
                MyRow.Fields.UserId == userId & MyRow.Fields.PunchIn >= today);

            if (existingPunch == null || existingPunch.PunchIn == null)
            {
                throw new ValidationError("Punch-in Not Found", "You need to punch in before punching out.");
            }

            // Update PunchOut time
            existingPunch.PunchOut = DateTime.Now;
            existingPunch.DateNTime = DateTime.Now;

            // Calculate duration safely
            var duration = (existingPunch.PunchOut.Value - existingPunch.PunchIn.Value).TotalHours;

            // Set the Type based on duration
            existingPunch.Type = duration >= 4 ? AttendanceTypeMaster.Present : AttendanceTypeMaster.HalfDay;

            // Update the record in the database
            uow.Connection.UpdateById(existingPunch);

            return new SaveResponse();
        }








        [ServiceAuthorize("Attendance:Can Approve")]
        public StandardResponse Approve(StandardRequest request)
        {
            var response = new StandardResponse();

            try
            {
                var connection = _connections.NewByKey("Default");
                connection.Execute("UPDATE Attendance SET ApprovedBy=" + Convert.ToInt32(Context.User.GetIdentifier()) + "WHERE Id=" + request.Id);

                response.Status = "Approved";
            }
            catch (Exception ex)
            {
                response.Status = ex.Message;
            }

            return response;
        }

        [ServiceAuthorize("Attendance:Export")]
        public FileContentResult ListExcel(IDbConnection connection, AttendanceListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.AttendanceColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "Enquiry_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        // Called by the Attendance Calendar banner after any punch action (Check In / Check Out / Break)
        // to update the live stat values without a full page reload.
        [HttpGet, Authorize]
        public IActionResult GetTodayStatus(int userId)
        {
            try
            {
                using var connection = _connections.NewByKey("Default");
                var today = DateTime.Today;
                var now   = DateTime.Now;

                var rec = connection.TryFirst<MyRow>(q => q
                    .SelectTableFields()
                    .Where(MyRow.Fields.Name == userId & MyRow.Fields.PunchIn >= today));

                if (rec == null || !rec.PunchIn.HasValue)
                {
                    return new JsonResult(new
                    {
                        checkIn      = (string)null,
                        checkOut     = (string)null,
                        workingHours = "0h 00m",
                        breakTime    = "0m"
                    });
                }

                var punchIn  = rec.PunchIn.Value;
                var punchOut = rec.PunchOut;
                int breakMins = rec.BreakMinutes ?? 0;

                string checkInStr  = punchIn.ToString("hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                string checkOutStr = null;
                string workingHoursStr;

                double grossMins;
                if (punchOut.HasValue && punchOut.Value != punchIn)
                {
                    checkOutStr = punchOut.Value.ToString("hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                    grossMins   = (punchOut.Value - punchIn).TotalMinutes;
                }
                else
                {
                    grossMins = (now - punchIn).TotalMinutes;
                }

                double netMins = Math.Max(0, grossMins - breakMins);
                int wh = (int)(netMins / 60);
                int wm = (int)(netMins % 60);
                workingHoursStr = $"{wh}h {wm:D2}m";

                string breakTimeStr = breakMins >= 60
                    ? $"{breakMins / 60}h {breakMins % 60:D2}m"
                    : $"{breakMins}m";

                return new JsonResult(new
                {
                    checkIn      = checkInStr,
                    checkOut     = checkOutStr,
                    workingHours = workingHoursStr,
                    breakTime    = breakTimeStr
                });
            }
            catch
            {
                return new JsonResult(new
                {
                    checkIn      = (string)null,
                    checkOut     = (string)null,
                    workingHours = "0h 00m",
                    breakTime    = "0m"
                });
            }
        }
    }
}
