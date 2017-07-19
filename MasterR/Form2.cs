using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Intrpl;
using GraphDraw;
using PointCoord;
using System.Windows.Forms.DataVisualization.Charting;
using ExperimentalE;

namespace MasterR
{
    public partial class Form2 : Form
    {
        double habanje;
        double vreme;
        GraphDrawing graphD = new GraphDrawing();
        ExperimentalExamination ispitivanje = new ExperimentalExamination();

        public Form2()
        {
            InitializeComponent();
        }

        private List<double> sumiranjeListe(List<double> list)
        {
            List<double> resultList = new List<double>();
            double sum = 0;
            for (int listIndex = 0; listIndex < list.Count; listIndex++)
            {
                sum += list[listIndex];
                resultList.Add(sum);
            }
            return resultList;
        }

        private void crtanjeLogartimaskogGrafika()
        {
            double[] brzine = { 3, 2, 1 };
            double[] postojanost = { 900, 600, 300 };
            Series series = chart2.Series.Add("a");

            chart2.ChartAreas[0].AxisY.IsStartedFromZero = false;
            chart2.ChartAreas[0].AxisX.IsLogarithmic = true;
            chart2.ChartAreas[0].AxisY.IsLogarithmic = true;

            chart2.ChartAreas[0].AxisX.MinorGrid.Interval = 1;
            chart2.ChartAreas[0].AxisX.MinorGrid.Enabled = true;
            chart2.ChartAreas[0].AxisX.MinorGrid.LineDashStyle = ChartDashStyle.DashDot;

            chart2.ChartAreas[0].AxisY.MinorGrid.Interval = 1;
            chart2.ChartAreas[0].AxisY.MinorGrid.Enabled = true;
            chart2.ChartAreas[0].AxisY.MinorGrid.LineDashStyle = ChartDashStyle.DashDot;
            double xCoordinate = 0;
            double yCoordinate = 0;
            double xMax = 0;
            double yMax = 0;
            double xMin = 0;
            double yMin = 0;

            for (int listElement = 0; listElement < brzine.Length; listElement++)
            {
                series.MarkerStyle = MarkerStyle.Circle;
                series.MarkerSize = 5;
                series.ChartType = SeriesChartType.Line;
                series.SmartLabelStyle.Enabled = true;
                xCoordinate = brzine[listElement];
                yCoordinate = postojanost[listElement];
                series.Points.AddXY(xCoordinate, yCoordinate);
                //series.Points[listElement].IsValueShownAsLabel = true;
                if (xCoordinate > xMax)
                {
                    xMax = Math.Ceiling(xCoordinate) * 1.1;
                }
                if (yCoordinate > yMax)
                {
                    yMax = Math.Ceiling(yCoordinate) * 1.1;
                }

                if (listElement == 0)
                {
                    xMin = xCoordinate;
                    yMin = yCoordinate;
                    if (xMin < 1)
                    {
                        xMin = minValueCalculation(xMin);

                    }
                    if (yMin < 1)
                    {
                        yMin = minValueCalculation(yMin);
                    }
                }
                if (xCoordinate <= xMin)
                {
                    xMin = minValueCalculation(xCoordinate);
                }
                if (yCoordinate <= yMin)
                {
                    yMin = minValueCalculation(yCoordinate);
                }
            }
            chart2.ChartAreas[0].AxisX.Maximum = xMax;
            chart2.ChartAreas[0].AxisX.Minimum = xMin;
            chart2.ChartAreas[0].AxisY.Maximum = yMax;
            chart2.ChartAreas[0].AxisY.Minimum = yMin;

            double spacerX = 0.90d;
            double spacerY = 0.95d;

            ChartArea ca = chart2.ChartAreas[0];
            ca.AxisX.IsLabelAutoFit = false;

            dodavanjeLabela(xMin, spacerX, spacerY, ca);
            chart2.ChartAreas[0].AxisX.LabelStyle.Format = "#0.0";
            chart2.ChartAreas[0].AxisY.LabelStyle.Format = "#0.0";
        }

        private static void dodavanjeLabela(double xMin, double spacerX, double spacerY, ChartArea ca)
        {
            for (int n = -1; n < 3; n++)
            {
                for (int i = 1; i < 10; i++)
                {
                    CustomLabel cl = new CustomLabel();
                    double x = i * Math.Pow(10, n);
                    cl.FromPosition = Math.Log10(x) * spacerX;
                    cl.ToPosition = Math.Log10(x) / spacerX;

                    if (i == 1 && n == 0)
                    {
                        cl.FromPosition = 0f;
                        cl.ToPosition = 0.01f;
                    }
                    if (i == 1 && n == -1)
                    {
                        cl.ToPosition += 0.02f;
                    }
                    cl.Text = x.ToString();

                    ca.AxisX.CustomLabels.Add(cl);
                    cl.Axis.LabelStyle.Format = "#0.0";
                    if (xMin < 1)
                    {
                        ca.AxisX.IsLabelAutoFit = true;

                    }
                }
            }
            for (int n = -4; n < 1; n++)
            {
                for (int i = 1; i < 10; i++)
                {
                    CustomLabel cl = new CustomLabel();
                    double x = i * Math.Pow(10, n);
                    cl.FromPosition = Math.Log10(x * 100) * spacerY;
                    cl.ToPosition = Math.Log10(x * 100) / spacerY;
                    cl.Text = (x * 100).ToString();

                    if (i == 1 && n == -2)
                    {
                        cl.FromPosition = 0f;
                        cl.ToPosition = 0.01f;
                    }
                    ca.AxisY.CustomLabels.Add(cl);
                    ca.AxisY.IsLabelAutoFit = true;
                    ca.AxisY.TextOrientation = TextOrientation.Horizontal;
                    cl.Axis.LabelStyle.Format = "#0.0";
                }
            }
        }

        private double minValueCalculation(double inputValue)
        {
            double conditionValue = 0.0;
            double result = 0.0;
            int multCoefficient = 1;
            if (inputValue >= 1)
            {
                while (true)
                {
                    conditionValue = Math.Floor(inputValue / multCoefficient);
                    if (conditionValue >= 10)
                    {
                        multCoefficient *= 10;
                    }
                    else
                    {
                        result = multCoefficient;
                        break;
                    }
                }
            }
            if (inputValue < 1)
            {
                while (true)
                {
                    conditionValue = (inputValue * multCoefficient);
                    if (conditionValue < 1)
                    {
                        multCoefficient *= 10;
                    }
                    else
                    {
                        result = 1 / (double)multCoefficient;
                        break;
                    }
                }
            }
            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            vrednostParametraNumericUpDown.Enabled = false;
            vrednostParametraNumericUpDown.BackColor = Color.LightBlue;
            unosRezultataMerenjaDataGridView.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<double> listaHabanja = new List<double>();
            List<double> listaDeltaVremena = new List<double>();
            double a = (double)vrednostParametraNumericUpDown.Value;
            foreach (DataGridViewRow item in unosRezultataMerenjaDataGridView.Rows)
            {
                if (item.Cells[0].Value != null && item.Cells[1].Value != null)
                {
                    bool isDouble = double.TryParse(item.Cells[0].Value.ToString(), out habanje);
                    bool isDouble2 = double.TryParse(item.Cells[1].Value.ToString(), out vreme);
                    if (isDouble && isDouble2)
                    {
                        listaHabanja.Add(vreme);
                        listaDeltaVremena.Add(habanje);
                    }
                }
            }
            List<double> listaVremena = sumiranjeListe(listaDeltaVremena);
            double izaracunatoVreme = Interpolation.NevilleInterpolator(listaHabanja, listaVremena, a);
            double brzinaVarirana = (double)brzinaNumericUpDown.Value;
            ispitivanje.InputParameters(brzinaVarirana, izaracunatoVreme);
            unosRezultataMerenjaDataGridView.Rows.Clear();
            List<PointCoordinates> listaTacakaZaGrafik = new List<PointCoordinates>();
            PointCoordinates point = new PointCoordinates();
            point.YCoordinate = 0;
            point.XCoordinate = 0;
            listaTacakaZaGrafik.Add(point);
            for (int i = 0; i < listaHabanja.Count; i++)
            {
                point = new PointCoordinates();
                point.YCoordinate = listaHabanja[i];
                point.XCoordinate = listaVremena[i];
                listaTacakaZaGrafik.Add(point);
            }
            graphD.DrawGraph(listaTacakaZaGrafik, brzinaVarirana.ToString(), brzinaVarirana, 1);
        }

        private void sizeDGV(DataGridView dgv, int maxNumOfRows, int columnWidth, int firstColumnWidth)
        {
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToOrderColumns = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AllowDrop = false;
            dgv.RowHeadersVisible = false;
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (column == dgv.Columns[0])
                {
                    column.Width = firstColumnWidth;
                }
                else
                {
                    column.Width = columnWidth;
                }
                column.HeaderCell.Style.Font = new Font("Consolas", 8.25F);
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            DataGridViewElementStates states = DataGridViewElementStates.None;
            int numOfRows = dgv.Rows.Count;
            int rowHeight = dgv.RowTemplate.Height;
            int sumOfRowsHeights = dgv.Rows.GetRowsHeight(states);
            int maxTotalHeight = maxNumOfRows * rowHeight;
            int totalHeight;
            int totalWidth;
            if (sumOfRowsHeights > maxTotalHeight)
            {
                totalHeight = maxTotalHeight + dgv.ColumnHeadersHeight;
                totalWidth = dgv.Columns.GetColumnsWidth(states) + dgv.RowHeadersWidth / 2 - 3;
                dgv.ScrollBars = ScrollBars.Vertical;
            }
            else
            {
                totalHeight = dgv.Rows.GetRowsHeight(states) + dgv.ColumnHeadersHeight;
                totalWidth = dgv.Columns.GetColumnsWidth(states);
                dgv.ScrollBars = ScrollBars.None;
            }
            dgv.ClientSize = new Size(totalWidth, totalHeight);
        }
    }
}
