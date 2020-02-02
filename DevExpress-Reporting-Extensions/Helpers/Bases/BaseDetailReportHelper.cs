﻿using System;

using DevExpress.XtraReports.UI;

using DevExpressReportingExtensions.Reports;

namespace DevExpressReportingExtensions.Helpers.Base
{
    public abstract class BaseDetailReportHelper : BaseMasterDetailHelper
    {
        public DetailReportBand ContainerBand { get; protected set; }

        public string DataMember { get; protected set; }

        protected BaseDetailReportHelper(XtraReport report, string dataMember)
            : base(report)
        {
            this.DataMember = dataMember ?? throw new ArgumentNullException(nameof(dataMember));
            this.CreateIfNotExistDetailBandInRootReport();
            this.ContainerBand = this.CreateContainerBand();
        }

        private void CreateIfNotExistDetailBandInRootReport()
        {
            var result = this.RootReport.GetBandByType<DetailBand>();
            if (result == null)
            {
                result = new DetailBand()
                {
                    HeightF = 0F,
                };
                this.RootReport.Bands.Add(result);
            }
        }

        protected virtual DetailReportBand CreateContainerBand()
        {
            var result = new DetailReportBand
            {
                HeightF = 0F
            };

            result.DataSource = this.BaseReport.DataSource;
            result.InitializeDataMember(this.BaseReport.JoinWithDataMember(this.DataMember));

            if (this.BaseReport is DetailReportBand)
            {
                result.Level = ((DetailReportBand)this.BaseReport).Level + 1;
            }

            this.BaseReport.Bands.Add(result);
            return result;
        }

        protected virtual DetailBand CreateDetailContainer(XRControl control)
        {
            var result = this.ContainerBand.GetBandByType<DetailBand>();
            if (result == null)
            {
                result = new DetailBand()
                {
                    HeightF = 0F
                };
                this.ContainerBand.Bands.Add(result);
            }
            else
            {
                result.Controls.Clear();
                result.HeightF = 0F;
            }
            result.Controls.Add(control);
            return result;
        }

    }
}
