﻿using System.Linq;

using DevExpress.XtraReports.UI;

namespace DevExpressReportingExtensions.DecorationHelpers.BaseClasses
{
    public abstract class BaseGroupFooterHelper : BaseMasterDetailReportHelper
    {
        public GroupFooterBand ContainerBand { get; protected set; }

        protected BaseGroupFooterHelper(XtraReport report, XtraReportBase detailReport)
            : base(report, detailReport)
        {
            this.ContainerBand = this.CreateContainerBand();
        }

        protected virtual GroupFooterBand CreateContainerBand()
        {
            var result = new GroupFooterBand
            {
                KeepTogether = true,
                HeightF = 0F,
                GroupUnion = GroupFooterUnion.WithLastDetail,
                RepeatEveryPage = false,
                Level = this.Report.Bands.OfType<GroupFooterBand>().Count(),
            };

            this.Report.Bands.Add(result);

            return result;
        }

    }
}
