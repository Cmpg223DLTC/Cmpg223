using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DriversSystem
{
    public partial class AdminPortal_NumAppointmentsPerWeek : System.Web.UI.Page
    {
        DatabaseHelper dbHelper = new DatabaseHelper();
        int grandTotal;

        // Default time is current month
        static DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        static DateTime endDate = startDate.AddMonths(1).AddDays(-1);
        string startDateString = startDate.ToString("yyyy-MM-dd");
        string endDateString = endDate.ToString("yyyy-MM-dd");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StartDateTextBox.Text = startDateString;
                EndDateTextBox.Text = endDateString;
                lblDateTime.Text = DateTime.Now.ToString("MMMM dd, yyyy HH:mm:ss");

               
                updateWeeklyReportHeading(startDate, endDate);

                populateGridView();
            }
        }

        protected void FilterButton_Click(object sender, EventArgs e)
        {
            startDateString = StartDateTextBox.Text;
            endDateString = EndDateTextBox.Text;

          
            DateTime startDate = DateTime.Parse(startDateString);
            DateTime endDate = DateTime.Parse(endDateString);

            updateWeeklyReportHeading(startDate, endDate);

            populateGridView();
        }

        protected void updateWeeklyReportHeading(DateTime startDate, DateTime endDate)
        {
            weeklyReportHeading.Text = "Weekly Appointments Report from " +
                                       startDate.ToString("yyyy-MM-dd") +
                                       " to " + endDate.ToString("yyyy-MM-dd");
        }
        protected void populateGridView()
        {
            ApplicationGridView.Columns.Clear();
            // Parse the input dates
            DateTime startDate = DateTime.Parse(startDateString);
            DateTime endDate = DateTime.Parse(endDateString);
            // Calculate the total number of weeks
            int totalWeeks = (int)((endDate - startDate).TotalDays / 7) + 1;
            totalWeeks = Math.Min(totalWeeks, 8);  // Limit to a maximum of 8 weeks
                                                   // Start building the SQL query to calculate the appointments per week
            string query = @"
        SELECT 
            CONCAT(CAST(t.StartTime AS VARCHAR(5)),' - ',CAST(t.EndTime AS VARCHAR(5))) AS TimeSlot,";

            // Create CASE statements to calculate counts for each week
            for (int i = 0; i < totalWeeks; i++)
            {
                query += $@"
            COUNT(CASE WHEN t.Date BETWEEN @Week{i}Start AND @Week{i}End THEN a.Application_ID ELSE NULL END) AS Week{i + 1},";
            }

            // Finish the SQL query for total appointments
            query += @"
        COUNT(a.Application_ID) AS TotalAppointments
        FROM 
            Available_Time t
        LEFT JOIN 
            Application a ON t.Time_ID = a.Time_ID
        WHERE 
            t.Date BETWEEN @StartDate AND @EndDate
            AND t.StartTime BETWEEN '08:00:00' AND '14:00:00'
        GROUP BY 
            t.StartTime, t.EndTime
        ORDER BY 
            t.StartTime";

            // Create the list of SQL parameters, including each week's start and end date
            List<SqlParameter> parameters = new List<SqlParameter>
    {
        new SqlParameter("@StartDate", SqlDbType.Date) { Value = startDate },
        new SqlParameter("@EndDate", SqlDbType.Date) { Value = endDate }
    };

            // Add parameters for each week's start and end dates
            for (int i = 0; i < totalWeeks; i++)
            {
                DateTime weekStart = startDate.AddDays(i * 7);
                DateTime weekEnd = weekStart.AddDays(6);
                parameters.Add(new SqlParameter($"@Week{i}Start", SqlDbType.Date) { Value = weekStart });
                parameters.Add(new SqlParameter($"@Week{i}End", SqlDbType.Date) { Value = weekEnd });
            }

            // Execute the query
            DataTable resultTable = dbHelper.ExecuteQuery(query, parameters.ToArray());
            ApplicationGridView.DataSource = resultTable;

            // Add Time Slot column
            BoundField timeSlotColumn = new BoundField();
            timeSlotColumn.HeaderText = "Time Slot";
            timeSlotColumn.DataField = "TimeSlot";
            ApplicationGridView.Columns.Add(timeSlotColumn);

            // Add columns for each week dynamically
            for (int i = 1; i <= totalWeeks; i++)
            {
                BoundField weekColumn = new BoundField();
                weekColumn.HeaderText = $"Week {i}";
                weekColumn.DataField = $"Week{i}";
                ApplicationGridView.Columns.Add(weekColumn);
            }

            // Add the Total Appointments column
            BoundField totalAppointmentsColumn = new BoundField();
            totalAppointmentsColumn.HeaderText = "Total Appointments";
            totalAppointmentsColumn.DataField = "TotalAppointments";
            ApplicationGridView.Columns.Add(totalAppointmentsColumn);

            // Bind the data to the GridView
            ApplicationGridView.DataBind();
        }
        // Display the grand total
        protected void ApplicationGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int rowTotal = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalAppointments"));
                grandTotal += rowTotal;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[e.Row.Cells.Count - 2].Text = "Grand Total:";
                e.Row.Cells[e.Row.Cells.Count - 2].Font.Bold = true;

                e.Row.Cells[e.Row.Cells.Count - 1].Text = grandTotal.ToString();
                e.Row.Cells[e.Row.Cells.Count - 1].Font.Bold = true;
            }
        }


    }
}