using Genocs.CleanArchitecture.Template.Application.Boundaries.GetAccountDetails;
using Genocs.CleanArchitecture.Template.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Data;

namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V2.GetAccountDetails;

public sealed class GetAccountDetailsPresenterV2 : IOutputPort
{
    public IActionResult? ViewModel { get; private set; }

    public void Error(string message)
    {
        var problemDetails = new ProblemDetails()
        {
            Title = "An error occurred",
            Detail = message
        };

        ViewModel = new BadRequestObjectResult(problemDetails);
    }

    public void Default(GetAccountDetailsOutput getAccountDetailsOutput)
    {
        var dataTable = new DataTable();
        dataTable.Columns.Add("Amount", typeof(decimal));
        dataTable.Columns.Add("Description", typeof(string));
        dataTable.Columns.Add("TransactionDate", typeof(DateTime));

        foreach (var item in getAccountDetailsOutput.Transactions)
        {
            var transaction = new TransactionModel(
                item.Amount,
                item.Description,
                item.TransactionDate);

            dataTable.Rows.Add(new object[] { item.Amount, item.Description, item.TransactionDate });
        }

        byte[] fileContents;

        using (ExcelPackage pck = new ExcelPackage())
        {
            var ws = pck.Workbook.Worksheets.Add(getAccountDetailsOutput.AccountId.ToString());
            ws.Cells["A1"].LoadFromDataTable(dataTable, true);
            ws.Row(1).Style.Font.Bold = true;
            ws.Column(3).Style.Numberformat.Format = "dd/MM/yyyy HH:mm";
            fileContents = pck.GetAsByteArray();
        }

        ViewModel = new FileContentResult(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    public void NotFound(string message)
    {
        ViewModel = new NotFoundObjectResult(message);
    }
}