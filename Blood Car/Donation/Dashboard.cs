using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Donation
{
    public partial class Dashboard : Form
    {
        bool drag = false;
        Point start_point = new Point(0, 0);
        private SqlConnection connection;
        private string connectionString = "Data Source=MASTER;Initial Catalog=Blood_Bank;Integrated Security=True";
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            using (connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    RunQueries();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    // Always close the connection in a finally block to ensure it's closed even if an exception occurs
                    if (connection.State == System.Data.ConnectionState.Open)
                        connection.Close();
                }
            }
        }
        private void RunQueries()
        {
            // Query 1: Total Number of Requests
            string query1 = "select count(ReqID) from [Requests] where ReqDate between @fromDate and @toDate";
            int numRequests = ExecuteScalarQuery<int>(query1, new SqlParameter("@fromDate", System.Data.SqlDbType.DateTime) { Value = DateTime.Parse("2023-05-05") }, new SqlParameter("@toDate", System.Data.SqlDbType.DateTime) { Value = DateTime.Parse("2024-01-01") });
            lblNumReq.Text = numRequests.ToString();

            // Query 2: Top 5 Blood Types in Stock
            DisplayTopBloodTypesChart();

            // Query 3: Total Number of Donors
            lblNumDonors.Text = ExecuteScalarQuery<int>("select count(DonorId) from Donors").ToString();

            // Query 4: Total Number of Patients
            lblNumPatients.Text = ExecuteScalarQuery<int>("select count(PatientId) from Patients").ToString();

            // Query 5: Total Number of Stock
            lblTotalProducts.Text = ExecuteScalarQuery<int>("select count(StockId) from Stock").ToString();

            // Query 6: Blood Types with Stock <= 6
            // (Note: The results of this query are not being used in the current code)

            // Query 7: Count of each blood type in Stock
            DisplayBloodTypeCounts();
            // Display the donor age distribution
            DisplayDonorAgeDistributionChart();
            DisplayLowStockBloodTypes();
            DisplayGenderDistributionChart();
        }
        private void DisplayTopBloodTypesChart()
        {
            string query = "SELECT TOP 5 BType, COUNT(*) AS TotalCount FROM Stock GROUP BY BType ORDER BY TotalCount DESC";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@fromDate", System.Data.SqlDbType.DateTime).Value = DateTime.Parse("2023-05-05");
                command.Parameters.Add("@toDate", System.Data.SqlDbType.DateTime).Value = DateTime.Parse("2024-01-01");

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    chart2.Series.Clear();
                    Series series = new Series("Blood Types");
                    series.ChartType = SeriesChartType.Doughnut;

                    while (reader.Read())
                    {
                        string bloodType = reader["BType"].ToString();
                        int totalCount = Convert.ToInt32(reader["TotalCount"]);

                        DataPoint dataPoint = new DataPoint();
                        dataPoint.SetValueXY(bloodType, totalCount);
                        dataPoint.LegendText = bloodType;
                        dataPoint.Label = totalCount.ToString();

                        series.Points.Add(dataPoint);
                    }

                    chart2.Series.Add(series);
                }
            }
        }
       
        private void DisplayBloodTypeCounts()
        {
            string query = "SELECT BType, COUNT(StockId) AS TotalCount FROM Stock GROUP BY BType";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    int labelIndex = 1;
                    while (reader.Read() && labelIndex <= 8) // Assuming you have 8 labels
                    {
                        string bloodType = reader["BType"].ToString();
                        int totalCount = Convert.ToInt32(reader["TotalCount"]);

                        Label currentLabel = Controls.Find($"labelBloodTypeCount{labelIndex}", true).FirstOrDefault() as Label;

                        if (currentLabel != null)
                        {
                            currentLabel.Text = $"{bloodType} : {totalCount}";
                        }

                        labelIndex++;
                    }
                }
            }
        }
        private void DisplayDonorAgeDistributionChart()
        {
            try
            {
                // Query: Retrieve donor ages
                string query = "SELECT Age FROM Donors";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Clear existing data in the chart
                        chart1.Series.Clear();

                        // Create a new series for the chart
                        Series series = new Series("Donor Age Distribution");
                        series.ChartType = SeriesChartType.Column;

                        // Set the bin width for the histogram
                        double binWidth = 5;

                        while (reader.Read())
                        {
                            // Read donor ages from the database
                            int donorAge = Convert.ToInt32(reader["Age"]);

                            // Calculate the bin for the current donor age
                            int bin = (int)(donorAge / binWidth) * (int)binWidth;

                            // Find the data point in the series corresponding to the bin
                            DataPoint dataPoint = null;

                            foreach (DataPoint existingDataPoint in series.Points)
                            {
                                if (existingDataPoint.XValue == bin)
                                {
                                    dataPoint = existingDataPoint;
                                    break;
                                }
                            }

                            // If the data point exists, increment its Y value; otherwise, create a new data point
                            if (dataPoint != null)
                            {
                                dataPoint.YValues[0]++;
                            }
                            else
                            {
                                series.Points.AddXY(bin, 1);
                            }
                        }

                        // Add the series to the chart
                        chart1.Series.Add(series);

                        // Customize the appearance of the chart
                        chart1.ChartAreas[0].AxisX.Title = "Donor Age";
                        chart1.ChartAreas[0].AxisY.Title = "Number of Donors";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void DisplayLowStockBloodTypes()
        {
            try
            {
                // Query: Blood Types with Stock <= 6
                string query = "SELECT BType, COUNT(StockID) AS TotalCount FROM Stock GROUP BY BType HAVING COUNT(StockID) <= 6";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Create a DataTable to hold the result
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        // Check if DataGridView columns need to be created
                        if (dgvUnderStock.Columns.Count == 0)
                        {
                            // Auto-generate columns based on the DataTable
                            dgvUnderStock.AutoGenerateColumns = true;
                            dgvUnderStock.DataSource = dataTable;
                        }
                        else
                        {
                            // Set the existing columns to match the DataTable schema
                            dgvUnderStock.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void DisplayGenderDistributionChart()
        {
            try
            {
                // Query: Count of genders from donors
                string query = "SELECT Gender, COUNT(*) AS TotalCount FROM Donors GROUP BY Gender";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Clear existing data in the chart
                        chart3.Series.Clear();

                        // Create a new series for the chart
                        Series series = new Series("Gender Distribution");
                        series.ChartType = SeriesChartType.Bar;

                        while (reader.Read())
                        {
                            // Read gender and count from the database
                            string gender = reader["Gender"].ToString();
                            int totalCount = Convert.ToInt32(reader["TotalCount"]);

                            // Map gender values to meaningful labels
                            string genderLabel = (gender == "Male") ? "Male" : "Female";

                            // Add data points to the series
                            DataPoint dataPoint = new DataPoint();
                            dataPoint.SetValueXY(genderLabel, totalCount);

                            // Set legend and label for the data point
                            dataPoint.LegendText = genderLabel;
                            dataPoint.Label = totalCount.ToString();

                            series.Points.Add(dataPoint);
                        }

                        // Add the series to the chart
                        chart3.Series.Add(series);

                        // Customize the appearance of the chart
                        chart3.ChartAreas[0].AxisX.Title = "Gender";
                        chart3.ChartAreas[0].AxisY.Title = "Number of Donors";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private T ExecuteScalarQuery<T>(string query, params SqlParameter[] parameters)
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddRange(parameters);
                object result = command.ExecuteScalar();
                return result == DBNull.Value ? default(T) : (T)result;
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Dashboard_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            start_point = new Point(e.X, e.Y);
        }

        private void Dashboard_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - start_point.X, p.Y - start_point.Y);
            }
        }

        private void Dashboard_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;

        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            AddDonor adddonor = new AddDonor();
            this.Hide();
            adddonor.FormClosed += (s, args) => this.Close();            
            adddonor.Show();
        }
    }
}
