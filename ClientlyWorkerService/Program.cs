using ClientlyWorkerService.Data;
using ClientlyWorkerService.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

IConfiguration _config;
//static string con { get; set; }
Console.WriteLine("Hello, World!");
ClientyDbContext _context = new ClientyDbContext();

string[] months = new string[]
{
    "Jan",
    "Feb",
    "Mar",
    "Apr",
    "May",
    "Jun",
    "Jul",
    "Aug",
    "Sep",
    "Oct",
    "Nov",
    "Dec"
};
var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false);
_config = builder.Build();
var x = _config.GetConnectionString("ClientlyDB");
//_config.GetSection("OtherConfig").Bind(otherConfig);
Console.WriteLine("Please Enter ProcessDate: dd-MMM-yyyyy");
var dateInput = Console.ReadLine();
var Today = Convert.ToDateTime(dateInput);

Console.WriteLine("Vat Monthly Processing Starts");
await ProcessVatMonthly();
Console.WriteLine("Vat Monthly Processing Ends");

Console.WriteLine("Labour Monthly Processing Starts");
await ProcessVatLabourMonthly();
Console.WriteLine("Labour Monthly Processing Ends");

Console.WriteLine("Salary Monthly Processing Starts");
await ProcessSalaryMonthly();
Console.WriteLine("Salary Monthly Processing Ends");

Console.WriteLine("Salary 14 Days Processing Starts");
await ProcessSalary14Days();
Console.WriteLine("Salary 14 Days Processing Ends");


Console.WriteLine("Tax Statement Processing Starts");
await ProcessTaxStatementTask();
Console.WriteLine("Tax Statement Processing Ends");


Console.ReadLine();

async Task ProcessVatMonthly()
{
    try
    {
        var prevMonth = Today.Month == 1 ? 12 : Today.Month - 1;
        var year = Today.Year;
        var PrevtaskYear = Today.Month == 1 ? Today.Year - 1 : Today.Year;
        List<PrimeTaskBook> newTasks = new List<PrimeTaskBook>();
        var tasks = await _context.PrimeTaskBooks.Include(e => e.Client).Where(e => e.TaskType == "VAT - Monthly"
        && e.CreateDate.Month == prevMonth && e.CreateDate.Year == PrevtaskYear && e.CreateDate.Day < 30).AsNoTracking().ToListAsync();
        foreach (var item in tasks)
        {
            var nCreationdate = Today;
            var LaborVatMonthly = await _context.LaborVatMonthlies.Where(e => e.TaskType == "VAT - Monthly" &&
            e.InstanceId == item.InstanceId
            ).Select(e => new LabourVatMonthlyVM()
            {
                CreateDate = e.CreateDate,
                DCreateDate = DateTime.ParseExact(e.CreateDate + "-" + year.ToString(), "dd-MMM-yyyy", CultureInfo.InvariantCulture),
                DDeadline = DateTime.ParseExact(e.Deadline + "-" + year.ToString(), "dd-MMM-yyyy", CultureInfo.InvariantCulture),
                Deadline = e.Deadline,
                DFromDate = DateTime.ParseExact(e.FromDate + "-" + year.ToString(), "dd-MMM-yyyy", CultureInfo.InvariantCulture),
                FromDate = e.FromDate,
                DTillDate = DateTime.ParseExact(e.TillDate + "-" + year.ToString(), "dd-MMM-yyyy", CultureInfo.InvariantCulture),
                Id = e.Id,
                TillDate = e.TillDate,
                InstanceId = e.InstanceId,
                TaskType = e.TaskType

            }).ToListAsync();


            var LabourVat = LaborVatMonthly
            .Where(e =>
             Today.Date >= e.DCreateDate.Date &&
             Today.Date <= e.DDeadline.Date
            )
            .FirstOrDefault();

            LabourVat.DDeadline = LabourVat.DDeadline.DayOfWeek == DayOfWeek.Saturday ? LabourVat.DDeadline.AddDays(2) : LabourVat.DDeadline.DayOfWeek == DayOfWeek.Sunday ? LabourVat.DDeadline.AddDays(1) : LabourVat.DDeadline;
            var task = new PrimeTaskBook()
            {
                TaskType = item.TaskType,
                Assignee = item.Assignee,
                AtchTitle = item.AtchTitle,
                ClientId = item.ClientId,
                CreateDate = Today.Date,
                Deadline = LabourVat.DDeadline,
                InstanceId = item.InstanceId,
                FileUrl = item.FileUrl,
                IsPrimeTask = item.IsPrimeTask,
                Notes = item.Notes,
                TaskName = $"{LabourVat.TaskType} {LabourVat.DFromDate.ToString("dd MMM")} to {LabourVat.DTillDate.ToString("dd MMM yyyy")}",
                TaskStatus = "To Do",
            };

            newTasks.Add(task);

            Console.WriteLine($"Previous Task: Client : {item.Client.ClientName} TaskName : {item.TaskName} CreationDate: {item.CreateDate.ToShortDateString()} Deadline= {item.Deadline?.ToShortDateString()}");

            Console.WriteLine($"New      Task: Client : {item.Client.ClientName} TaskName : {task.TaskName} CreationDate: {task.CreateDate.ToShortDateString()} Deadline= {task.Deadline?.ToShortDateString()}");
            Console.WriteLine("");
        }

        await _context.PrimeTaskBooks.AddRangeAsync(newTasks);
        await _context.SaveChangesAsync();
    }
    catch (Exception ex)
    {

        throw;
    }


}


async Task ProcessVatLabourMonthly()
{
    //Today;
    try
    {
        var prevMonth = Today.Month == 1 ? 12 : Today.Month - 1;
        var year = Today.Year;

        var PrevtaskYear = Today.Month == 1 ? Today.Year - 1 : Today.Year;
        List<PrimeTaskBook> newTasks = new List<PrimeTaskBook>();
        var tasks = await _context.PrimeTaskBooks.Include(e => e.Client).Where(e => e.TaskType == "Labor - Monthly"
        && e.CreateDate.Month == prevMonth && e.CreateDate.Year == PrevtaskYear && e.CreateDate.Day < 30).AsNoTracking().ToListAsync();
        foreach (var item in tasks)
        {
            var nCreationdate = Today;
            var LaborVatMonthly = await _context.LaborVatMonthlies.Where(e => e.TaskType == "Labor - Monthly" &&
            e.InstanceId == item.InstanceId
            ).Select(e => new LabourVatMonthlyVM()
            {
                CreateDate = e.CreateDate,
                DCreateDate = e.FromDate == "16-Dec" ? DateTime.ParseExact(e.CreateDate + "-" + (PrevtaskYear + 1).ToString(), "dd-MMM-yyyy", CultureInfo.InvariantCulture) : DateTime.ParseExact(e.CreateDate + "-" + (PrevtaskYear).ToString(), "dd-MMM-yyyy", CultureInfo.InvariantCulture),

                DDeadline = e.FromDate == "16-Dec" ? DateTime.ParseExact(e.Deadline + "-" + (PrevtaskYear + 1).ToString(), "dd-MMM-yyyy", CultureInfo.InvariantCulture) : DateTime.ParseExact(e.Deadline + "-" + PrevtaskYear.ToString(), "dd-MMM-yyyy", CultureInfo.InvariantCulture),
                Deadline = e.Deadline,

                DFromDate = DateTime.ParseExact(e.FromDate + "-" + PrevtaskYear.ToString(), "dd-MMM-yyyy", CultureInfo.InvariantCulture),
                FromDate = e.FromDate,

                DTillDate = e.FromDate == "16-Dec" ? DateTime.ParseExact(e.TillDate + "-" + (PrevtaskYear + 1).ToString(), "dd-MMM-yyyy", CultureInfo.InvariantCulture) : DateTime.ParseExact(e.TillDate + "-" + PrevtaskYear.ToString(), "dd-MMM-yyyy", CultureInfo.InvariantCulture),

                Id = e.Id,
                TillDate = e.TillDate,
                InstanceId = e.InstanceId,
                TaskType = e.TaskType

            }).ToListAsync();

            //Where

            //@EntryDate >= FromDate and @EntryDate<= TillDate
            var LabourVat = LaborVatMonthly
            .Where(e =>
             Today.Date >= e.DFromDate.Date &&
             Today.Date <= e.DTillDate.Date
            )
            .FirstOrDefault();

            LabourVat.DDeadline = LabourVat.DDeadline.DayOfWeek == DayOfWeek.Saturday ? LabourVat.DDeadline.AddDays(2) : LabourVat.DDeadline.DayOfWeek == DayOfWeek.Sunday ? LabourVat.DDeadline.AddDays(1) : LabourVat.DDeadline;

            //Labor - 2023 (Monthly Sep)
            var task = new PrimeTaskBook()
            {
                TaskType = item.TaskType,
                Assignee = item.Assignee,
                AtchTitle = item.AtchTitle,
                ClientId = item.ClientId,
                CreateDate = Today.Date,
                Deadline = LabourVat.DDeadline,
                InstanceId = item.InstanceId,
                FileUrl = item.FileUrl,
                IsPrimeTask = item.IsPrimeTask,
                Notes = item.Notes,
                TaskName = $"Labor - {Today.Year} (Monthly {LabourVat.DFromDate.ToString("MMM")})",
                TaskStatus = "To Do",
            };

            newTasks.Add(task);

            Console.WriteLine($"Previous Task: Client : {item.Client.ClientName} TaskName : {item.TaskName} CreationDate: {item.CreateDate.ToShortDateString()} Deadline= {item.Deadline?.ToShortDateString()}");

            Console.WriteLine($"New      Task: Client : {item.Client.ClientName} TaskName : {task.TaskName} CreationDate: {task.CreateDate.ToShortDateString()} Deadline= {task.Deadline?.ToShortDateString()}");
            Console.WriteLine("");
        }

        await _context.PrimeTaskBooks.AddRangeAsync(newTasks);
        await _context.SaveChangesAsync();
    }
    catch (Exception ex)
    {

        throw;
    }


}


async Task ProcessSalaryMonthly()
{
    //Today;
    try
    {
        var prevMonth = Today.Month == 1 ? 12 : Today.Month - 1;
        var year = Today.Year;
        var PrevtaskYear = Today.Month == 1 ? Today.Year - 1 : Today.Year;
        List<PrimeTaskBook> newTasks = new List<PrimeTaskBook>();
        var tasks = await _context.PrimeTaskBooks.Include(e => e.Client).Where(e => e.TaskName == "Payroll, Manually - Monthly"
        && e.CreateDate.Month == prevMonth && e.CreateDate.Year == PrevtaskYear && e.CreateDate.Day < 30).AsNoTracking().ToListAsync();
        foreach (var item in tasks)
        {
            var nCreationdate = Today;
            var LaborVatMonthly = await _context.Payrolls.Where(e => e.TaskType == "Manually - Monthly" &&
            e.InstanceId == item.InstanceId
            ).Select(e => new LabourVatMonthlyVM()
            {
                CreateDate = e.CreateDate,
                DCreateDate = DateTime.ParseExact(e.CreateDate + "-" + (PrevtaskYear).ToString(), "dd-MMM-yyyy", CultureInfo.InvariantCulture),
                DDeadline = DateTime.ParseExact(e.Deadline + "-" + PrevtaskYear.ToString(), "dd-MMM-yyyy", CultureInfo.InvariantCulture),
                Deadline = e.Deadline,
                Id = e.Id,
                InstanceId = e.InstanceId,
                TaskType = e.TaskType

            }).ToListAsync();

            //Where

            //@EntryDate >= FromDate and @EntryDate<= TillDate
            var LabourVat = LaborVatMonthly
            .Where(e =>
             Today.Date >= e.DCreateDate.Date &&
             Today.Date <= e.DDeadline.Date
            )
            .FirstOrDefault();

            LabourVat.DDeadline = LabourVat.DDeadline.DayOfWeek == DayOfWeek.Saturday ? LabourVat.DDeadline.AddDays(2) : LabourVat.DDeadline.DayOfWeek == DayOfWeek.Sunday ? LabourVat.DDeadline.AddDays(1) : LabourVat.DDeadline;

            //Labor - 2023 (Monthly Sep)
            var task = new PrimeTaskBook()
            {
                TaskType = item.TaskType,
                Assignee = item.Assignee,
                AtchTitle = item.AtchTitle,
                ClientId = item.ClientId,
                CreateDate = Today.Date,
                Deadline = LabourVat.DDeadline,
                InstanceId = item.InstanceId,
                FileUrl = item.FileUrl,
                IsPrimeTask = item.IsPrimeTask,
                Notes = item.Notes,
                TaskName = $"Payroll, Manually - Monthly",
                TaskStatus = "To Do",
            };

            newTasks.Add(task);

            Console.WriteLine($"Previous Task: Client : {item.Client.ClientName} TaskName : {item.TaskName} CreationDate: {item.CreateDate.ToShortDateString()} Deadline= {item.Deadline?.ToShortDateString()}");

            Console.WriteLine($"New      Task: Client : {item.Client.ClientName} TaskName : {task.TaskName} CreationDate: {task.CreateDate.ToShortDateString()} Deadline= {task.Deadline?.ToShortDateString()}");
            Console.WriteLine("");
        }

        await _context.PrimeTaskBooks.AddRangeAsync(newTasks);
        await _context.SaveChangesAsync();
    }
    catch (Exception ex)
    {

        throw;
    }


}

async Task ProcessSalary14Days()
{
    //Today;
    try
    {
        var prevMonth = Today.Month == 1 ? 12 : Today.Month - 1;
        var year = Today.Year;
        var PrevtaskYear = Today.Month == 1 ? Today.Year - 1 : Today.Year;
        List<PrimeTaskBook> newTasks = new List<PrimeTaskBook>();
        var tasks = await _context.PrimeTaskBooks.Include(e => e.Client).Where(e => e.TaskName == "Payroll, Manually - 14 Days"
        && e.CreateDate.Month == prevMonth && e.CreateDate.Year == PrevtaskYear && e.CreateDate.Day <= 30).AsNoTracking().ToListAsync();
        foreach (var item in tasks)
        {
            var nCreationdate = Today;
            var LaborVatMonthly = await _context.Payrolls.Where(e => e.TaskType == "Manually - 14 Days" &&
            e.InstanceId == item.InstanceId
            ).Select(e => new LabourVatMonthlyVM()
            {
                CreateDate = e.CreateDate,
                DCreateDate = DateTime.ParseExact(e.CreateDate + "-" + (PrevtaskYear).ToString(), "dd-MMM-yyyy", CultureInfo.InvariantCulture),
                DDeadline = DateTime.ParseExact(e.Deadline + "-" + PrevtaskYear.ToString(), "dd-MMM-yyyy", CultureInfo.InvariantCulture),
                Deadline = e.Deadline,
                Id = e.Id,
                InstanceId = e.InstanceId,
                TaskType = e.TaskType

            }).ToListAsync();

            //Where

            //@EntryDate >= FromDate and @EntryDate<= TillDate
            var LabourVat = LaborVatMonthly
            .Where(e =>
             Today.Date >= e.DCreateDate.Date &&
             Today.Date <= e.DDeadline.Date
            )
            .FirstOrDefault();

            LabourVat.DDeadline = LabourVat.DDeadline.DayOfWeek == DayOfWeek.Saturday ? LabourVat.DDeadline.AddDays(2) : LabourVat.DDeadline.DayOfWeek == DayOfWeek.Sunday ? LabourVat.DDeadline.AddDays(1) : LabourVat.DDeadline;

            //Labor - 2023 (Monthly Sep)
            var task = new PrimeTaskBook()
            {
                TaskType = item.TaskType,
                Assignee = item.Assignee,
                AtchTitle = item.AtchTitle,
                ClientId = item.ClientId,
                CreateDate = Today.Date,
                Deadline = LabourVat.DDeadline,
                InstanceId = item.InstanceId,
                FileUrl = item.FileUrl,
                IsPrimeTask = item.IsPrimeTask,
                Notes = item.Notes,
                TaskName = "Payroll, Manually - 14 Days",
                TaskStatus = "To Do",
            };

            newTasks.Add(task);

            Console.WriteLine($"Previous Task: Client : {item.Client.ClientName} TaskName : {item.TaskName} CreationDate: {item.CreateDate.ToShortDateString()} Deadline= {item.Deadline?.ToShortDateString()}");

            Console.WriteLine($"New      Task: Client : {item.Client.ClientName} TaskName : {task.TaskName} CreationDate: {task.CreateDate.ToShortDateString()} Deadline= {task.Deadline?.ToShortDateString()}");
            Console.WriteLine("");
        }

        await _context.PrimeTaskBooks.AddRangeAsync(newTasks);
        await _context.SaveChangesAsync();
    }
    catch (Exception ex)
    {

        throw;
    }


}


async Task ProcessTaxStatementTask()
{
    if (Today.Month == 1 && Today.Day==1)
    {
        try
        {
            var prevMonth = Today.Month == 1 ? 12 : Today.Month - 1;
            var year = Today.Year;
            var PrevtaskYear =  Today.Year - 1 ;
            List<PrimeTaskBook> newTasks = new List<PrimeTaskBook>();
            var tasks = await _context.PrimeTaskBooks.Include(e => e.Client).Where(e => e.TaskType == "Tax Statement"
            && e.CreateDate.Year == PrevtaskYear ).AsNoTracking().ToListAsync();
            foreach (var item in tasks)
            {
                var financialyear= item.Client.FinancialYear.Split("-").FirstOrDefault()?.Trim();
                var DDs = financialyear?.Split(".");
                var month = Convert.ToInt32(DDs[1]);
                var day = Convert.ToInt32(DDs[0]);
                var creationDate = new DateTime(Today.Year, month, day);
                var Deadline = creationDate.AddMonths(6).AddDays(2);



                 Deadline = Deadline.DayOfWeek == DayOfWeek.Saturday ? Deadline.AddDays(2) : Deadline.DayOfWeek == DayOfWeek.Sunday ? Deadline.AddDays(1) : Deadline;

                //Labor - 2023 (Monthly Sep)
                var task = new PrimeTaskBook()
                {
                    TaskType = item.TaskType,
                    Assignee = item.Assignee,
                    AtchTitle = item.AtchTitle,
                    ClientId = item.ClientId,
                    CreateDate = creationDate,
                    Deadline = Deadline,
                    InstanceId = item.InstanceId,
                    FileUrl = item.FileUrl,
                    IsPrimeTask = item.IsPrimeTask,
                    Notes = item.Notes,
                    TaskName = item.TaskName.Replace(PrevtaskYear.ToString(),Today.Year.ToString()),
                    TaskStatus = "To Do",
                };

                newTasks.Add(task);

                Console.WriteLine($"Previous Task: Client : {item.Client.ClientName} TaskName : {item.TaskName} CreationDate: {item.CreateDate.ToShortDateString()} Deadline= {item.Deadline?.ToShortDateString()}");

                Console.WriteLine($"New      Task: Client : {item.Client.ClientName} TaskName : {task.TaskName} CreationDate: {task.CreateDate.ToShortDateString()} Deadline= {task.Deadline?.ToShortDateString()}");
                Console.WriteLine("");
            }

            await _context.PrimeTaskBooks.AddRangeAsync(newTasks);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    //Today;
  


}