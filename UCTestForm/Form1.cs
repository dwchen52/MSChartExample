using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace UCTestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetupDT();
            InitDT();
        }

        DataTable dt = new DataTable();
        private void Form1_Load(object sender, EventArgs e)
        {
            int minLavel = Convert.ToInt32(dt.Compute("min([Low])", string.Empty))-2;
            int maxLavel = Convert.ToInt32(dt.Compute("max([High])", string.Empty))+2;

            chart.DataSource = dt;

            chart.ChartAreas.Clear();                                           // 清除所有的 Chartarea
            //chart.BackColor = Color.Black;
            //chart.ForeColor = Color.White;
            

            #region 1st ChartArea            
            ChartArea StockArea = new ChartArea("StockKArea");                  // 第一個顯示區(K棒顯示) 
            chart.ChartAreas.Add(StockArea);    

            // ChartArea Init
            StockArea.Position.Y = 5;                                           //chart.ChartAreas[0].Position.Y = 0;
            StockArea.Position.Height = 70;
            StockArea.Position.X = 10;
            StockArea.Position.Width = 94;
            StockArea.BackColor = Color.Black;                                  // Area 背景顏色
            StockArea.BorderColor = Color.DarkRed;                              // 外框顏色
            StockArea.BorderWidth = 2;                                          // 外框線條寬度
            StockArea.BorderDashStyle = ChartDashStyle.Solid;                   // 外框線條樣式
            StockArea.BackSecondaryColor = Color.White;

            
            //StockArea.AxisY2.Enabled = AxisEnabled.True;
            //StockArea.BackGradientStyle = GradientStyle.HorizontalCenter;
            //StockArea.ShadowOffset = 10;

            // X格線設定 
            StockArea.AxisX.MajorGrid.LineWidth = 1;                            // X格線
            StockArea.AxisX.MajorGrid.LineColor = Color.FromArgb(53, 53, 53);   // X格線顏色
            StockArea.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            StockArea.AxisX.LabelStyle.Enabled = false;                         // Disable x label
            StockArea.AxisX.LineColor = Color.Red;                              // 底部外框線顏色

            // Y格線設定
            // 將右側的數據移到右側
            StockArea.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            StockArea.AxisY2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;

            StockArea.AxisY.MajorGrid.LineColor = Color.FromArgb(53, 53, 53);   // Y格線顏色
            StockArea.AxisY.MajorGrid.LineWidth = 1;                            // Y格線寬度
            StockArea.AxisY.Minimum = minLavel;                                 // Y顯示值的最小值
            StockArea.AxisY.Maximum = maxLavel;                                 // Y顯示值的最大值
            StockArea.AxisY.LineColor = Color.Red;                              // 左側外框線顏色
            StockArea.AxisY2.LabelStyle.ForeColor = Color.Red;                  // 左側格線數據顏色
            //chart.ChartAreas["StockKArea"].AxisY.Title = "數量";              // 顯示Y的抬頭
            //StockArea.ShadowColor = Color.Red;

            // Series Section
            chart.Series.Clear();
            // Series1 is for K Stock (K棒 圖,蠟燭線)
            Series series1 = new Series("StockK");
            series1.ChartArea = "StockKArea";
            chart.Series.Add(series1);
            
            series1.IsValueShownAsLabel = false;             // Disable Show Series Name(STockK)            
            series1.ChartType = SeriesChartType.Candlestick; // 顯示為 蠟燭線
            series1.XValueMember = "Time";                   // Depend on Datatable "Time" Column
            series1.YValueMembers = "High,Low,Open,Close";   // Depend on Datatable "Open,High,Low,Close" Column
            series1.IsXValueIndexed = true;
            //series1.XValueMember.

            series1.XValueType = ChartValueType.Date;        // 
            series1.CustomProperties = "PriceDownColor=Blue, PriceUpColor=Red";
            //chart.Series["StockK"]["OpenCloseStyle"] = "Triangle";
            //chart.Series["StockK"]["ShowOpenClose"] = "Both";

            // Serias2 is for MA1
            Series series2 = new Series("MA1");
            series2.ChartArea = "StockKArea";
            chart.Series.Add(series2);
            series2.IsValueShownAsLabel = false;             // Disable Show Series Name(STockK)            
            series2.ChartType = SeriesChartType.Spline;      // 顯示為 蠟燭線
            series2.XValueMember = "Time";                   // Depend on Datatable "Time" Column
            series2.YValueMembers = "MA1";                   // Depend on Datatable "MA1" Column
            series2.IsXValueIndexed = true;
            series2.XValueType = ChartValueType.Date;        // 
            series2.Color = Color.Red;
            // Series3 is for MA2


            // Series4 is for BB_MA


            // Series5 is for BB_UP


            // Series6 is for BB_Down


            #endregion

            #region 2nd ChartArea
            int VolumeMaxLavel = Convert.ToInt32(dt.Compute("max([Volume])", string.Empty)) + 10;
            ChartArea VolumnArea = new ChartArea("VolumnArea");                 // 第二個顯示區(量顯示)
            chart.ChartAreas.Add(VolumnArea);

            VolumnArea.Position.Y = StockArea.Position.Bottom ; 
            VolumnArea.Position.Height = 28;
            VolumnArea.Position.X = 0;
            VolumnArea.Position.Width = 95;

            VolumnArea.AxisX.MajorGrid.LineWidth = 0;           // X格線
            VolumnArea.AxisY.MajorGrid.LineWidth = 1;           // Y格線
            VolumnArea.AxisY.Minimum = 0;                       // Y顯示值的最小值
            VolumnArea.AxisY.Maximum = VolumeMaxLavel;          // Y顯示值的最大值
            //chart.ChartAreas["StockKArea"].AxisY.Title = "數量";                // 顯示Y的抬頭
            //VolumnArea.ShadowColor = Color.Red;
            //VolumnArea.BackColor = Color.White; 

            // Series Section            
            Series seriesVolume = new Series("KVolume");
            seriesVolume.ChartArea = "VolumnArea";
            chart.Series.Add(seriesVolume);

            seriesVolume.IsValueShownAsLabel = false;               // Disable Show Series Name(STockK)
            seriesVolume.ChartType = SeriesChartType.Column;          // 顯示為 柱狀線
            seriesVolume.XValueMember = "Time";                     // Depend on Datatable "Time" Column
            seriesVolume.YValueMembers = "Volume";                  // Depend on Datatable "Open,High,Low,Close" Column
            seriesVolume.IsXValueIndexed = true;
            seriesVolume.XValueType = ChartValueType.Date;          // 

            seriesVolume.Color = Color.FromArgb(0, 146, 0);


            //StockKseries.CustomProperties = "PriceDownColor=Blue, PriceUpColor=Red";
            //chart.Series["StockK"]["OpenCloseStyle"] = "Triangle";
            //chart.Series["StockK"]["ShowOpenClose"] = "Both";

            chart.DataBind();

            // 這段程式必須在 chart.DataBind 之後
            for (int i = 0; i < seriesVolume.Points.Count; i++)
            {
                //如果數列的K值是紅棒，將Volume數列的顏色變為紅色
                if (Convert.ToSingle(dt.Rows[i]["Open"].ToString())< Convert.ToSingle(dt.Rows[i]["Close"].ToString()))
                    seriesVolume.Points[i].Color = Color.Red;                
            }
           

            #endregion


            //chart.DataManipulator.IsStartFromFirst = true;

            //chart.Series["StockK"].BorderColor = System.Drawing.Color.Yellow;

            // chart.Series["StockK"].Color = System.Drawing.Color.Yellow;




            //int i = seriesVolume.Points.Count;


        }

        public void SetupDT()
        {
            dt = new DataTable();
            dt.Columns.Add("Time");
            dt.Columns.Add("Open",typeof(float));
            dt.Columns.Add("High", typeof(float));
            dt.Columns.Add("Low", typeof(float));
            dt.Columns.Add("Close", typeof(float));
            dt.Columns.Add("Volume", typeof(Int32));
            dt.Columns.Add("MA1", typeof(Int32));
            dt.Columns.Add("MA2", typeof(Int32));

        }

        public void InitDT()
        {
            DataRow dr = dt.NewRow();
            dr["Time"] = "10:00";
            dr["Open"] = 25.2;
            dr["High"] = 25.5;
            dr["Low"] = 25;
            dr["Close"] = 25.4;
            dr["Volume"] = 200;
            dr["MA1"] = 25;
            dr["MA2"] = 20;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Time"] = "10:01";
            dr["Open"] = 25.4;
            dr["High"] = 25.8;
            dr["Low"] = 25.2;
            dr["Close"] = 25.6;
            dr["Volume"] = 120;
            dr["MA1"] = 25;
            dr["MA2"] = 20;
            dt.Rows.Add(dr);
            
            dr = dt.NewRow();
            dr["Time"] = "10:02";
            dr["Open"] = 25.6;
            dr["High"] = 25.9;
            dr["Low"] = 25.5;
            dr["Close"] = 25.9;
            dr["Volume"] = 100;
            dr["MA1"] = 26;
            dr["MA2"] = 22;
            dt.Rows.Add(dr);
            
            dr = dt.NewRow();
            dr["Time"] = "10:03";
            dr["Open"] = 25.9;
            dr["High"] = 25.9;
            dr["Low"] = 25;
            dr["Close"] = 25.2;
            dr["Volume"] = 120;
            dr["MA1"] = 25;
            dr["MA2"] = 22;
            dt.Rows.Add(dr);
            
            dr = dt.NewRow();
            dr["Time"] = "10:04";
            dr["Open"] = 25.2;
            dr["High"] = 25.5;
            dr["Low"] = 25;
            dr["Close"] = 25.4;
            dr["Volume"] = 130;
            dr["MA1"] = 25;
            dr["MA2"] = 21;
            dt.Rows.Add(dr);
            
            dr = dt.NewRow();
            dr["Time"] = "10:05";
            dr["Open"] = 25.4;
            dr["High"] = 25.5;
            dr["Low"] = 25;
            dr["Close"] = 25.2;
            dr["Volume"] = 150;
            dr["MA1"] = 26;
            dr["MA2"] = 21;
            dt.Rows.Add(dr);
            
            dr = dt.NewRow();
            dr["Time"] = "10:06";
            dr["Open"] = 25.2;
            dr["High"] = 25.5;
            dr["Low"] = 25;
            dr["Close"] = 25.4;
            dr["Volume"] = 200;
            dr["MA1"] = 25;
            dr["MA2"] = 22;
            dt.Rows.Add(dr);
            
            dr = dt.NewRow();
            dr["Time"] = "10:07";
            dr["Open"] = 25.4;
            dr["High"] = 25.5;
            dr["Low"] = 24.8;
            dr["Close"] = 24.8;
            dr["Volume"] = 160;
            dr["MA1"] = 25;
            dr["MA2"] = 20;
            dt.Rows.Add(dr);
            
            dr = dt.NewRow();
            dr["Time"] = "10:08";
            dr["Open"] = 24.8;
            dr["High"] = 25.2;
            dr["Low"] = 24.6;
            dr["Close"] = 24.8;
            dr["Volume"] = 120;
            dr["MA1"] = 25;
            dr["MA2"] = 24;
            dt.Rows.Add(dr);
            
            dr = dt.NewRow();
            dr["Time"] = "10:09";
            dr["Open"] = 24.8;
            dr["High"] = 25;
            dr["Low"] = 24.3;
            dr["Close"] = 24.4;
            dr["Volume"] = 100;
            dr["MA1"] = 24;
            dr["MA2"] = 23;
            dt.Rows.Add(dr);

        }
    }
}
